using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class classroomRepository : IRepository<classroom>
    {
        private BESTDBModel db;
        public classroomRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<classroom> GetAll() => db.classrooms;
        public classroom Get(int id) => db.classrooms.Find(id);
        public void Create(ref classroom item) => db.classrooms.Add(item);
        public void Update(classroom item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<classroom> Find(Func<classroom, bool> predicate) => db.classrooms.Where(predicate);
        public void Delete(int id)
        {
            classroom classroom_ = db.classrooms.Find(id);
            if (classroom_ != null)
                db.classrooms.Remove(classroom_);
        }
    }
}
