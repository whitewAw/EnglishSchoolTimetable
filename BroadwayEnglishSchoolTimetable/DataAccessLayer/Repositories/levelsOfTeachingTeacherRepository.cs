using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    class levelsOfTeachingTeacherRepository : IRepository<levelsOfTeachingTeacher>
    {
        private BESTDBModel db;
        public levelsOfTeachingTeacherRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<levelsOfTeachingTeacher> GetAll() => db.levelsOfTeachingTeachers;
        public levelsOfTeachingTeacher Get(int id) => db.levelsOfTeachingTeachers.Find(id);
        public void Create(ref levelsOfTeachingTeacher item) => db.levelsOfTeachingTeachers.Add(item);
        public void Update(levelsOfTeachingTeacher item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<levelsOfTeachingTeacher> Find(Func<levelsOfTeachingTeacher, bool> predicate) => db.levelsOfTeachingTeachers.Where(predicate);
        public void Delete(int id)
        {
            levelsOfTeachingTeacher levelsOfTeachingTeacher_ = db.levelsOfTeachingTeachers.Find(id);
            if (levelsOfTeachingTeacher_ != null)
                db.levelsOfTeachingTeachers.Remove(levelsOfTeachingTeacher_);
        }
    }
}
