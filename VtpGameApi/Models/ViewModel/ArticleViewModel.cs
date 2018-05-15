using System.Web;

namespace VtpGameApi.Models.ViewModel
{
	public class ArticleVM
	{
		public Article Article { get; set; }

		public string ActionName { get; set; }

		public ArticleType ArticleType { get; set; }

		public int Count { get; set; }

		public HttpPostedFileBase ImageFile {get;set;}
	}
}