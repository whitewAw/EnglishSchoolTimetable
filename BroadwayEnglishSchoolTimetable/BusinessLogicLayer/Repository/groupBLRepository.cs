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
    public class groupBLRepository : IRepositoryBL<groupBL>
    {
        IUnitOfWork Database { get; set; }
        public groupBLRepository(ref IUnitOfWork uow) => Database = uow;
        public int Add(groupBL el)
        {
            try
            {
                el.name = el.name.Trim();
                if (Database.Groups.Find(p => p.name == el.name && p.idLevelOfEnglish== el.idLevelOfEnglish).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<groupBL, group>()).CreateMapper();
                    var elAdd = mapper.Map<groupBL, group>(el);
                    Database.Groups.Create(ref elAdd);
                    Database.Save();
                    foreach (var i in el.studentSelect)
                    {
                        var newEl = new studentsInGroup { idStudents = i, idGroup = elAdd.Id };
                        Database.StudentsInGroups.Create(ref newEl);
                    }
                    Database.Save();
                    return elAdd.Id;
                }
            }
            catch {  }
            return 0;
        }

        public void Delete(int id)
        {
            Database.Groups.Delete(id);
            var el = Database.Groups.Get(id).studentsInGroups.Select(p => p.Id);
            foreach (var i in el)
                Database.StudentsInGroups.Delete(i);
            Database.Save();
        }

        public groupBL Get(int id)
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
             .ForMember("LevelOfEnglish", opt => opt.MapFrom(c => c.levelOfEnglish.level))
             .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
             ).CreateMapper();

            var el = new MapperConfiguration(cfg => cfg.CreateMap<group, groupBL>()
            .ForMember("student", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<student>, List<studentBL>>(c.studentsInGroups.Select(p => p.student))))
           // .ForMember("studentBig", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<student>, List<studentBL>>(Database.Students.GetAll())))
            .ForMember("studentSelect", opt => opt.MapFrom(c => c.studentsInGroups.Select(p => p.student.Id)))
            .ForMember("studentBig", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<student>, List<studentBL>>(Database.Students.Find(p => p.disable == false || p.studentsInGroups.Select(d => d.student.Id).Contains(p.Id)))))
            .ForMember("nameEducationalMaterials", opt => opt.MapFrom(c => c.educationalMaterial.name))
            .ForMember("levelOfEnglish", opt => opt.MapFrom(c => c.levelOfEnglish.level))
            ).CreateMapper().Map<group, groupBL>(Database.Groups.Get(id));


            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDate, timetableByDateBL>()
           .ForMember("ClassRoom", opt => opt.MapFrom(c => c.classroom.name))
           .ForMember("Teacher", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name))
           .ForMember("Group", opt => opt.MapFrom(c => c.group.name))
           .ForMember("Student", opt => opt.MapFrom(c => c.student.surname + " " + c.student.name))
           .ForMember("Admin", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))
           .ForMember("causeOfDisruption", opt => opt.MapFrom(c => mapper2.Map<causeOfDisruption, causeOfDisruptionBL>(c.pauses.FirstOrDefault().causeOfDisruption)))
           ).CreateMapper();

            var listTimeTable = Database.TimetableByDates.Find(p => p.idGroup == id).ToList();
            el.timeTableHistory = mapper.Map<IEnumerable<timetableByDate>, List<timetableByDateBL>>(listTimeTable.Where(p => p.endTime < DateTime.Now).OrderByDescending(p => p.endTime));
            el.nextLesson = mapper.Map<IEnumerable<timetableByDate>, List<timetableByDateBL>>(listTimeTable.Where(p => p.beginTime > DateTime.Now).OrderBy(p => p.beginTime));

            return el;
        }

        public IEnumerable<groupBL> GetAll()
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>().ForMember("IdStudent", opt => opt.MapFrom(c => ""))).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<group, groupBL>()
            .ForMember("student", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<student>, List<studentBL>>(c.studentsInGroups.Select(p=>p.student))))
            .ForMember("nameEducationalMaterials", opt => opt.MapFrom(c => c.educationalMaterial.name))
            .ForMember("levelOfEnglish", opt => opt.MapFrom(c => c.levelOfEnglish.level))
            ).CreateMapper();
            return mapper.Map<IEnumerable<group>, IEnumerable<groupBL>>(Database.Groups.GetAll());
        }

        public groupBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(groupBL El)
        {
            El.name = El.name.Trim();
            if (Database.Groups.Find(p => p.name == El.name && p.Id != El.Id && p.idLevelOfEnglish == El.idLevelOfEnglish).FirstOrDefault() == null)
            {
                var el = Database.Groups.Get(El.Id);
                if (el == null)
                    return 2;
                if (!string.IsNullOrEmpty(el.name))
                    el.name = el.name.Trim();
                el.name = El.name;
                if (!string.IsNullOrEmpty(el.name))
                    el.notes = el.notes.Trim();
                el.notes = El.notes;
                el.disable = El.disable;
                el.idEducationalMaterials = El.idEducationalMaterials;
                Database.Groups.Update(el);

                var oldListStud = el.studentsInGroups.Select(p => p.idStudents).ToList();

                List<int> forDelete = new List<int>();
                List<int> forCreate = new List<int>();

                foreach (var i in oldListStud)
                    if (!El.studentSelect.Contains(i))
                       forDelete.AddRange(Database.StudentsInGroups.Find(p => p.idStudents == i && p.idGroup == el.Id).Select(p => p.Id));
                foreach (var i in El.studentSelect)
                    if (!oldListStud.Contains(i))
                        forCreate.Add(i);
                foreach (var i in forDelete)
                    Database.StudentsInGroups.Delete(i);
                foreach (var i in forCreate)
                {
                    var newEl = new studentsInGroup { idStudents = i, idGroup = el.Id };
                    Database.StudentsInGroups.Create(ref newEl);
                }
                Database.Save();
                return 1;
            }
            return 0;
        }

        public groupBL GetEmpty()
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<student, studentBL>()
            .ForMember("LevelOfEnglish", opt=>opt.MapFrom(c=>c.levelOfEnglish.level))
            .ForMember("IdStudent", opt => opt.MapFrom(c => ""))
            ).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<group, groupBL>()
            .ForMember("studentBig", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<student>, List<studentBL>>(Database.Students.Find(p=>p.disable==false && (p.idFormOfEducation == (int)FormEdication.Group || p.idFormOfEducation == (int)FormEdication.GroupPrivate))))) //форма обучения!!!
            ).CreateMapper(); 
            return mapper.Map<group, groupBL>(new group { name = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() });
        }

        public IEnumerable<groupBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
