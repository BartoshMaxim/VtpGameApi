using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using VtpGameApi.Models;

namespace VtpGameApi.Helpers
{
	public static class ArticleHelper
	{
		public static IEnumerable<Article> GetArticlesFromHtml(string uri)
		{
			var htmlNodes = new HtmlWeb().Load(uri).DocumentNode.SelectNodes("//article");

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
			}

			return null;
		}
	}
}