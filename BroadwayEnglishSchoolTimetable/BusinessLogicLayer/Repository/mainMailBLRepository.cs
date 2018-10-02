using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Repository
{
    class mainMailBLRepository : IRepositoryBL<mainMailBL>
    {
        IUnitOfWork Database { get; set; }
        public mainMailBLRepository(ref IUnitOfWork uow) => Database = uow;

        public int Add(mainMailBL el) => throw new NotImplementedException();

        public void Delete(int i) => throw new NotImplementedException();

        public mainMailBL Get(int id = 0) => new MapperConfiguration(cfg => cfg.CreateMap<mainMail, mainMailBL>().ForMember("password", opt => opt.MapFrom(c => ""))).CreateMapper().Map<mainMail, mainMailBL>(Database.MainMail.GetAll().LastOrDefault());

        public IEnumerable<mainMailBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<mainMail, mainMailBL>().ForMember("password", opt => opt.MapFrom(c => ""))).CreateMapper().Map<IEnumerable<mainMail>, List<mainMailBL>>(Database.MainMail.GetAll());

        public mainMailBL GetFullInfo(int id = 0) => new MapperConfiguration(cfg => cfg.CreateMap<mainMail, mainMailBL>()).CreateMapper().Map<mainMail, mainMailBL>(Database.MainMail.GetAll().LastOrDefault());

        public int Update(mainMailBL AdminEl)
        {
            AdminEl.mail = AdminEl.mail.Trim();
            AdminEl.password = AdminEl.password.Trim();
            var oldel = Database.MainMail.GetAll().LastOrDefault();
            if(oldel==null)
            {
                var el = new mainMail { mail = AdminEl.mail, password = AdminEl.password };
                Database.MainMail.Create(ref el);
                return 1;
            }
    
            if (oldel.mail == AdminEl.mail)
            {
                oldel.password = AdminEl.password;
                Database.MainMail.Update(oldel);
            }
            else
            {
                var el = new mainMail { mail = AdminEl.mail, password = AdminEl.password };
                Database.MainMail.Create(ref el);
            }
            Database.Save();
            return 1;
        }

        public mainMailBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<mainMailBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
