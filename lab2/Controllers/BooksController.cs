using System.Linq;
using System.Net;
using System.Web.Mvc;
using Lab2.Models;
using Lab2.Patterns.Repositories;

namespace Lab2.Controllers
{
    public class BooksController : Controller
    {
        private IRepository<Book> repo;
        public BooksController(BookRepository book)
        {
            this.repo = book;
        }

        // GET: Books
        public ActionResult Index()
        {
            return View(this.repo.GetAll().ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = this.repo.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
    }
}
