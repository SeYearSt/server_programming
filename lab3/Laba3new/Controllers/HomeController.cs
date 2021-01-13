using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laba3new.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Enter()
        {
            return View();
        }
        public ActionResult Sage()
        {
            return View();
        }
        public ActionResult Purchase()
        {
            return View();
        }
        
    }
}
