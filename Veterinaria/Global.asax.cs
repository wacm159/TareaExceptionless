using Exceptionless;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Veterinaria
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ExceptionlessClient.Default.Configuration.DefaultData["FirstName"] = "Williams";
            ExceptionlessClient.Default.Configuration.DefaultData["IgnoredProperty"] = "Mal Ingreso Datos";

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ExceptionlessClient.Default.Configuration.UseTraceLogger();
            ExceptionlessClient.Default.Configuration.UseReferenceIds();
            ExceptionlessClient.Default.RegisterWebApi(GlobalConfiguration.Configuration);
        }
    }
}
