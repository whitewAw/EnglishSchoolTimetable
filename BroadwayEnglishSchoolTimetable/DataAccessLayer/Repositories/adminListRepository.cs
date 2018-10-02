using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    class adminListRepository : IRepository<AdminList>
    {
        private BESTDBModel db;
        public adminListRepository(BESTDBModel context) => this.db = context;
        public IEnumerable<AdminList> GetAll() => db.AdminLists;
        public AdminList Get(int id) => db.AdminLists.Find(id);
        public void Create(ref AdminList item) => db.AdminLists.Add(item);
        public void Update(AdminList item) => db.Entry(item).State = EntityState.Modified;
        public IEnumerable<AdminList> Find(Func<AdminList, bool> predicate) => db.AdminLists.Where(predicate);
        public void Delete(int id)
        {
            AdminList adminList = db.AdminLists.Find(id);
            if (adminList != null)
                db.AdminLists.Remove(adminList);
        }
    }
}
