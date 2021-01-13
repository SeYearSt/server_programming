using Lab2.Models;

namespace Lab2.Patterns.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(SageBookDBContext context)
            : base(context)
        {
        }

        public SageBookDBContext SageBookDBContext
        {
            get
            {
                return context as SageBookDBContext;
            }
        }
        // Implementation of methods from IBookRepository
    }
}