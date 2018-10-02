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
    class reminderOrСommentsTeacherBLRepository : IRepositoryBL<reminderOrСommentsTeacherBL>
    {
        IUnitOfWork Database { get; set; }
        public reminderOrСommentsTeacherBLRepository(ref IUnitOfWork uow) => Database = uow;



        public IEnumerable<reminderOrСommentsTeacherBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСommentsTeacher, reminderOrСommentsTeacherBL>()
        .ForMember("TeachersName", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name+" "+ c.teacher.patronymic))
        ).CreateMapper().Map<IEnumerable<reminderOrСommentsTeacher>, List<reminderOrСommentsTeacherBL>>(Database.ReminderOrСommentsTeacher.GetAll());

        public int Add(reminderOrСommentsTeacherBL el)
        {
            try
            {
                el.date = new DateTime(el.datePL.Value.Year, el.datePL.Value.Month, el.datePL.Value.Day, el.timePL.Value.Hours, el.timePL.Value.Minutes, 0);
                var eladd = new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСommentsTeacherBL, reminderOrСommentsTeacher>()).CreateMapper().Map<reminderOrСommentsTeacherBL, reminderOrСommentsTeacher>(el);
                Database.ReminderOrСommentsTeacher.Create(ref eladd);
                Database.Save();
                return eladd.Id;
            }
            catch { return 0; }
        }








        public reminderOrСommentsTeacherBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<reminderOrСommentsTeacher, reminderOrСommentsTeacherBL>()
                                                              .ForMember("TeachersName", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name + " " + c.teacher.patronymic))
                                                              .ForMember("datePL", opt => opt.MapFrom(c => new DateTime(c.date.Year, c.date.Month, c.date.Day)))
                                                             .ForMember("timePL", opt => opt.MapFrom(c => new TimeSpan(c.date.Hour, c.date.Minute, 0)))
            ).CreateMapper().Map<reminderOrСommentsTeacher, reminderOrСommentsTeacherBL>(Database.ReminderOrСommentsTeacher.Get(id));

        reminderOrСommentsTeacherBL IRepositoryBL<reminderOrСommentsTeacherBL>.GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(reminderOrСommentsTeacherBL El)
        {
            var el = Database.ReminderOrСommentsTeacher.Get(El.Id);
            if (el == null)
                return 2;
            el.date = new DateTime(El.datePL.Value.Year, El.datePL.Value.Month, El.datePL.Value.Day, El.timePL.Value.Hours, El.timePL.Value.Minutes, 0);
            el.notes = El.notes;
            el.idTeachers = El.idTeachers;
            Database.ReminderOrСommentsTeacher.Update(el);
            Database.Save();
            return 1;
        }

        public void Delete(int id)
        {
            Database.ReminderOrСommentsTeacher.Delete(id);
            Database.Save();
        }

        reminderOrСommentsTeacherBL IRepositoryBL<reminderOrСommentsTeacherBL>.GetEmpty() => throw new NotImplementedException();

        IEnumerable<reminderOrСommentsTeacherBL> IRepositoryBL<reminderOrСommentsTeacherBL>.TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
