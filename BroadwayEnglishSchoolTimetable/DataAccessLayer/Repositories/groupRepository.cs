using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class groupRepository : IRepository<group>
    {
        private BESTDBModel db;
        public groupRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<group> GetAll() => db.groups;
        public group Get(int id) => db.groups.Find(id);
        public void Create(ref group item) => db.groups.Add(item);
        public void Update(group item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<group> Find(Func<group, bool> predicate) => db.groups.Where(predicate);
        public void Delete(int id)
        {
            group group_ = db.groups.Find(id);
            if (group_ != null)
                db.groups.Remove(group_);
        }
    }
}
