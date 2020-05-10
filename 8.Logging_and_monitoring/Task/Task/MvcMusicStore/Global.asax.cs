using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Infrastructure;
using System.Diagnostics;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.RegisterType<Counters>().SingleInstance();
            builder.RegisterType<MvcMusicLogger>().As<ILogger>().SingleInstance();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger.Info("Application started");

            if (CategoryNotExists()) SetupCategory();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger.Error(ex.ToString());
        }

        private static bool CategoryNotExists()
            => !PerformanceCounterCategory.Exists("MvcMusicStoreCategory");

        private static void SetupCategory()
        {
            var counterDataCollection = new CounterCreationDataCollection();

            var visitHomeNumber = new CounterCreationData
            {
                CounterType = PerformanceCounterType.NumberOfItems64,
                CounterName = "NumberOfGoingToHome"
            };

            counterDataCollection.Add(visitHomeNumber);

            var logInNumber = new CounterCreationData
            {
                CounterType = PerformanceCounterType.NumberOfItems64,
                CounterName = "logInNumber"
            };

            counterDataCollection.Add(logInNumber);

            var logOffNumber = new CounterCreationData
            {
                CounterType = PerformanceCounterType.NumberOfItems64,
                CounterName = "logOffNumber"
            };

            counterDataCollection.Add(logOffNumber);

            PerformanceCounterCategory.Create("MvcMusicStoreCategory", "MvcMusicStore category.",
                PerformanceCounterCategoryType.SingleInstance, counterDataCollection);
        }
    }
}
