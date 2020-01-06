using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using StudyWebApp.Models;

namespace StudyWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           ViewBag.IP=HttpContext.Features.Get<IHttpConnectionFeature>().LocalIpAddress;
            ViewBag.Port = HttpContext.Features.Get<IHttpConnectionFeature>().LocalPort;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("test")))
            {
                ViewBag.se="empty";
                HttpContext.Session.SetString("test","my value");
            }
            else
            {
                ViewBag.se= HttpContext.Session.GetString("test");
            }
            return View();
        }

        public IActionResult About()
        {
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
