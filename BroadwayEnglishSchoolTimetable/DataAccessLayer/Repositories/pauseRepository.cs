using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class pauseRepository : IRepository<pause>
    {
        private BESTDBModel db;
        public pauseRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<pause> GetAll() => db.pauses;
        public pause Get(int id) => db.pauses.Find(id);
        public void Create(ref pause item) => db.pauses.Add(item);
        public void Update(pause item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<pause> Find(Func<pause, bool> predicate) => db.pauses.Where(predicate);
        public void Delete(int id)
        {
            pause pause_ = db.pauses.Find(id);
            if (pause_ != null)
                db.pauses.Remove(pause_);
        }
    }
}
