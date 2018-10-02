using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class reminderOrСommentsRepository : IRepository<reminderOrСomments>
    {
        private BESTDBModel db;
        public reminderOrСommentsRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<reminderOrСomments> GetAll() => db.reminderOrСomments;
        public reminderOrСomments Get(int id) => db.reminderOrСomments.Find(id);
        public void Create(ref reminderOrСomments item) => db.reminderOrСomments.Add(item);

        public void Update(reminderOrСomments item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<reminderOrСomments> Find(Func<reminderOrСomments, bool> predicate) => db.reminderOrСomments.Where(predicate);
        public void Delete(int id)
        {
            reminderOrСomments reminderOrСomments_ = db.reminderOrСomments.Find(id);
            if (reminderOrСomments_ != null)
                db.reminderOrСomments.Remove(reminderOrСomments_);
        }
    }
}
