using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laba3new.Models
{
    public class SageRepository : BaseRepository<Sage>
    {
        public SageRepository(BookSage db) : base(db)
        {

        }
    }
}