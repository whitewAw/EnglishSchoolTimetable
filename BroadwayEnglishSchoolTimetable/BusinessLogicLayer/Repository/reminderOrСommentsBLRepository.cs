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
    class reminderOrСommentsBLRepository : IRepositoryBL<reminderOrСommentsBL>
    {
        IUnitOfWork Database { get; set; }
        public reminderOrСommentsBLRepository(ref IUnitOfWork uow) => Database = uow;
        public IEnumerable<reminderOrСommentsBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСomments, reminderOrСommentsBL>()
        .ForMember("AdminName", opt => opt.MapFrom(c => c.AdminList.surname+" "+c.AdminList.name))
        //.ForMember("idAdmin", opt => opt.MapFrom(c => 0))
        .ForMember("idAdminStr", opt => opt.MapFrom(c => c.AdminList.IdAdmin))
        ).CreateMapper().Map<IEnumerable<reminderOrСomments>, List<reminderOrСommentsBL>>(Database.ReminderOrСomments.GetAll());
        public int Add(reminderOrСommentsBL el)
        {
            try
            {
                el.date = new DateTime(el.datePL.Year, el.datePL.Month, el.datePL.Day, el.timePL.Hours, el.timePL.Minutes, 0);
                //el.idAdmin = Database.AdminLists.GetAll().Where(p => p.IdAdmin == el.idAdminStr).Select(p=>p.Id).FirstOrDefault();
                el.idAdmin = Database.AdminLists.Find(p => p.IdAdmin == el.idAdminStr).Select(p => p.Id).FirstOrDefault();
                var eladd = new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСommentsBL, reminderOrСomments>()).CreateMapper().Map<reminderOrСommentsBL, reminderOrСomments>(el);
                Database.ReminderOrСomments.Create(ref eladd);
                Database.Save();
                return eladd.Id;
            }
            catch { return 0; }
        }
        public reminderOrСommentsBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСomments, reminderOrСommentsBL>()
                                                             .ForMember("AdminName", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))
                                                             //.ForMember("idAdmin", opt => opt.MapFrom(c => 0))
                                                             .ForMember("idAdminStr", opt => opt.MapFrom(c => c.AdminList.IdAdmin))
                                                             .ForMember("datePL", opt => opt.MapFrom(c => new DateTime(c.date.Year, c.date.Month, c.date.Day)))
                                                             .ForMember("timePL", opt => opt.MapFrom(c => new TimeSpan(c.date.Hour, c.date.Minute, 0)))
            ).CreateMapper().Map<reminderOrСomments, reminderOrСommentsBL>(Database.ReminderOrСomments.Get(id));

        public reminderOrСommentsBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(reminderOrСommentsBL El)
        {
            var el = Database.ReminderOrСomments.Get(El.Id);
            if (el == null)
                return 2;
            el.date = new DateTime(El.datePL.Year, El.datePL.Month, El.datePL.Day, El.timePL.Hours, El.timePL.Minutes, 0);
            el.notes = El.notes;
            if (El.idAdminStr == "admin")
                el.idAdmin = El.idAdmin;
            Database.ReminderOrСomments.Update(el);
            Database.Save();
            return 1;
        }

        public void Delete(int id)
        {
            Database.ReminderOrСomments.Delete(id);
            Database.Save();
        }

        public reminderOrСommentsBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<reminderOrСommentsBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
