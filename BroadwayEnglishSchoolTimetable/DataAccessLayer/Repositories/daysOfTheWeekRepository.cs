using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class daysOfTheWeekRepository : IRepository<daysOfTheWeek>
    {
        private BESTDBModel db;
        public daysOfTheWeekRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<daysOfTheWeek> GetAll() => db.daysOfTheWeeks;
        public daysOfTheWeek Get(int id) => db.daysOfTheWeeks.Find(id);
        public void Create(ref daysOfTheWeek item) => db.daysOfTheWeeks.Add(item);
        public void Update(daysOfTheWeek item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<daysOfTheWeek> Find(Func<daysOfTheWeek, bool> predicate) => db.daysOfTheWeeks.Where(predicate);
        public void Delete(int id)
        {
            daysOfTheWeek daysOfTheWeek_ = db.daysOfTheWeeks.Find(id);
            if (daysOfTheWeek_ != null)
                db.daysOfTheWeeks.Remove(daysOfTheWeek_);
        }
    }
}
