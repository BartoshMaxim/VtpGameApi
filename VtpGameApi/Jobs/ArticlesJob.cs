using Quartz;
using System.Threading.Tasks;
using VtpGameApi.Helpers;

namespace VtpGameApi.Jobs
{
	public class ArticlesJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ArticleHelper.UpdateArticles();
        }
    }
}