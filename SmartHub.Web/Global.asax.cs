using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire.Logging;
using SmartHub.Web.App_Start;
using Hangfire;

namespace SmartHub.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = AutofacMvcConfig.RegisterAll(
                typeof(MvcApplication).Assembly,
                Directory.GetFiles(Server.MapPath("~/bin"), "SmartHub.*.dll").Select(x => Assembly.LoadFrom(x)).ToArray()
            );

            AutofacConfig.Register(container);
            GlobalConfiguration.Configuration.UseAutofacActivator(container);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SecurityConfig.RegisterProtocol();
        }
    }
}
