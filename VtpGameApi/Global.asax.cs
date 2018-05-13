using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using VtpGameApi.Helpers;
using VtpGameApi.Jobs;

namespace VtpGameApi
{
    public class WebApiApplication : System.Web.HttpApplication
	{
        public object ArticlesScheduler { get; private set; }

        protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
            ArticlesSheduler.Start();
        }
	}
}
