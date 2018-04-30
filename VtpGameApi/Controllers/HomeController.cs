using HtmlAgilityPack;
using System.Linq;
using System.Web.Http;
using VtpGameApi.Helpers;

namespace VtpGameApi.Controllers
{
	public class HomeController : ApiController
	{
		// GET api/values
		public string Get()
		{
			var web = new HtmlWeb();
			var doc = web.Load(@"http://web.kpi.kharkov.ua/otp/ru/2018/");

			doc.DocumentNode.Descendants("input")
			.Select(y => y.Descendants()
			.Where(x => x.Attributes["class"].Value == "box"))
			.ToList();

			return string.Empty;
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
