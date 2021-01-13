using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Laba3new.Models
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(BookSage db) : base(db)
        {

        }
    }
}