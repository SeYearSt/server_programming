using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Microsoft.EntityFrameworkCore;
//using System.Configuration;
using System.Data.Entity;

namespace WindowsFormsApp1
{
    public class SageBookDbContext: DbContext
    {
        //base(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString) { }
        // conStr
        //public SageBookDbContext() : base("conStr") { }

        public SageBookDbContext() : base("SageBookDb") { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Sage> Sages { get; set; }
    }
}
