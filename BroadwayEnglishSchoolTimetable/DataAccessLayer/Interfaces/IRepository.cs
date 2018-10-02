using System;
using System.Collections.Generic;
namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(ref T item);
        void Update(T item);
        void Delete(int id);
    }
}
