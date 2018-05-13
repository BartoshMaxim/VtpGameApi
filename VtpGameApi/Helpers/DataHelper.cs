using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using VtpGameApi.Models;
using VtpGameApi.Models.ViewModel;

namespace VtpGameApi.Helpers
{
    public static class DataHelper
    {
        public static User GetUser(string login, string password)
        {
            using (var context = new SqlConnection(Settings.Sql))
            {
                return context.Query<User>(@"
                    SELECT
                        UserId
                        ,Login
                        ,Password
                    FROM
                        Users
                    WHERE
                        Login    = @login
                    AND
                        Password = @password
                ", new
                {
                    login,
                    password = ToMd5(password)
                }).FirstOrDefault();
            }
        }

        public static IEnumerable<Article> GetArticles(ArticleType articleType, int count = 10)
        {
            using (var context = new SqlConnection(Settings.Sql))
            {
                return context.Query<Article>($@"
                    SELECT TOP {count}
                        Header
                        ,Body
                        ,Uri
                        ,ImageUri
                    FROM
                        Articles
                    WHERE
                        ArticleTypeId    = @articletype
                ", new
                {
                    articletype = (int)articleType
                });
            }
        }

        public static void SaveArticles(IEnumerable<Article> articles, ArticleType type)
        {
            if (articles?.Any() ?? false)
            {
                var articlesStr = new StringBuilder();

                int id = GetNextId();

                if (id == 0)
                {
                    id = 1;
                }

                foreach (var article in articles)
                {
                    articlesStr.AppendFormat(",({0},{1},N'{2}',N'{3}',N'{4}',N'{5}')\n",
                        id,
                        (int)type,
                        !string.IsNullOrEmpty(article.Header) ? Regex.Replace(article.Header, " {2,}", " ") : string.Empty,
                        !string.IsNullOrEmpty(article.Body) ? Regex.Replace(article.Body, " {2,}", " ") : string.Empty,
                        !string.IsNullOrEmpty(article.Uri) ? Regex.Replace(article.Uri, " {2,}", " ") : string.Empty,
                        !string.IsNullOrEmpty(article.ImageUri) ? Regex.Replace(article.ImageUri, " {2,}", " ") : string.Empty);

                    id++;
                }

                using (var context = new SqlConnection(Settings.Sql))
                {
                    context.Execute($@"
                    INSERT INTO Articles (ArticleId, ArticleTypeId, Header, Body, Uri, ImageUri)
                    VALUES {articlesStr.ToString().Substring(1)}
                ");
                }
            }
        }

        public static void DeleteArticles(ArticleType type)
        {
            using (var context = new SqlConnection(Settings.Sql))
            {
                context.Execute($@"
                   DELETE FROM Articles
                    WHERE ArticleTypeId = @type
                ", new
                {
                    type = (int)type
                });
            }
        }

        public static int GetCountRows()
        {
            using (var context = new SqlConnection(Settings.Sql))
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(ArticleId)       
                    FROM 
                        Articles");
            }
        }

        public static bool IsExists(int articleId)
        {
            using (var context = new SqlConnection(Settings.Sql))
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(ArticleId)
                FROM
                    Articles
                WHERE
                    ArticleId = @articleId
                ", new
                {
                    articleId
                }) != 0;
            }
        }

        public static T GetPromo<T>() where T : PromoBase
        {
            using (var context = new SqlConnection(Settings.Sql))
            {
                return context.QueryFirst<T>($@"
                    SELECT
                        ArticleType
                        ,PromoPeriod
                        ,ArticlesCount
                    FROM
                        Settings
                    WHERE
                        SettingName = @settingName
                ", new
                {
                    settingName = typeof(T).Name
                });
            }
        }

        public static T SavePromo<T>(this T promo) where T : PromoBase
        {
            StringBuilder setProp = new StringBuilder();
            setProp.AppendFormat("PromoPeriod = {0},\n", promo.PromoPeriod);

            if (promo is PromoList promoList)
            {
                setProp.AppendFormat("ArticlesCount = {0},\n", promoList.ArticlesCount);
                setProp.AppendFormat("ArticleType = {0}\n", (int)promoList.ArticleType);
            }
            else if (promo is SmallPromo smallPromo)
            {
                setProp.AppendFormat("ArticleType = {0}\n", (int)smallPromo.ArticleType);
            }
            else if (promo is BigPromo bigPromo)
            {
                setProp.Replace(",", "");
            }

            using (var context = new SqlConnection(Settings.Sql))
            {
                return context.ExecuteScalar<T>($@"
                    UPDATE Settings
                    SET
                        {setProp.ToString()}
                    WHERE
                        SettingName = @settingName
                ", new
                {
                    settingName = typeof(T).Name
                });
            }
        }

        private static string ToMd5(string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private static int GetNextId()
        {
            var articleID = GetCountRows();

            while (IsExists(articleID))
            {
                articleID++;
            }
            return articleID;
        }
    }
}