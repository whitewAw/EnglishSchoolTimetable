using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class formOfEducationRepository : IRepository<formOfEducation>
    {
        private BESTDBModel db;
        public formOfEducationRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<formOfEducation> GetAll() => db.formOfEducations;
        public formOfEducation Get(int id) => db.formOfEducations.Find(id);
        public void Create(ref formOfEducation item) => db.formOfEducations.Add(item);
        public void Update(formOfEducation item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<formOfEducation> Find(Func<formOfEducation, bool> predicate) => db.formOfEducations.Where(predicate);
        public void Delete(int id)
        {
            formOfEducation formOfEducation_ = db.formOfEducations.Find(id);
            if (formOfEducation_ != null)
                db.formOfEducations.Remove(formOfEducation_);
        }
    }
}
