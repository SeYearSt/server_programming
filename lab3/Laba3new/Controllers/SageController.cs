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
    public class SageController : ApiController
    {
        IRepository<Sage> db = new SageRepository(new BookSage());
        // GET api/values
        [Authorize]
        public IEnumerable<Sage> GetSages()
        {
            return db.GetAll();
        }
        // GET api/values/5
        public Sage GetSage(int id)
        {
            Sage sage = db.GetBook(id);
            return sage;
        }

        // POST api/values
        [HttpPost]
        public void CreateBook([FromBody]Sage sage)
        {
            db.Create(sage);
        }

        // PUT api/values/5
        [HttpPut]
        public void EditSage(int id, [FromBody]Sage sage)
        {
            if (id == sage.Id)
            {
                db.Update(sage);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public void DeleteSage(int id)
        {
            Sage sage = db.GetBook(id);
            if (sage != null)
            {
                db.Delete(id);
            }
        }
    }
}
