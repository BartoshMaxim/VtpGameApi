using System.Linq;
using System.Web.Mvc;
using VtpGameApi.Helpers;
using VtpGameApi.Models;
using VtpGameApi.Models.ViewModel;

namespace VtpGameApi.Controllers
{
	[Authorize]
    public class PromoController : Controller
    {
        // GET: Promo
        public ActionResult Index(string controllerName)
        {
            PromoSettingsVM settingsVM = new PromoSettingsVM();
            settingsVM.SetDefault();

            return View(settingsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PromoSettingsVM settingsVM)
        {
            if (ModelState.IsValid)
            {
                settingsVM.Save();
            }
            else
            {
                ModelState.AddModelError("", "Error settings");
            }

            return View(settingsVM);
        }

		public ActionResult PromoList(ArticleType articleType, int count = 10)
		{
			return View(new PromoPreviewVM(DataHelper.GetArticles(articleType, count), articleType, count));
		}

		public ActionResult SmallPromo(ArticleType articleType)
		{
			return View(new ArticleVM { Article = DataHelper.GetArticles(articleType, 1).FirstOrDefault(), ActionName = "SmallPromo", ArticleType = articleType, Count = 1 });
		}

		public ActionResult BigPromo(ArticleType articleType)
		{
			return View(new ArticleVM { Article = DataHelper.GetArticles(articleType, 1).FirstOrDefault(), ActionName = "BigPromo", ArticleType = articleType, Count = 1 });
		}
	}
}