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
    public class studentBLRepository : IRepositoryBL<studentBL>
    {
        IUnitOfWork Database { get; set; }
        public studentBLRepository(ref IUnitOfWork uow) => Database = uow;
        public IEnumerable<studentBL> GetAll()
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
            .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
            .ForMember("Parent", opt => opt.MapFrom(c => c.idParent == null ? null : c.parentsOfStudent.surname + " " + c.parentsOfStudent.name + " " + c.parentsOfStudent.patronymic))
            .ForMember("FormOfEducation", opt => opt.MapFrom(c => c.formOfEducation.form))
            .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idEducationalMaterials == null ? null : c.educationalMaterial.name))
            .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.idLevelOfEnglish == null ? null : c.levelOfEnglish.level))
            ).CreateMapper().Map<IEnumerable<student>, List<studentBL>>(Database.Students.GetAll());
        }
        public int Add(studentBL el_)
        {
            try
            {
                el_.name = el_.name.Trim();
                if (!string.IsNullOrEmpty(el_.surname))
                    el_.surname = el_.surname.Trim();
                if (!string.IsNullOrEmpty(el_.mail))
                    el_.mail = el_.mail.Trim();
                if (!string.IsNullOrEmpty(el_.phoneNumber))
                    el_.phoneNumber = el_.phoneNumber.Trim();
                if (!string.IsNullOrEmpty(el_.patronymic))
                    el_.patronymic = el_.patronymic.Trim();
                var searchTemp = el_.name + el_.surname + el_.mail + el_.phoneNumber + el_.patronymic;
                if (Database.Students.Find(p => (p.name + p.surname + p.mail + p.phoneNumber + p.patronymic) == searchTemp).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<studentBL, student>()).CreateMapper();
                    student el = mapper.Map<studentBL, student>(el_);
                    el.dateCreate = DateTime.Today;
                    if (!string.IsNullOrEmpty(el.mail))
                        el.mail = el.mail.ToLower();

                    if (el.idEducationalMaterials == 0)
                        el.idEducationalMaterials = null;
                    if (el.idLevelOfEnglish == 0)
                        el.idLevelOfEnglish = null;
                    if (el.idParent == 0)
                        el.idParent = null;
                    Database.Students.Create(ref el);
                    Database.Save();
                    return el.Id;
                }
            }
            catch { }
            return 0;
        }
        public studentBL Get(int id)
        {
            var el = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
            .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
            .ForMember("Parent", opt => opt.MapFrom(c => c.idParent == null ? null : c.parentsOfStudent.surname + " " + c.parentsOfStudent.name + " " + c.parentsOfStudent.patronymic))
            .ForMember("FormOfEducation", opt => opt.MapFrom(c => c.formOfEducation.form))
            .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idEducationalMaterials == null ? null : c.educationalMaterial.name))
            .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.idLevelOfEnglish == null ? null : c.levelOfEnglish.level))
            ).CreateMapper().Map<student, studentBL>(Database.Students.Get(id));

            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruption, causeOfDisruptionBL>()).CreateMapper();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDate, timetableByDateBL>()
            .ForMember("ClassRoom", opt => opt.MapFrom(c => c.classroom.name))
            .ForMember("Teacher", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name))
            .ForMember("Admin", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))
            .ForMember("causeOfDisruption", opt => opt.MapFrom(c => mapper2.Map<causeOfDisruption, causeOfDisruptionBL>(c.pauses.FirstOrDefault().causeOfDisruption)))
            ).CreateMapper();
            var listTimeTable = Database.TimetableByDates.Find(p => p.idStudent == id || (p.idGroup != null && p.group.studentsInGroups.Select(c => c.idStudents).Contains(id))).ToList();
            el.timeTableHistory = mapper.Map<IEnumerable<timetableByDate>, List<timetableByDateBL>>(listTimeTable.Where(p => p.endTime < DateTime.Now).OrderByDescending(p => p.endTime));
            el.nextLesson = mapper.Map<IEnumerable<timetableByDate>, List<timetableByDateBL>>(listTimeTable.Where(p => p.beginTime > DateTime.Now).OrderBy(p => p.beginTime));

            return el;
        }
        public studentBL GetFullInfo(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
           .ForMember("Parent", opt => opt.MapFrom(c => c.idParent == null ? null : c.parentsOfStudent.surname + " " + c.parentsOfStudent.name + " " + c.parentsOfStudent.patronymic))
           .ForMember("FormOfEducation", opt => opt.MapFrom(c => c.formOfEducation.form))
           .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idEducationalMaterials == null ? null : c.educationalMaterial.name))
           .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.idLevelOfEnglish == null ? null : c.levelOfEnglish.level))
           ).CreateMapper();
            return mapper.Map<student, studentBL>(Database.Students.Get(id));
        }
        public int Update(studentBL El)
        {
            El.name = El.name.Trim();
            if (!string.IsNullOrEmpty(El.surname))
                El.surname = El.surname.Trim();
            if (!string.IsNullOrEmpty(El.mail))
                El.mail = El.mail.Trim();
            if (!string.IsNullOrEmpty(El.phoneNumber))
                El.phoneNumber = El.phoneNumber.Trim();
            if (!string.IsNullOrEmpty(El.patronymic))
                El.patronymic = El.patronymic.Trim();
            var searchTemp = El.name + El.surname + El.patronymic+ El.mail+El.phoneNumber;
            student old = Database.Students.Get(El.Id);
            if (old == null)
                return 2;

            if (Database.Students.Find(p => (p.name + p.surname + p.patronymic + p.mail + p.phoneNumber) == searchTemp && p.Id != El.Id).FirstOrDefault() != null)
                   return 0;
            old.surname = El.surname;
            old.name = El.name;
            old.patronymic = El.patronymic;
            old.mail = El.mail;
            old.phoneNumber = El.phoneNumber;
            old.dateOfBirth = El.dateOfBirth;
            old.idParent = El.idParent == 0 ? null : El.idParent;
            old.idFormOfEducation = El.idFormOfEducation;
            old.idEducationalMaterials = El.idEducationalMaterials == 0 ? null : El.idEducationalMaterials;
            old.idLevelOfEnglish = El.idLevelOfEnglish == 0 ? null : El.idLevelOfEnglish;
            old.notes = El.notes;
            old.isFirstLesson = El.isFirstLesson;
            old.IdStudent = El.IdStudent;
            old.disable = El.disable;
            Database.Students.Update(old);
            Database.Save();
            return 1;

        }

        public void Delete(int id)
        {
            Database.Students.Delete(id);
            var el = Database.TimetableByDates.Find(p => p.idStudent == id).Select(p => p.Id);
            foreach (var i in el)
                Database.TimetableByDates.Delete(i);
            Database.Save();
        }

        public studentBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<studentBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
