using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using VtpGameApi.Models;
using System.Web;
using System.IO;

namespace VtpGameApi.Helpers
{
    public static class ArticleHelper
    {
        public static IEnumerable<Article> GetSecondArticleList(string uri, int itemsCount = 20)
        {
            var htmlNodes = new HtmlWeb().Load(uri).DocumentNode.SelectNodes("//div[contains(@class,'item-post')]");

            var articleList = new List<Article>();

            int i = 1;
            foreach (var htmlNode in htmlNodes)
            {
                var article = new Article();

                var headerNode = htmlNode.SelectNodes($"//div[contains(@class,'item-post')][{i}]/div/h2/a").FirstOrDefault();

                StringWriter tempWriter = new StringWriter();

                if (headerNode != null)
                {
                    HttpUtility.HtmlDecode(headerNode.InnerText, tempWriter);
                    article.Header = tempWriter.ToString();
                    article.Uri = headerNode.Attributes["href"]?.Value;
                }

                var bodyNode = htmlNode.SelectNodes($"//div[contains(@class,'item-post')][{i}]/div/div[contains(@class,'post-content')]").FirstOrDefault();


                if (bodyNode != null)
                {
                    HttpUtility.HtmlDecode(bodyNode.InnerText, tempWriter);
                    article.Body = tempWriter.ToString();
                }


                try
                {
                    var imageNode = htmlNode.SelectNodes($"//div[contains(@class,'item-post')][{i}]/div[contains(@class,'post-thumb')]/a/img").FirstOrDefault();

                    if (imageNode != null)
                    {
                        article.ImageUri = imageNode.Attributes["src"]?.Value;
                    }
                }
                catch { }

                articleList.Add(article);

                if (articleList.Count == itemsCount)
                {
                    break;
                }

                i++;
            }

            return articleList;
        }

        public static IEnumerable<Article> GetThirdArticleList(string uri, int itemsCount = 20)
        {
            var htmlNodes = new HtmlWeb().Load(uri).DocumentNode.SelectNodes("//article");

            var articleList = new List<Article>();

            int i = 1;
            foreach (var htmlNode in htmlNodes)
            {
                var article = new Article();

                var headerNode = htmlNode.SelectNodes($"//article[{i}]/header/h1/a").FirstOrDefault();

                StringWriter tempWriter = new StringWriter();

                if (headerNode != null)
                {
                    HttpUtility.HtmlDecode(headerNode.InnerText, tempWriter);
                    article.Header = tempWriter.ToString();
                    article.Uri = headerNode.Attributes["href"]?.Value;
                }
                try
                {
                    var bodyNode = htmlNode.SelectNodes($"//article[{i}]/div[contains(@class,'entry-content')]/p/span").FirstOrDefault();


                    if (bodyNode != null)
                    {
                        HttpUtility.HtmlDecode(bodyNode.InnerText, tempWriter);
                        article.Body = tempWriter.ToString();
                        article.Uri = headerNode.Attributes["href"]?.Value;
                    }
                }
                catch { }

                try
                {
                    var imageNode = htmlNode.SelectNodes($"//article[{i}]/div[contains(@class,'entry-content')]/p/a/img").FirstOrDefault();

                    if (imageNode != null)
                    {
                        article.ImageUri = imageNode.Attributes["src"]?.Value;
                    }
                }
                catch { }

                articleList.Add(article);

                if (articleList.Count == itemsCount)
                {
                    break;
                }

                i++;
            }

            return articleList;
        }

        public static void UpdateArticles()
        {
            var secondArticleList = GetSecondArticleList(@"http://web.kpi.kharkov.ua/otp/ru/");
            var thirdArticleList = GetThirdArticleList(@"http://web.kpi.kharkov.ua/otp/ru/2018/");

            DataHelper.DeleteArticles(ArticleType.SecondList);
            DataHelper.DeleteArticles(ArticleType.ThirdList);

            DataHelper.SaveArticles(secondArticleList, ArticleType.SecondList);
            DataHelper.SaveArticles(thirdArticleList, ArticleType.ThirdList);
        }
    }
}