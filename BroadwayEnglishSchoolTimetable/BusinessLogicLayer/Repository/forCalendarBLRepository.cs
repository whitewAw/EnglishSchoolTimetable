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
    class forCalendarBLRepository : IRepositoryBL<forCalendarBL>
    {
        IUnitOfWork Database { get; set; }
        public forCalendarBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<forCalendarBL> GetAll() => throw new NotImplementedException();

        public int Add(forCalendarBL el) => throw new NotImplementedException();

        public forCalendarBL Get(int id)
        {
            if (id == 0)
            {
                var el = new forCalendarBL();
                el.openingHour = new MapperConfiguration(cfg => cfg.CreateMap<openingHour, openingHourBL>()).CreateMapper().Map<openingHour, openingHourBL>(Database.OpeningHour.GetAll().FirstOrDefault());



                el.reminderOrСomments = new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСomments, reminderOrСommentsBL>()
                                        .ForMember("AdminName", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))
                                        .ForMember("idAdmin", opt => opt.MapFrom(c => c.AdminList.Id))
                                        .ForMember("idAdminStr", opt => opt.MapFrom(c => ""))
                                         ).CreateMapper().Map<IEnumerable<reminderOrСomments>, List<reminderOrСommentsBL>>(Database.ReminderOrСomments.Find(p => p.date > DateTime.Today)).OrderBy(p => p.date).ThenBy(p => p.idAdminStr).ToList();

                var mapstud = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
                                        .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
                                        .ForMember("Parent", opt => opt.MapFrom(c => c.idParent == null ? null : c.parentsOfStudent.surname + " " + c.parentsOfStudent.name + " " + c.parentsOfStudent.patronymic))
                                        .ForMember("FormOfEducation", opt => opt.MapFrom(c => c.formOfEducation.form))
                                        .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idEducationalMaterials == null ? null : c.educationalMaterial.name))
                                        .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.idLevelOfEnglish == null ? null : c.levelOfEnglish.level))
                                         ).CreateMapper();

                el.waitingList = new MapperConfiguration(cfg => cfg.CreateMap<waitingList, waitingListBL>()
                                        .ForMember("studentEl", opt => opt.MapFrom(c => mapstud.Map<student, studentBL>(c.student)))
                                        .ForMember("adminEl", opt => opt.MapFrom(c => c.AdminList))
                                        ).CreateMapper().Map<IEnumerable<waitingList>, List<waitingListBL>>(Database.WaitingLists.Find(p => p.dateTarget_ > DateTime.Today.AddDays(-1))).OrderBy(p => p.dateTarget_).ThenBy(p => p.studentEl.surname).ToList();
                return el;
            }
            else if (id == -1)
            {
                return new forCalendarBL { openingHour = new MapperConfiguration(cfg => cfg.CreateMap<openingHour, openingHourBL>()).CreateMapper().Map<openingHour, openingHourBL>(Database.OpeningHour.GetAll().FirstOrDefault()) };
            }
            else
            {
                var el = new forCalendarBL();
                el.openingHour = new MapperConfiguration(cfg => cfg.CreateMap<openingHour, openingHourBL>()).CreateMapper().Map<openingHour, openingHourBL>(Database.OpeningHour.GetAll().FirstOrDefault());
                el.reminderOrСommentsTeacher = new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСommentsTeacher, reminderOrСommentsTeacherBL>()
               .ForMember("TeachersName", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name + " " + c.teacher.patronymic))
               ).CreateMapper().Map<IEnumerable<reminderOrСommentsTeacher>, List<reminderOrСommentsTeacherBL>>(Database.ReminderOrСommentsTeacher.Find(p => p.date > DateTime.Today && p.idTeachers == id)).OrderBy(p => p.date).ThenBy(p => p.idTeachers).ToList();
                return el;
            }
        }

        public forCalendarBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(forCalendarBL El) => throw new NotImplementedException();

        public void Delete(int id) => throw new NotImplementedException();

        public forCalendarBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<forCalendarBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
