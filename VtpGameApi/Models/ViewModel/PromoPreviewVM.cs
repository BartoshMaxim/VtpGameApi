using System.Collections.Generic;

namespace VtpGameApi.Models.ViewModel
{
	public class PromoPreviewVM
	{
		public IEnumerable<Article> Articles { get; set; }

		public ArticleType ArticleType { get; set; }

		public int Count{ get; set; }

		public PromoPreviewVM(IEnumerable<Article> articles, ArticleType articleType, int count)
		{
			ArticleType = articleType;
			Articles = articles;
			Count = count;
		}
	}
}