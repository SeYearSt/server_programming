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
    public class SagesController : Controller
    {
        private UnitOfWork UoW;

        public SagesController(IUnitOfWork unitOfWork)
        {
            this.UoW = (UnitOfWork)unitOfWork;
        }

        // GET: Admin/Sages
        public ActionResult Index()
        {
            return View(this.UoW.Sages.GetAll());
        }

        // GET: Admin/Sages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sage sage = this.UoW.Sages.Get((int)id);
            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }

        // GET: Admin/Sages/Create
        public ActionResult Create()
        {
            Sage sage = new Sage();
            foreach (var item in this.UoW.Books.GetAll())
                sage.Books.Add(item);
            return View(sage);
        }

        // POST: Admin/Sages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSage,name,age,photo,city")] Sage sage, List<int> Books)
        {
            if (ModelState.IsValid)
            {
                if (Books != null)
                {
                    foreach (var id in Books)
                        sage.Books.Add(this.UoW.Books.Get(id));
                }
                this.UoW.Sages.Add(sage);
                this.UoW.Complete();
                return RedirectToAction("Index");
            }

            return View(sage);
        }

        // GET: Admin/Sages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sage sage = this.UoW.Sages.Get((int)id);
            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }

        // POST: Admin/Sages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSage,name,age,photo,city")] Sage sage, List<int> Books)
        {
            if (ModelState.IsValid)
            {
                Sage changedSage = this.UoW.Sages.Get(sage.IdSage);
                changedSage.name = sage.name;
                changedSage.age = sage.age;
                changedSage.photo = sage.photo;
                changedSage.city = sage.city;
                if(Books != null)
                {
                    foreach (var id in Books)
                    {
                        var book = this.UoW.Books.Get(id);
                        if (!changedSage.Books.Contains(book))
                            changedSage.Books.Add(book);
                    }

                    foreach (var id in changedSage.Books.Select(x => x.IdBook))
                    {
                        if (!Books.Contains(id))
                            changedSage.Books.Remove(this.UoW.Books.Get(id));
                    }
                }
                this.UoW.Complete();
                return RedirectToAction("Index");
            }
            return View(sage);
        }

        // GET: Admin/Sages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sage sage = this.UoW.Sages.Get((int)id);
            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }

        // POST: Admin/Sages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.UoW.Sages.Remove(this.UoW.Sages.Get(id));
            this.UoW.Complete();
            return RedirectToAction("Index");
        }
        // GET: Admin/Sages/RemoveBook/?BookId=x&SageId=y&returnUrl=
        public ActionResult RemoveBook(int BookId, int SageId, string returnUrl)
        {
            this.UoW.Books.Get(BookId).Sages.Remove(this.UoW.Sages.Get(SageId));
            this.UoW.Complete();
            return Redirect(returnUrl);
        }

        // GET: Admin/Sages/AddBook/Id
        public ActionResult AddBook(int Id)
        {
            List<Book> books = this.UoW.Books.GetAll().Where(x => !this.UoW.Sages.Get(Id).Books.Contains(x)).ToList();
            ViewBag.AddBookToSageId = Id;
            return View(books);
        }

        // POST: Admin/Sages/AddBook
        [HttpPost]
        public ActionResult AddBook(int SageId, List<int> Books)
        {
            Sage sage = this.UoW.Sages.Get(SageId);
            foreach (var id in Books)
            {
                sage.Books.Add(this.UoW.Books.Get(id));
            }
            this.UoW.Complete();
            return RedirectToAction("Edit", new { id = SageId });
        }
    }
}
