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
    public class waitingListBLRepository : IRepositoryBL<waitingListBL>
    {
        IUnitOfWork Database { get; set; }
        public waitingListBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<waitingListBL> GetAll()
        {
            var mapstud = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
            .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
            .ForMember("Parent", opt => opt.MapFrom(c => c.idParent == null ? null : c.parentsOfStudent.surname + " " + c.parentsOfStudent.name + " " + c.parentsOfStudent.patronymic))
            .ForMember("FormOfEducation", opt => opt.MapFrom(c => c.formOfEducation.form))
            .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idEducationalMaterials == null ? null : c.educationalMaterial.name))
            .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.idLevelOfEnglish == null ? null : c.levelOfEnglish.level))
            ).CreateMapper();

            return new MapperConfiguration(cfg => cfg.CreateMap<waitingList, waitingListBL>()
                          .ForMember("studentEl", opt => opt.MapFrom(c => mapstud.Map<student, studentBL>(c.student)))
                          .ForMember("adminEl", opt => opt.MapFrom(c => c.AdminList))
            ).CreateMapper().Map<IEnumerable<waitingList>, List<waitingListBL>>(Database.WaitingLists.GetAll());
        }

        public int Add(waitingListBL el)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<waitingListBL, waitingList>()).CreateMapper();
                var elAdd = mapper.Map<waitingListBL, waitingList>(el);
                Database.WaitingLists.Create(ref elAdd);
                Database.Save();
                return elAdd.Id;
            }
            catch { return 0; }
        }

        public waitingListBL Get(int id)
        {
            var mapstud = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
            .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
            .ForMember("Parent", opt => opt.MapFrom(c => c.idParent == null ? null : c.parentsOfStudent.surname + " " + c.parentsOfStudent.name + " " + c.parentsOfStudent.patronymic))
            .ForMember("FormOfEducation", opt => opt.MapFrom(c => c.formOfEducation.form))
            .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idEducationalMaterials == null ? null : c.educationalMaterial.name))
            .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.idLevelOfEnglish == null ? null : c.levelOfEnglish.level))
            ).CreateMapper();


            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<waitingList, waitingListBL>()
              .ForMember("studentEl", opt => opt.MapFrom(c => mapstud.Map<student, studentBL>(c.student)))
              .ForMember("adminEl", opt => opt.MapFrom(c => c.AdminList))).CreateMapper();
            return mapper.Map<waitingList, waitingListBL>(Database.WaitingLists.Get(id));
        }
        public waitingListBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(waitingListBL El)
        {
            var el = Database.WaitingLists.Get(El.Id);
            if (el == null)
                return 2;
            el.idStudents = El.idStudents;
            el.dateTarget_ = El.dateTarget_;
            el.notes = El.notes;
            el.idAdmin = El.idAdmin;
            Database.WaitingLists.Update(el);
            Database.Save();
            return 1;
        }
        public void Delete(int id)
        {
            Database.WaitingLists.Delete(id);
            Database.Save();
        }

        public waitingListBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<waitingListBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
