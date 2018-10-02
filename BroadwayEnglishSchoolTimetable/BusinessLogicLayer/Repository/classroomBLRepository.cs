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
    class classroomBLRepository : IRepositoryBL<classroomBL>
    {
        IUnitOfWork Database { get; set; }
        public classroomBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<classroomBL> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<classroom, classroomBL>()).CreateMapper();
            return mapper.Map<IEnumerable<classroom>, List<classroomBL>>(Database.Classrooms.GetAll());
        }
        public int Add(classroomBL el)
        {
            try
            {
                el.name = el.name.Trim();
                if (Database.Classrooms.Find(p => p.name == el.name).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<classroomBL, classroom>()).CreateMapper();
                    var eladd = mapper.Map<classroomBL, classroom>(el);
                    Database.Classrooms.Create(ref eladd);
                    Database.Save();
                    return eladd.Id;
                }
            }
            catch {  }
            return 0;
        }

        public classroomBL Get(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<classroom, classroomBL>()).CreateMapper();
            return mapper.Map<classroom, classroomBL>(Database.Classrooms.Get(id));
        }
        public classroomBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(classroomBL El)
        {
            El.name = El.name.Trim();
            if (Database.Classrooms.Find(p => p.name == El.name && p.Id != El.Id).FirstOrDefault() == null)
            {
                var el = Database.Classrooms.Get(El.Id);
                if (el == null)
                    return 2;
                el.name = El.name;
                el.disable = El.disable;
                Database.Classrooms.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public void Delete(int id)
        {
            Database.Classrooms.Delete(id);
            Database.Save();
        }

        public classroomBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<classroomBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
