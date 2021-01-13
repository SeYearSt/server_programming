using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Sage
    {
        [Key]
        public int IdSage { get; set; }
        [Required]
        public string name { get; set; }
        public int age { get; set; }
        public string photo { get; set; }
        public string city { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public Sage()
        {
            this.Books = new HashSet<Book>();
        }
    }
}
