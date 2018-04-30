using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using VtpGameApi.Models;

namespace VtpGameApi.Helpers
{
	public static class ArticleHelper
	{
		public static IEnumerable<Article> GetArticlesFromHtml(string uri, int itemsCount = 10)
		{
			var htmlNodes = new HtmlWeb().Load(uri).DocumentNode.SelectNodes("//article");

			var articleList = new List<Article>();

			foreach (var htmlNode in htmlNodes)
			{
				var article = new Article();

				var headerNode = htmlNode.SelectNodes("//header/h1/a").FirstOrDefault();

				if (headerNode != null)
				{
					article.Header = headerNode.InnerText;
					article.Uri = headerNode.Attributes["href"]?.Value;
				}

				var bodyNode = htmlNode.SelectNodes("//div[contains(@class,'entry-content')]/p/span").FirstOrDefault();

				if (bodyNode != null)
				{
					article.Body = bodyNode.InnerText;
					article.Uri = headerNode.Attributes["href"]?.Value;
				}

				var imageNode = htmlNode.SelectNodes("//div[contains(@class,'entry-content')]/p/a/img").FirstOrDefault();

				if (imageNode != null)
				{
					article.ImageUri = imageNode.Attributes["src"]?.Value;
				}

				articleList.Add(article);

				if(articleList.Count == itemsCount)
				{
					break;
				}
			}

			return articleList;
		}
	}
}