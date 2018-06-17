using System.Web.Mvc;
using Hangfire;
using Hangfire.Logging;
using Microsoft.Owin;
using Owin;
using SmartHub.Helper;
using SmartHub.Web.Processors;

[assembly: OwinStartup(typeof(SmartHub.Web.Startup))]
namespace SmartHub.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new TextBufferLogProvider());
            TextBuffer.WriteLine("Application started.");

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireDbConnection");

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                ServerName = "SmartHub",
                WorkerCount = ConfigHelper.AppSettings<int>("WorkerCount",2),
            });

            var connectionCheckIntervalInMinutes = ConfigHelper.AppSettings<int>("ConnectionCheckIntervalInMinutes", 5);
            RecurringJob.AddOrUpdate<ISmartHubConnectionProcessor>("SmartHubConnectionJob", p => p.Proceed(),Cron.MinuteInterval(connectionCheckIntervalInMinutes));
        }
    }
}