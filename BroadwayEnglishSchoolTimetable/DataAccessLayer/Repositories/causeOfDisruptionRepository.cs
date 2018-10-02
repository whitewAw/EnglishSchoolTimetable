using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class causeOfDisruptionRepository : IRepository<causeOfDisruption>
    {
        private BESTDBModel db;
        public causeOfDisruptionRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<causeOfDisruption> GetAll() => db.causeOfDisruptions;
        public causeOfDisruption Get(int id) => db.causeOfDisruptions.Find(id);
        public void Create(ref causeOfDisruption item) => db.causeOfDisruptions.Add(item);
        public void Update(causeOfDisruption item) => db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        public IEnumerable<causeOfDisruption> Find(Func<causeOfDisruption, bool> predicate) => db.causeOfDisruptions.Where(predicate);
        public void Delete(int id)
        {
            causeOfDisruption causeOfDisruption_ = db.causeOfDisruptions.Find(id);
            if (causeOfDisruption_ != null)
                db.causeOfDisruptions.Remove(causeOfDisruption_);
        }
    }
}
