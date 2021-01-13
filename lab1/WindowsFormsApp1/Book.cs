using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Book
    { 
        public Book() {
            this.Sages = new HashSet<Sage>();
        }
        public int id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public virtual ICollection<Sage> Sages { get; set; }
    }
}
