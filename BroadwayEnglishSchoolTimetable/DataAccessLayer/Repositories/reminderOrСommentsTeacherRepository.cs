using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    class reminderOrСommentsTeacherRepository : IRepository<reminderOrСommentsTeacher>
    {
        private BESTDBModel db;
        public reminderOrСommentsTeacherRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<reminderOrСommentsTeacher> GetAll() => db.reminderOrСommentsTeacher;
        public reminderOrСommentsTeacher Get(int id) => db.reminderOrСommentsTeacher.Find(id);
        public void Create(ref reminderOrСommentsTeacher item) => db.reminderOrСommentsTeacher.Add(item);

        public void Update(reminderOrСommentsTeacher item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<reminderOrСommentsTeacher> Find(Func<reminderOrСommentsTeacher, bool> predicate) => db.reminderOrСommentsTeacher.Where(predicate);
        public void Delete(int id)
        {
            reminderOrСommentsTeacher reminderOrСommentsTeacher_ = db.reminderOrСommentsTeacher.Find(id);
            if (reminderOrСommentsTeacher_ != null)
                db.reminderOrСommentsTeacher.Remove(reminderOrСommentsTeacher_);
        }
    }
}
