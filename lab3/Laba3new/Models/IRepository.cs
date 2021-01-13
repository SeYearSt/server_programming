using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3new.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetBook(int id);
        void Create(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
