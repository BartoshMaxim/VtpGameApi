using System.Web.Mvc;
using VtpGameApi.Helpers;
using VtpGameApi.Models;
using VtpGameApi.Models.ViewModel;

namespace VtpGameApi.Controllers
{
	public class PromoListController : Controller
	{
		public ActionResult Index(ArticleType articleType, int count = 10)
		{
			return View(new PromoPreviewVM(DataHelper.GetArticles(articleType, count), articleType, count));
		}
	}
}