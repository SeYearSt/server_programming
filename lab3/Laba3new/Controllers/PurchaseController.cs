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
    public class PurchaseController : ApiController
    {
        IRepository<Purchase> db = new PurchaseRepository(new BookSage());
        // GET api/values
        public IEnumerable<Purchase> GetSages()
        {
            return db.GetAll();
        }

        // GET api/values/5
        public Purchase GetSage(int id)
        {
            Purchase purchase = db.GetBook(id);
            return purchase;
        }

        // POST api/values
        [HttpPost]
        public void CreatePurchase([FromBody]Purchase purchase)
        {
            db.Create(purchase);
        }

        // PUT api/values/5
        [HttpPut]
        public void EditSage(int id, [FromBody]Purchase purchase)
        {
            if (id == purchase.Id)
            {
                db.Update(purchase);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public void DeleteSage(int id)
        {
            Purchase purchase = db.GetBook(id);
            if (purchase != null)
            {
                db.Delete(id);
            }
        }
    }
}
