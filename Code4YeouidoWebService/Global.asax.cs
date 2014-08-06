using Code4YeouidoWebService.Helpers;
using FluentScheduler;
using FluentScheduler.Model;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Code4YeouidoWebService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TaskManager.UnobservedTaskException += TaskManager_UnobservedTaskException;
            TaskManager.Initialize(new ScheduleRegistry());
        }

        static void TaskManager_UnobservedTaskException(TaskExceptionInformation sender, UnhandledExceptionEventArgs e)
        {
            System.Console.Out.WriteLine("An error happened with a scheduled task: " + e.ExceptionObject);
        }
    }
}
