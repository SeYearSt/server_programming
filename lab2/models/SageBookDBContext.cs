using System.Data.Entity;

namespace Lab2.Models
{
    public class SageBookDBContext: DbContext
    {
        public SageBookDBContext()
            : base("SabeBookWeb")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Sage> Sages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasMany<Sage>(s => s.Sages)
                        .WithMany(c => c.Books)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("IdBook");
                            cs.MapRightKey("IdSage");
                            cs.ToTable("SageBooks");
                        });
        }
    }
}
