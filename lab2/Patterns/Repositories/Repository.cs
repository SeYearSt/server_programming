using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Lab2.Patterns.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;
        }
        public void Add(T item)
        {
            this.context.Set<T>().Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            this.context.Set<T>().AddRange(items);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.context.Set<T>().Where(predicate);
        }

        public T Get(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.context.Set<T>().ToList();
        }

        public IQueryable<T> Queryable()
        {
            return this.context.Set<T>();
        }

        public void Remove(T item)
        {
            this.context.Set<T>().Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            this.context.Set<T>().RemoveRange(items);
        }
    }
}