using Newtonsoft.Json;
using System.Web.Http;
using VtpGameApi.Helpers;
using VtpGameApi.Models.Api;

namespace VtpGameApi.Controllers
{
	public class HomeController : ApiController
	{
		public PromoApiModel Get(string promoType)
		{
			PromoApiModel promoRes = new PromoApiModel();

			switch (promoType)
			{
				case "PromoList":
					promoRes = PromoHelper.GetPromoList();
					break;
				case "SmallPromo":
					promoRes = PromoHelper.GetSmallPromo();
					break;
				case "BigPromo":
					promoRes = PromoHelper.GetBigPromo();
					break;
			}

			return promoRes;
		}
	}
}
