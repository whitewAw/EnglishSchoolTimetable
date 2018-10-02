using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class parentsOfStudentRepository : IRepository<parentsOfStudent>
    {
        private BESTDBModel db;
        public parentsOfStudentRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<parentsOfStudent> GetAll() => db.parentsOfStudents;
        public parentsOfStudent Get(int id) => db.parentsOfStudents.Find(id);
        public void Create(ref parentsOfStudent item) => db.parentsOfStudents.Add(item);
        public void Update(parentsOfStudent item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<parentsOfStudent> Find(Func<parentsOfStudent, bool> predicate) => db.parentsOfStudents.Where(predicate);
        public void Delete(int id)
        {
            parentsOfStudent parentsOfStudent_ = db.parentsOfStudents.Find(id);
            if (parentsOfStudent_ != null)
                db.parentsOfStudents.Remove(parentsOfStudent_);
        }
    }
}
