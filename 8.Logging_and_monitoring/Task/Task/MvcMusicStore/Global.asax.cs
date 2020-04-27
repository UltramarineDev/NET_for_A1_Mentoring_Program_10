using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.RegisterType<MvcMusicLogger>().As<ILogger>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger.Info("Application started");

            //using (var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Test project"))
            //{
            //    counterHelper.RawValue(Counters.GoToHome, 0);
            //}
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger.Error(ex.ToString());
        }
    }
}
