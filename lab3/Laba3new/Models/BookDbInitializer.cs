using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Laba3new.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<BookSage>
    {
        protected override void Seed(BookSage db)
        {
            db.Books.Add(new Book { Name = "Гарри Потер", Author = "Me", Price = 111 });
            db.Books.Add(new Book { Name = "Taras", Author = "You", Price = 222 });

            db.Sages.Add(new Sage { Name = "Shevchenko", Age = 33, City = "Lviv" });
            db.Sages.Add(new Sage { Name = "Lesia", Age = 55, City = "Kyiv" });
            db.Sages.Add(new Sage { Name = "Ukrainka", Age = 42, City = "Odesa" });

            db.Sages.Add(new Sage { Name = "Shevchenko", Age = 33, City = "Lviv" });
            db.Sages.Add(new Sage { Name = "Lesia", Age = 55, City = "Kyiv" });
            db.Sages.Add(new Sage { Name = "Ukrainka", Age = 42, City = "Odesa" });

            base.Seed(db);
        }
    }
}