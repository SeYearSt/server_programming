namespace Lab2.Patterns.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        System.Collections.Generic.IEnumerable<T> GetAll();
        System.Collections.Generic.IEnumerable<T> Find(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate);
        System.Linq.IQueryable<T> Queryable();

        void Add(T item);
        void AddRange(System.Collections.Generic.IEnumerable<T> items);

        void Remove(T item);
        void RemoveRange(System.Collections.Generic.IEnumerable<T> items);

    }
}
