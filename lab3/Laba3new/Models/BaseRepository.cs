using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Laba3new.Models
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        BookSage db;
        IDbSet<T> dbSet;

        public BaseRepository(BookSage db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Create(T obj)
        {
            dbSet.Add(obj);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            T book = dbSet.Find(id);
            /* Book book = db.Books.Find(id);
             if (book != null)
                 db.Books.Remove(book);*/
            dbSet.Remove(book);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
        public T GetBook(int id)
        {
            return dbSet.Find(id);
        }
    }
}
