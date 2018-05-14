namespace VtpGameApi.Models.ViewModel
{
	public class ArticleVM
	{
		public Article Article { get; set; }

		public string ControllerName { get; set; }

		public ArticleType ArticleType { get; set; }

		public int Count { get; set; }
	}
}