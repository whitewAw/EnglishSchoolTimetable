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
    class formOfEducationBLRepository : IRepositoryBL<formOfEducationBL>
    {
        IUnitOfWork Database { get; set; }
        public formOfEducationBLRepository(ref IUnitOfWork uow) => Database = uow;
        public int Add(formOfEducationBL el)
        {
            try
            {
                el.form = el.form.Trim();
                if (Database.FormOfEducations.Find(p => p.form == el.form).FirstOrDefault() == null)
                {
                    var elDL = new MapperConfiguration(cfg => cfg.CreateMap<formOfEducationBL, formOfEducation>()).CreateMapper().Map<formOfEducationBL, formOfEducation>(el);
                    Database.FormOfEducations.Create(ref elDL);
                    Database.Save();
                    return elDL.Id;
                }
            }
            catch { }
            return 0;
        }

        public void Delete(int id)
        {
            Database.FormOfEducations.Delete(id);
            Database.Save();
        }

        public formOfEducationBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<formOfEducation, formOfEducationBL>()).CreateMapper().Map<formOfEducation, formOfEducationBL>(Database.FormOfEducations.Get(id));

        public IEnumerable<formOfEducationBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<formOfEducation, formOfEducationBL>()).CreateMapper().Map<IEnumerable<formOfEducation>, List<formOfEducationBL>>(Database.FormOfEducations.GetAll());

        public formOfEducationBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(formOfEducationBL El)
        {
            El.form = El.form.Trim();
            if (Database.FormOfEducations.Find(p => p.form == El.form && p.Id != El.Id).FirstOrDefault() == null)
            {
                var el = Database.FormOfEducations.Get(El.Id);
                if (el == null)
                    return 2;
                el.form = El.form;
                el.disable = El.disable;
                Database.FormOfEducations.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public formOfEducationBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<formOfEducationBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
