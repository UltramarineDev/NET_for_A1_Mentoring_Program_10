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
            builder.RegisterType<MvcMusicLogger>().As<ILogger>();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger.Info("Application started");

            if (!IsCategoryexists())
            {
                SetupCategory();
                CreateCounters();
            }
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger.Error(ex.ToString());
        }

        private static bool IsCategoryexists()
            => PerformanceCounterCategory.Exists("GoToHomeNumberCategory");

        private static void SetupCategory()
        {
            CounterCreationDataCollection counterDataCollection = new CounterCreationDataCollection();

            CounterCreationData visitHomeNumber = new CounterCreationData
            {
                CounterType = PerformanceCounterType.NumberOfItems64,
                CounterName = "NumberOfGoingToHome"
            };

            counterDataCollection.Add(visitHomeNumber);

            CounterCreationData logInNumber = new CounterCreationData
            {
                CounterType = PerformanceCounterType.NumberOfItems64,
                CounterName = "logInNumber"
            };

            counterDataCollection.Add(logInNumber);

            CounterCreationData logOffNumber = new CounterCreationData
            {
                CounterType = PerformanceCounterType.NumberOfItems64,
                CounterName = "logOffNumber"
            };

            counterDataCollection.Add(logOffNumber);

            PerformanceCounterCategory.Create("MvcMusicStoreCategory", "MvcMusicStore category.",
                PerformanceCounterCategoryType.SingleInstance, counterDataCollection);
        }

        private static void CreateCounters()
        {
            Counters.GoToHome = new PerformanceCounter("MvcMusicStoreCategory", "NumberOfGoingToHome", false);

            Counters.LogIn = new PerformanceCounter("MvcMusicStoreCategory", "logInNumber", false);

            Counters.LogOff = new PerformanceCounter("MvcMusicStoreCategory", "logOffNumber", false);

            Counters.GoToHome.RawValue = 0;
            Counters.LogIn.RawValue = 0;
            Counters.LogOff.RawValue = 0;
        }
    }
}
