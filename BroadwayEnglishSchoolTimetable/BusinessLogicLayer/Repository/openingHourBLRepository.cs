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
    class openingHourBLRepository : IRepositoryBL<openingHourBL>
    {
        IUnitOfWork Database { get; set; }
        public openingHourBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<openingHourBL> GetAll() => throw new NotImplementedException();

        public int Add(openingHourBL el) => throw new NotImplementedException();

        public openingHourBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<openingHour, openingHourBL>()).CreateMapper().Map<openingHour, openingHourBL>(Database.OpeningHour.GetAll().FirstOrDefault());

        public openingHourBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(openingHourBL El)
        {
            if (El.close > El.open)
            {
                var el = Database.OpeningHour.GetAll().FirstOrDefault();
                if (el == null)
                    return 2;
                el.open = El.open;
                el.close = El.close;
                Database.OpeningHour.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public void Delete(int id) => throw new NotImplementedException();

        public openingHourBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<openingHourBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
