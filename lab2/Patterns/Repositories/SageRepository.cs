using Lab2.Models;

namespace Lab2.Patterns.Repositories
{
    public class SageRepository : Repository<Sage>, ISageRepository
    {
        public SageRepository(SageBookDBContext context)
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
        // Implementation of methods from ISageRepository
    }
}