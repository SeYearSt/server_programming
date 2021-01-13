using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Lab2.Models;
using Lab2.Patterns;

namespace Lab2.Areas.Admin.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private UnitOfWork UoW;

        public BooksController(IUnitOfWork unitOfWork)
        {
            this.UoW = (UnitOfWork)unitOfWork;
        }

        // GET: Admin/Books
        public ActionResult Index()
        {
            return View(this.UoW.Books.GetAll());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = this.UoW.Books.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            Book book = new Book();
            foreach(var item in this.UoW.Sages.GetAll())
                book.Sages.Add(item);
            return View(book);
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBook,name,description")] Book book, List<int> Sages)
        {
            if (ModelState.IsValid)
            {
                if (Sages != null)
                {
                    foreach (var id in Sages)
                        book.Sages.Add(this.UoW.Sages.Get(id));
                }
                this.UoW.Books.Add(book);
                this.UoW.Complete();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = this.UoW.Books.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBook,name,description")] Book book, List<int> Sages)
        {
            if (ModelState.IsValid)
            {
                Book changedBook = this.UoW.Books.Get(book.IdBook);
                changedBook.name = book.name;
                changedBook.description = book.description;
                
                if(Sages != null)
                {
                    foreach (var id in Sages)
                    {
                        var sage = this.UoW.Sages.Get(id);
                        if (!changedBook.Sages.Contains(sage))
                            changedBook.Sages.Add(sage);
                    }
                    foreach (var id in changedBook.Sages.Select(x => x.IdSage))
                    {
                        if (!Sages.Contains(id))
                            changedBook.Sages.Remove(this.UoW.Sages.Get(id));
                    }
                }

                this.UoW.Complete();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = this.UoW.Books.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = this.UoW.Books.Get(id);
            if (item != null)
            {
                this.UoW.Books.Remove(item);
                this.UoW.Complete();
                return RedirectToAction("Index");
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // GET: Admin/Books/RemoveSage/?BookId=x&SageId=y&returnUrl=
        public ActionResult RemoveSage(int BookId, int SageId, string returnUrl)
        {
            this.UoW.Books.Get(BookId).Sages.Remove(this.UoW.Sages.Get(SageId));
            this.UoW.Complete();
            return Redirect(returnUrl);
        }

        // GET: Admin/Books/AddSage/Id
        public ActionResult AddSage(int Id)
        {
            List<Sage> sages = this.UoW.Sages.GetAll().Where(x => !this.UoW.Books.Get(Id).Sages.Contains(x)).ToList();
            ViewBag.AddSageToBookId = Id;
            return View(sages);
        }

        // POST: Admin/Books/AddSage
        [HttpPost]
        public ActionResult AddSage(int BookId, List<int> Sages)
        {
            Book book = this.UoW.Books.Get(BookId);
            foreach (var SageId in Sages)
            {
                book.Sages.Add(this.UoW.Sages.Get(SageId));
            }
            this.UoW.Complete();
            return RedirectToAction("Edit", new { id = BookId });
        }
    }
}
