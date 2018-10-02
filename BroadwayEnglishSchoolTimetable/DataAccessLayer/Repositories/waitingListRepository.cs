using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class waitingListRepository : IRepository<waitingList>
    {
        private BESTDBModel db;
        public waitingListRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<waitingList> GetAll() => db.waitingLists;
        public waitingList Get(int id) => db.waitingLists.Find(id);
        public void Create(ref waitingList item) => db.waitingLists.Add(item);
        public void Update(waitingList item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<waitingList> Find(Func<waitingList, bool> predicate) => db.waitingLists.Where(predicate);
        public void Delete(int id)
        {
            waitingList waitingList_ = db.waitingLists.Find(id);
            if (waitingList_ != null)
                db.waitingLists.Remove(waitingList_);
        }
    }
}
