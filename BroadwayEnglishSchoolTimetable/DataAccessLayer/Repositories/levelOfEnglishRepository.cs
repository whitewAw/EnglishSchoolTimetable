using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class levelOfEnglishRepository : IRepository<levelOfEnglish>
    {
        private BESTDBModel db;
        public levelOfEnglishRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<levelOfEnglish> GetAll() => db.levelOfEnglishes;
        public levelOfEnglish Get(int id) => db.levelOfEnglishes.Find(id);
        public void Create(ref levelOfEnglish item) => db.levelOfEnglishes.Add(item);
        public void Update(levelOfEnglish item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<levelOfEnglish> Find(Func<levelOfEnglish, bool> predicate) => db.levelOfEnglishes.Where(predicate);
        public void Delete(int id)
        {
            levelOfEnglish levelOfEnglish_ = db.levelOfEnglishes.Find(id);
            if (levelOfEnglish_ != null)
                db.levelOfEnglishes.Remove(levelOfEnglish_);
        }
    }
}
