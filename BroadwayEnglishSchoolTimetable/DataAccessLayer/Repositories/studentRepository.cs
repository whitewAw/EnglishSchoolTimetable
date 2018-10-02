using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class studentRepository : IRepository<student>
    {
        private BESTDBModel db;
        public studentRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<student> GetAll() => db.students;
        public student Get(int id) => db.students.Find(id);
        public void Create(ref student item) => db.students.Add(item);
        public void Update(student item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<student> Find(Func<student, bool> predicate) => db.students.Where(predicate);
        public void Delete(int id)
        {
            student student_ = db.students.Find(id);
            if (student_ != null)
                db.students.Remove(student_);
        }
    }
}
