using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Laba3new.Models
{
    public class PurchaseRepository : BaseRepository<Purchase>
    {
        public PurchaseRepository(BookSage db) : base(db)
        {

        }
    }
}