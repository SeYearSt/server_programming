using System;
using Lab2.Patterns.Repositories;
using Lab2.Models;

namespace Lab2.Patterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SageBookDBContext _context;
        public IBookRepository Books { get; private set; }
        public ISageRepository Sages { get; private set; }

        public UnitOfWork(SageBookDBContext context)
        {
            this._context = context;
            this.Books = new BookRepository(this._context);
            this.Sages = new SageRepository(this._context);
        }
        
        public int Complete()
        {
            return this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}