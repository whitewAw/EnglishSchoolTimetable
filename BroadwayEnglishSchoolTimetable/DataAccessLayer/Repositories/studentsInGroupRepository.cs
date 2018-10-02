using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class studentsInGroupRepository : IRepository<studentsInGroup>
    {
        private BESTDBModel db;
        public studentsInGroupRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<studentsInGroup> GetAll() => db.studentsInGroups;
        public studentsInGroup Get(int id) => db.studentsInGroups.Find(id);
        public void Create(ref studentsInGroup item) => db.studentsInGroups.Add(item);
        public void Update(studentsInGroup item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<studentsInGroup> Find(Func<studentsInGroup, bool> predicate) => db.studentsInGroups.Where(predicate);
        public void Delete(int id)
        {
            studentsInGroup studentsInGroup_ = db.studentsInGroups.Find(id);
            if (studentsInGroup_ != null)
                db.studentsInGroups.Remove(studentsInGroup_);
        }
    }
}
