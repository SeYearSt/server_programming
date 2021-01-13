using Lab2.Models;
using Lab2.Patterns;
using Lab2.Patterns.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Book> bookRepo;
        public HomeController(IRepository<Book> bookRepository)
        {
            this.bookRepo = bookRepository;
        }
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction(controllerName: "Home", actionName: "Index", routeValues: new { area = "Admin" });
            
            return View(this.bookRepo.GetAll().ToList());
        }
    }
}