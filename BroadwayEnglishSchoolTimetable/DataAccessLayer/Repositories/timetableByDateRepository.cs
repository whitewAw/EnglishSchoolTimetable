using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class timetableByDateRepository : IRepository<timetableByDate>
    {
        private BESTDBModel db;
        public timetableByDateRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<timetableByDate> GetAll() => db.timetableByDates;
        public timetableByDate Get(int id) => db.timetableByDates.Find(id);
        public void Create(ref timetableByDate item) => db.timetableByDates.Add(item);
        public void Update(timetableByDate item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<timetableByDate> Find(Func<timetableByDate, bool> predicate) => db.timetableByDates.Where(predicate);
        public void Delete(int id)
        {
            timetableByDate timetableByDate_ = db.timetableByDates.Find(id);
            if (timetableByDate_ != null)
                db.timetableByDates.Remove(timetableByDate_);
        }
    }
}
