using System.Collections.Generic;

namespace VtpGameApi.Models.Api
{
	public class PromoApiModel
	{
		public IEnumerable<Article> Articles { get; set; }

		public int PromoPeriod { get; set; }

		public PromoApiModel(){ }
	}
}