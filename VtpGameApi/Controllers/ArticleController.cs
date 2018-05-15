using System.Web.Mvc;
using VtpGameApi.Helpers;
using VtpGameApi.Models;
using VtpGameApi.Models.ViewModel;

namespace VtpGameApi.Controllers
{
	public class ArticleController : Controller
    {
		public ActionResult Add(string controllerName, ArticleType articleType, int count)
		{
			ArticleVM articleVM = new ArticleVM
			{
				ArticleType = articleType,
				ActionName = controllerName,
				Count = count
			};

			return View(articleVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(ArticleVM articleVM)
		{
			if (ModelState.IsValid)
			{
				DataHelper.SaveArticles(new[] { articleVM.Article }, articleVM.ArticleType);
				return RedirectToAction(articleVM.ActionName, "Promo", new { articleType = articleVM.ArticleType, count = articleVM.Count });
			}

			return View(articleVM);
		}

		public ActionResult Edit(int id, string actionName, ArticleType articleType, int count)
		{
			var article = DataHelper.GetArticleById(id);

			if (article == null)
			{
				return HttpNotFound();
			}

			ArticleVM articleVM = new ArticleVM
			{
				ArticleType = articleType,
				ActionName = actionName,
				Count = count,
				Article = article
			};

			return View(articleVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ArticleVM articleVM)
		{
			if (ModelState.IsValid)
			{
				DataHelper.UpdateArticle(articleVM.Article);
				return RedirectToAction(articleVM.ActionName, "Promo", new { articleType = articleVM.ArticleType, count = articleVM.Count });
			}

			return View();
		}

		public ActionResult Delete(int id, string actionName, ArticleType articleType, int count)
		{
			var article = DataHelper.GetArticleById(id);

			if (article == null)
			{
				return HttpNotFound();
			}

			ArticleVM articleVM = new ArticleVM
			{
				ArticleType = articleType,
				ActionName = actionName,
				Count = count,
				Article = article
			};

			return View(articleVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(ArticleVM articleVM)
		{
			if (ModelState.IsValid)
			{
				DataHelper.DeleteArticle(articleVM.Article);
				return RedirectToAction(articleVM.ActionName, "Promo", new { articleType = articleVM.ArticleType, count = articleVM.Count });
			}

			return View(articleVM);
		}
	}
}