using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class accessibilityOfTeacherRepository : IRepository<accessibilityOfTeacher>
    {
        private BESTDBModel db;
        public accessibilityOfTeacherRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<accessibilityOfTeacher> GetAll() => db.accessibilityOfTeachers;
        public accessibilityOfTeacher Get(int id) => db.accessibilityOfTeachers.Find(id);
        public void Create(ref accessibilityOfTeacher item) => db.accessibilityOfTeachers.Add(item);
        public void Update(accessibilityOfTeacher item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<accessibilityOfTeacher> Find(Func<accessibilityOfTeacher, bool> predicate) => db.accessibilityOfTeachers.Where(predicate);
        public void Delete(int id)
        {
            accessibilityOfTeacher accessibilityOfTeacher_ = db.accessibilityOfTeachers.Find(id);
            if (accessibilityOfTeacher_ != null)
                db.accessibilityOfTeachers.Remove(accessibilityOfTeacher_);
        }
    }
}
