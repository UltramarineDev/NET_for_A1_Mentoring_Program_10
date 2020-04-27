using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Infrastructure;
using MvcMusicStore.Models;
using PerformanceCounterHelper;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly ILogger _logger;
        private static CounterHelper<Counters> _counterHelper;

        public HomeController(ILogger logger)
        {
            _logger = logger;
            _counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Test project");
        }

        // GET: /Home/
        public async Task<ActionResult> Index()
        {
            _logger.Debug("Enter to home page");

           _counterHelper.Increment(Counters.GoToHome);

            return View(await _storeContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(6)
                .ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storeContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}