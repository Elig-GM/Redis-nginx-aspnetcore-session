using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StudyWebApp.Models;

namespace StudyWebApp.Controllers
{
    public class HomeController : Controller
    {
        IndexModel m;
        public HomeController(IDistributedCache cache)
        {
            m=new IndexModel(cache);
        }
        public IActionResult Index()
        {
            
            m.OnPostResetCachedTime().Wait();
            HttpContext.Session.Set("What", new byte[] { 1, 2, 3, 4, 5 });
            ViewBag.IP=HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress;
            ViewBag.Port = HttpContext.Features.Get<IHttpConnectionFeature>().RemotePort;
            ViewBag.se = "empty";
           ViewBag.seId= HttpContext.Session.Id;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("test")))
            {
                ViewBag.se = "empty";
                HttpContext.Session.SetString("test", "my value");
            }
            else
            {
                ViewBag.se = HttpContext.Session.GetString("test");
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.C=m.OnGetAsync().Result;
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
