using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Index()
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

        public ActionResult Preview(ArticleType articleType, string promoType, int count)
        {
            switch (promoType)
            {
                case "PromoList":
                    return PartialView("Preview", DataHelper.GetArticles(articleType, count));
                case "SmallPromo":
                    if (articleType == ArticleType.Custom)
                    {
                        articleType = ArticleType.SmallPromo;
                    }

                    return PartialView("Preview", DataHelper.GetArticles(articleType, 1));
                case "BigPromo":
                    return PartialView("Preview", DataHelper.GetArticles(ArticleType.BigPromo, 1));
            }
            return View("Index");
        }
    }
}