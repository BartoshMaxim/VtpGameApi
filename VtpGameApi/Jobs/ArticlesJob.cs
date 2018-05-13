using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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