﻿using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Infrastructure;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly ILogger _logger;
        private readonly Counters _counters;

        public HomeController(ILogger logger, Counters counters)
        {
            _logger = logger;
            _counters = counters;
        }

        // GET: /Home/
        public async Task<ActionResult> Index()
        {
            _logger.Debug("Enter to home page");

            _counters.GoToHome.Increment();

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