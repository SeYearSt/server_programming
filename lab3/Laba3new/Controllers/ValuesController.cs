using Laba3new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data;


namespace Laba3new.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        IRepository<Book> db = new BookRepository(new BookSage());
        // GET api/values
        /*public ValuesController(BookRepository db, PurchaseRepository pr)
        {
            this.db = db;
            this.pr = pr;
        }*/
        public IEnumerable<Book> GetBooks()
        {

            return db.GetAll();
        }
        // GET api/values/5
        public Book GetBook(int id)
        {
            Book book = db.GetBook(id);
            return book;
        }

        // POST api/values
        [HttpPost]
        public void CreateBook([FromBody]Book book)
        {
            db.Create(book);
        }

        // PUT api/values/5
        [HttpPut]
        public void EditBook(int id, [FromBody]Book book)
        {
            if (id == book.Id)
            {
                db.Update(book);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public void DeleteBook(int id)
        {
            Book book = db.GetBook(id);
            if (book != null)
            {
                db.Delete(id);
            }
        }
    }
}
