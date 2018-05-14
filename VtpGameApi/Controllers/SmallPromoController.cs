using System.Linq;
using System.Web.Mvc;
using VtpGameApi.Helpers;
using VtpGameApi.Models;
using VtpGameApi.Models.ViewModel;

namespace VtpGameApi.Controllers
{
	public class SmallPromoController : Controller
	{
		public ActionResult Index(ArticleType articleType)
		{
			return View(new ArticleVM { Article = DataHelper.GetArticles(articleType, 1).FirstOrDefault(), ControllerName = "SmallPromo", ArticleType = articleType, Count = 1 });
		}
	}
}