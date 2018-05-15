using VtpGameApi.Models;
using VtpGameApi.Models.Api;
using VtpGameApi.Models.ViewModel;

namespace VtpGameApi.Helpers
{
	public static class PromoHelper
	{
		public static PromoApiModel GetPromoList()
		{
			var promoListSettings = DataHelper.GetPromoSettings<PromoList>();

			return new PromoApiModel { Articles = DataHelper.GetArticles(promoListSettings.ArticleType, promoListSettings.ArticlesCount), PromoPeriod = promoListSettings.PromoPeriod };
		}

		public static PromoApiModel GetSmallPromo()
		{
			var promoListSettings = DataHelper.GetPromoSettings<SmallPromo>();

			return new PromoApiModel { Articles = DataHelper.GetArticles(promoListSettings.ArticleType, 1), PromoPeriod = promoListSettings.PromoPeriod };
		}

		public static PromoApiModel GetBigPromo()
		{
			var promoListSettings = DataHelper.GetPromoSettings<BigPromo>();

			return new PromoApiModel { Articles = DataHelper.GetArticles(ArticleType.BigPromo, 1), PromoPeriod = promoListSettings.PromoPeriod };
		}
	}
}