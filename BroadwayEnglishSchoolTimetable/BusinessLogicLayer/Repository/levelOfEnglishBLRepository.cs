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
    class levelOfEnglishBLRepository : IRepositoryBL<levelOfEnglishBL>
    {
        IUnitOfWork Database { get; set; }
        public levelOfEnglishBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<levelOfEnglishBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<levelOfEnglish, levelOfEnglishBL>()).CreateMapper().Map<IEnumerable<levelOfEnglish>, List<levelOfEnglishBL>>(Database.LevelOfEnglishes.GetAll());

        public int Add(levelOfEnglishBL el)
        {
            try
            {
                el.level = el.level.Trim();
                if (Database.LevelOfEnglishes.Find(p => p.level == el.level).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<levelOfEnglishBL, levelOfEnglish>()).CreateMapper();
                    var eladd = mapper.Map<levelOfEnglishBL, levelOfEnglish>(el);
                    Database.LevelOfEnglishes.Create(ref eladd);
                    Database.Save();
                    return eladd.Id;
                }
            }
            catch {  }
            return 0;
        }

        public levelOfEnglishBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<levelOfEnglish, levelOfEnglishBL>()).CreateMapper().Map<levelOfEnglish, levelOfEnglishBL>(Database.LevelOfEnglishes.Get(id));

        public levelOfEnglishBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(levelOfEnglishBL El)
        {
            El.level = El.level.Trim();
            if (Database.LevelOfEnglishes.Find(p => p.level == El.level && p.Id!= El.Id).FirstOrDefault() == null)
            {
                var el = Database.LevelOfEnglishes.Get(El.Id);
                if (el == null)
                    return 2;
                el.level = El.level;
                el.disable = El.disable;
                Database.LevelOfEnglishes.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public void Delete(int id)
        {
            Database.LevelOfEnglishes.Delete(id);
            Database.Save();
        }

        public levelOfEnglishBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<levelOfEnglishBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
