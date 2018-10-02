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
    class causeOfDisruptionBLRepository : IRepositoryBL<causeOfDisruptionBL>
    {
        IUnitOfWork Database { get; set; }
        public causeOfDisruptionBLRepository(ref IUnitOfWork uow) => Database = uow;
        public IEnumerable<causeOfDisruptionBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruption, causeOfDisruptionBL>()).CreateMapper().Map<IEnumerable<causeOfDisruption>, List<causeOfDisruptionBL>>(Database.CauseOfDisruptions.GetAll());
        public int Add(causeOfDisruptionBL el)
        {
            try
            {
                el.cause = el.cause.Trim();
                if (Database.CauseOfDisruptions.Find(p => p.cause == el.cause).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruptionBL, causeOfDisruption>()).CreateMapper();
                    var eladd = mapper.Map<causeOfDisruptionBL, causeOfDisruption>(el);
                    Database.CauseOfDisruptions.Create(ref eladd);
                    Database.Save();
                    return eladd.Id;
                }
            }
            catch { }
            return 0;
        }
        public causeOfDisruptionBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruption, causeOfDisruptionBL>()).CreateMapper().Map<causeOfDisruption, causeOfDisruptionBL>(Database.CauseOfDisruptions.Get(id));
        public causeOfDisruptionBL GetFullInfo(int id) => throw new NotImplementedException();
        public int Update(causeOfDisruptionBL El)
        {
            El.cause = El.cause.Trim();
            if (Database.CauseOfDisruptions.Find(p => p.cause == El.cause && p.Id != El.Id).FirstOrDefault() == null)
            {
                var el = Database.CauseOfDisruptions.Get(El.Id);
                if (el == null)
                    return 2;
                el.cause = El.cause;
                el.disable = El.disable;
                Database.CauseOfDisruptions.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public void Delete(int id)
        {
            Database.CauseOfDisruptions.Delete(id);
            Database.Save();
        }
        public causeOfDisruptionBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<causeOfDisruptionBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
