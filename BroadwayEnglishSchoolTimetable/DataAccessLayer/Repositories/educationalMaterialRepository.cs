using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class educationalMaterialRepository : IRepository<educationalMaterial>
    {
        private BESTDBModel db;
        public educationalMaterialRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<educationalMaterial> GetAll() => db.educationalMaterials;
        public educationalMaterial Get(int id) => db.educationalMaterials.Find(id);
        public void Create(ref educationalMaterial item) => db.educationalMaterials.Add(item);
        public void Update(educationalMaterial item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<educationalMaterial> Find(Func<educationalMaterial, bool> predicate) => db.educationalMaterials.Where(predicate);
        public void Delete(int id)
        {
            educationalMaterial educationalMaterial_ = db.educationalMaterials.Find(id);
            if (educationalMaterial_ != null)
                db.educationalMaterials.Remove(educationalMaterial_);
        }
    }
}
