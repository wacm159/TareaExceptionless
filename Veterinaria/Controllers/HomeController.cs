using Exceptionless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veterinaria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Submit logs
            ExceptionlessClient.Default.SubmitLog("Logging made easy");

            // You can also specify the log source and log level.
            // We recommend specifying one of the following log levels: Trace, Debug, Info, Warn, Error
            ExceptionlessClient.Default.SubmitLog(typeof(InvalidProgramException).FullName, "This is so easy", "Info");
            ExceptionlessClient.Default.CreateLog(typeof(InvalidProgramException).FullName, "This is so easy", "Info").AddTags("Exceptionless").Submit();

            // Submit feature usages
            ExceptionlessClient.Default.SubmitFeatureUsage("MyFeature");
            ExceptionlessClient.Default.CreateFeatureUsage("MyFeature").AddTags("Exceptionless").Submit();

            // Submit a 404
            ExceptionlessClient.Default.SubmitNotFound("/somepage");
            ExceptionlessClient.Default.CreateNotFound("/somepage").AddTags("Exceptionless").Submit();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}