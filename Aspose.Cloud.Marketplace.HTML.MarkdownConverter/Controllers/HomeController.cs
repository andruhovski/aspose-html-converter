using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Aspose.Cloud.Marketplace.HTML.Converter.Models;
using Aspose.Cloud.Marketplace.HTML.Converter.Services;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Controllers
{
    /// <summary>
    /// Controller for Web UI
    /// </summary>
    public class HomeController : Controller
    {
        private readonly StatisticalService _statisticalService;

        /// <summary>
        /// Constructor initialize basic MVC Controller
        /// </summary>
        /// <param name="statisticalService">StatisticalService holds </param>
        public HomeController(StatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }
        /// <summary>
        /// Home page 
        /// </summary>
        /// <returns>Views/Home/Index.cshtml</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Page for stats
        /// </summary>
        /// <returns>Views/Home/Statistic.cshtml</returns>
        public IActionResult Statistic()
        {
            var top100 = _statisticalService.Stats.ToArray().OrderByDescending(e => e.Value).Take(100);
            return View(top100);
        }

        /// <summary>
        /// Health Check page. Intended for monitoring tools
        /// </summary>
        /// <returns>Views/Home/Statistic.cshtml</returns>
        public IActionResult Status()
        {
            ViewBag.Message = $"Health check: {DateTime.Now}";
            return View();
        }

        /// <summary>
        /// Default error page
        /// </summary>
        /// <returns>Views/Shared/Statistic.cshtml</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
