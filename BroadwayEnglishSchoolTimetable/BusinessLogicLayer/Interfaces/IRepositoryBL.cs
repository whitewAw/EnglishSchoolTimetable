using BusinessLogicLayer.Repository;
using System;
using System.Collections.Generic;


namespace BusinessLogicLayer.Interfaces
{
    public interface IRepositoryBL<T> where T : class
    {
        IEnumerable<T> GetAll();
        int Add(T el);
        T Get(int id);
        T GetFullInfo(int id);
        int Update(T El);
        void Delete(int id);
        T GetEmpty();
        IEnumerable<T> TakeBetweenDates(DateTime? start, DateTime? finish);
    }
}

