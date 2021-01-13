using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Laba3new.Models
{
    public class BookSage : DbContext
    {
        public BookSage() : base("name=BookSage")
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sage> Sages { get; set; }
    }

}