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
    class daysOfTheWeekBLRepository : IRepositoryBL<daysOfTheWeekBL>
    {
        IUnitOfWork Database { get; set; }
        public daysOfTheWeekBLRepository(ref IUnitOfWork uow) => Database = uow;
        public int Add(daysOfTheWeekBL el)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<daysOfTheWeekBL, daysOfTheWeek>()).CreateMapper();
            //    var eladd = mapper.Map<daysOfTheWeekBL, daysOfTheWeek>(el);
            //    Database.daysOfTheWeeks.Create(ref eladd);
            //    Database.Save();
            //    return eladd.Id;
            //}
            //catch { return 0; }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
            //Database.daysOfTheWeeks.Delete(id);
            //Database.Save();
        }

        public daysOfTheWeekBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<daysOfTheWeek, daysOfTheWeekBL>()).CreateMapper().Map<daysOfTheWeek, daysOfTheWeekBL>(Database.DaysOfTheWeeks.Get(id));

        public IEnumerable<daysOfTheWeekBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<daysOfTheWeek, daysOfTheWeekBL>()).CreateMapper().Map<IEnumerable<daysOfTheWeek>, List<daysOfTheWeekBL>>(Database.DaysOfTheWeeks.GetAll());

        public daysOfTheWeekBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(daysOfTheWeekBL El)
        {
            El.day = El.day.Trim();
            if (Database.DaysOfTheWeeks.Find(p => p.day == El.day && p.Id != El.Id).FirstOrDefault() == null)
            {
                var el = Database.DaysOfTheWeeks.Get(El.Id);
                if (el == null)
                    return 2;
                el.day = El.day;
                Database.DaysOfTheWeeks.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public daysOfTheWeekBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<daysOfTheWeekBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
