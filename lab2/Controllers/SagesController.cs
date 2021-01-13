using System.Linq;
using System.Net;
using System.Web.Mvc;
using Lab2.Models;
using Lab2.Patterns.Repositories;

namespace Lab2.Controllers
{
    public class SagesController : Controller
    {
        private IRepository<Sage> repo;

        public SagesController(SageRepository sage)
        {
            this.repo = sage;
        }
        // GET: Sages
        public ActionResult Index()
        {
            return View(this.repo.GetAll().ToList());
        }

        // GET: Sages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sage sage = this.repo.Get((int)id);
            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }
    }
}
