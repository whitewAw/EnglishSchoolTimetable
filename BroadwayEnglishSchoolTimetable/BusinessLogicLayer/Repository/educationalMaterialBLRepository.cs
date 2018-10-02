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
    class educationalMaterialBLRepository: IRepositoryBL<educationalMaterialBL>
    {
        IUnitOfWork Database { get; set; }
        public educationalMaterialBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<educationalMaterialBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<educationalMaterial, educationalMaterialBL>()).CreateMapper().Map<IEnumerable<educationalMaterial>, List<educationalMaterialBL>>(Database.EducationalMaterials.GetAll());

        public int Add(educationalMaterialBL el)
        {
            try
            {
                el.name = el.name.Trim();
                if (Database.EducationalMaterials.Find(p => p.name == el.name).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<educationalMaterialBL, educationalMaterial>()).CreateMapper();
                    var elout = mapper.Map<educationalMaterialBL, educationalMaterial>(el);
                    Database.EducationalMaterials.Create(ref elout);
                    Database.Save();
                    return elout.Id;
                }
            }
            catch { }
            return 0;
        }

        public educationalMaterialBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<educationalMaterial, educationalMaterialBL>()).CreateMapper().Map<educationalMaterial, educationalMaterialBL>(Database.EducationalMaterials.Get(id));

        public educationalMaterialBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(educationalMaterialBL El)
        {
            El.name = El.name.Trim();
            if (Database.EducationalMaterials.Find(p => p.name == El.name && p.Id != El.Id).FirstOrDefault() == null)
            {
                var el = Database.EducationalMaterials.Get(El.Id);
                if (el == null)
                    return 2;
                el.name = El.name;
                el.disable = El.disable;
                Database.EducationalMaterials.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public void Delete(int id)
        {
            Database.EducationalMaterials.Delete(id);
            Database.Save();
        }

        public educationalMaterialBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<educationalMaterialBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
