using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using VtpGameApi.Helpers;
using VtpGameApi.Models;

namespace VtpGameApi.Controllers
{
	public class HomeController : ApiController
	{
		// GET api/values
		[OutputCache(Duration = 10000, Location = OutputCacheLocation.Server)]
		public IEnumerable<Article> Get(int itemsCount = 10)
		{
			return ArticleHelper.GetArticlesFromHtml(@"http://web.kpi.kharkov.ua/otp/ru/2018/", itemsCount);
		}
	}
}
