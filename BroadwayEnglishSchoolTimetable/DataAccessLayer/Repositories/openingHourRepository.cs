using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class openingHourRepository : IRepository<openingHour>
    {
        private BESTDBModel db;
        public openingHourRepository(BESTDBModel context) => this.db = context;

        public void Create(ref openingHour item) => db.openingHours.Add(item);

        public void Delete(int id)
        {
            openingHour openingHour_ = db.openingHours.Find(id);
            if (openingHour_ != null)
                db.openingHours.Remove(openingHour_);
        }

        public IEnumerable<openingHour> Find(Func<openingHour, bool> predicate) => db.openingHours.Where(predicate);

        public openingHour Get(int id) => db.openingHours.Find(id);

        public IEnumerable<openingHour> GetAll() => db.openingHours;

        public void Update(openingHour item) => db.Entry(item).State = EntityState.Modified;
    }
}
