using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class teacherRepository : IRepository<teacher>
    {
        private BESTDBModel db;
        public teacherRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<teacher> GetAll() => db.teachers;
        public teacher Get(int id) => db.teachers.Find(id);
        public void Create(ref teacher item) => db.teachers.Add(item);
        public void Update(teacher item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<teacher> Find(Func<teacher, bool> predicate) => db.teachers.Where(predicate);
        public void Delete(int id)
        {
            teacher teacher_ = db.teachers.Find(id);
            if (teacher_ != null)
                db.teachers.Remove(teacher_);
        }
    }
}
