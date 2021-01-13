using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Book
    {
        [Key]
        public int IdBook { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public virtual ICollection<Sage> Sages { get; set; }
        public Book()
        {
            this.Sages = new HashSet<Sage>();
        }
    }
}
