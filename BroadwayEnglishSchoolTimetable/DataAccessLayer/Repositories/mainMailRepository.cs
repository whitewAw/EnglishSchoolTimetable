using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class mainMailRepository : IRepository<mainMail>
    {
        private BESTDBModel db;
        public mainMailRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<mainMail> GetAll() => db.mainMails;
        public mainMail Get(int id) => db.mainMails.Find(id);
        public void Create(ref mainMail item) => db.mainMails.Add(item);
        public void Update(mainMail item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<mainMail> Find(Func<mainMail, bool> predicate) => db.mainMails.Where(predicate);
        public void Delete(int id)
        {
            mainMail mainMail_ = db.mainMails.Find(id);
            if (mainMail_ != null)
                db.mainMails.Remove(mainMail_);
        }
    }
}
