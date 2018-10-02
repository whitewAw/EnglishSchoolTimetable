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
   public class timetableByDateBLRepository : IRepositoryBL<timetableByDateBL>
    {
        IUnitOfWork Database { get; set; }
        public timetableByDateBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<timetableByDateBL> GetAll()
        {
            throw new NotImplementedException();
            //var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruption, causeOfDisruptionBL>()).CreateMapper();

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDate, timetableByDateBL>()
            //.ForMember("ClassRoom", opt => opt.MapFrom(c => c.classroom.name))
            //.ForMember("Teacher", opt => opt.MapFrom(c => c.teacher.surname+" "+ c.teacher.name))
            //.ForMember("Group", opt => opt.MapFrom(c => c.group.name))
            //.ForMember("Student", opt => opt.MapFrom(c => c.student.surname+" "+ c.student.name))
            //.ForMember("Admin", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))

            //.ForMember("causeOfDisruption", opt => opt.MapFrom(c => mapper2.Map<causeOfDisruption, causeOfDisruptionBL>(c.pauses.FirstOrDefault().causeOfDisruption)))
            //).CreateMapper();
            //return mapper.Map<IEnumerable<timetableByDate>, List<timetableByDateBL>>(Database.TimetableByDates.GetAll());
        }

        public int Add(timetableByDateBL el)
        {
            try
            {
                //el.uniqueManyNumber = (int)DateTime.Now.Subtract(new DateTime(2000, 9, 15)).TotalSeconds;
                el.uniqueManyNumber = (int)DateTime.Now.Subtract(new DateTime(1986, 6, 4)).TotalSeconds - (int)DateTime.Now.Subtract(new DateTime(2000, 9, 15)).TotalSeconds;
                el.idAdmin = Database.AdminLists.Find(p => p.IdAdmin == el.Admin).Select(p => p.Id).FirstOrDefault();

                //2 уровень языка не соответсвуут
                //3 выбранная дата не соответсвует дню.
                //4 у студента не выбраны учебные материалы
                if (el.idStudent != 0)
                     if (Database.Students.Get(el.idStudent).idEducationalMaterials == null)
                        return 4;
              

                int levEngl=0;
                if (el.idStudent != 0)
                    levEngl = (int)Database.Students.Get(el.idStudent).idLevelOfEnglish;
                else if (el.idGroup != 0)
                    levEngl = Database.Groups.Get(el.idGroup).idLevelOfEnglish;
                bool addDate = false;
                string note;
                for (;el.beginTime <= el.endTime; el.beginTime=el.beginTime.Value.AddDays(1))
                {
                    var idDay = (int)el.beginTime.Value.DayOfWeek;
                    idDay = idDay == 0 ? 7 : idDay;
                    var elPart = el.timetableDays.Where(p => p.idDay == idDay).FirstOrDefault();
                    if (elPart != null)
                        if (elPart.begin != null && elPart.end != null)
                        {
                            var teachLavel = Database.Teachers.Get(elPart.idTeacher).levelsOfTeachingTeachers.Select(p => p.idLevelOfEnglish).ToList();
                            if (!teachLavel.Contains(levEngl))
                                return 2;

                            note = "";
                            int? idGoup;
                            if (el.idGroup == 0)
                                idGoup = null;
                            else
                                idGoup = el.idGroup;
                            int? idStud;
                            if (el.idStudent == 0)
                                idStud = null;
                            else
                                idStud = el.idStudent;
                            if (el.idStudent != 0)
                            {
                                var studet = Database.Students.Get(el.idStudent);
                                if (studet.isFirstLesson==true)
                                {
                                    note += string.Format("First Class:\n{0};\n\n", studet.surname + " " + studet.name);
                                    studet.isFirstLesson = false;
                                    Database.Students.Update(studet);
                                    Database.Save();
                                }

                                if (studet.dateOfBirth != null)
                                    if ((el.beginTime - studet.dateOfBirth).Value.TotalDays < 14 * 365)
                                         note += string.Format("Студент младше 14 лет:\n{0};\n\n", studet.surname + " " + studet.name);
                            }
                            if (el.idGroup != 0)
                            {
                                var studetinGrup = Database.Groups.Get(el.idGroup).studentsInGroups.Select(p => p.idStudents).ToList();
                                var studet = Database.Students.GetAll().Where(p => studetinGrup.Contains(p.Id)).ToList();
                                var studentFirsLesson = studet.Where(p => p.isFirstLesson).ToList();
                                if (studentFirsLesson.Count > 0)
                                {
                                    note += "First Class:\n";
                                    for (int i = 0, j = studentFirsLesson.Count; i < j; i++)
                                    {
                                        note += string.Format("{0};\n", studentFirsLesson[i].surname + " " + studentFirsLesson[i].name);
                                        studentFirsLesson[i].isFirstLesson = false;
                                        Database.Students.Update(studentFirsLesson[i]);
                                        Database.Save();
                                    }
                                    note += "\n";
                                }
                                var studentwithdateOfBirth = studet.Where(p => p.dateOfBirth != null).ToList();
                                if (studentwithdateOfBirth != null)
                                {
                                    var studentYoung = studentwithdateOfBirth.Where(p => (el.beginTime - p.dateOfBirth).Value.TotalDays < 14 * 365).ToList();
                                    if (studentYoung.Count > 0)
                                    {
                                        note += "Студенты младше 14 лет:\n";
                                        foreach (var i in studentYoung)
                                        {
                                            note += string.Format("{0};\n", i.surname + " " + i.name);
                                        }

                                    }
                                    note += "\n";
                                }
                            }
                            var elAdd = new timetableByDate {
                                beginTime = (DateTime)(el.beginTime.Value + elPart.begin),
                                endTime = (DateTime)(el.beginTime.Value + elPart.end),
                                idClassRoom = elPart.idClassRoom,
                                idTeacher = elPart.idTeacher,
                                idGroup = idGoup,
                                idStudent = idStud,
                                isCanceled = false,
                                notes = note+elPart.notes,
                                uniqueManyNumber = el.uniqueManyNumber,
                                idAdmin = el.idAdmin
                            };
                            addDate = true;
                            Database.TimetableByDates.Create(ref elAdd);
                        }
                }
                if (addDate == false)
                    return 3;
                Database.Save();
                return 1;
            }
            catch { return 0; }
        }

        public timetableByDateBL Get(int id)
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruption, causeOfDisruptionBL>()).CreateMapper();
 
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDate, timetableByDateBL>()
            .ForMember("ClassRoom", opt => opt.MapFrom(c => c.classroom.name))
            .ForMember("Teacher", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name))
            .ForMember("Group", opt => opt.MapFrom(c => c.group.levelOfEnglish.level + " " + c.group.name))
            .ForMember("Student", opt => opt.MapFrom(c => c.student.surname + " " + c.student.name))
            .ForMember("Admin", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))
            .ForMember("begin", opt => opt.MapFrom(c => new TimeSpan(c.beginTime.Hour, c.beginTime.Minute,0)))
            .ForMember("end", opt => opt.MapFrom(c => new TimeSpan(c.endTime.Hour, c.endTime.Minute, 0)))
            .ForMember("EducationalMaterials", opt => opt.MapFrom(c => c.idStudent == null ? c.group.educationalMaterial.name : c.student.educationalMaterial.name))
            .ForMember("causeOfDisruption", opt => opt.MapFrom(c => mapper2.Map<causeOfDisruption, causeOfDisruptionBL>(c.pauses.FirstOrDefault().causeOfDisruption)))
            ).CreateMapper();
            var el = mapper.Map<timetableByDate, timetableByDateBL>(Database.TimetableByDates.Get(id));
            if(el.causeOfDisruption!=null)
                el.idcauseOfDisruption = el.causeOfDisruption.Id;
            return el;
        }

        public timetableByDateBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(timetableByDateBL El)
        {
            var old = Database.TimetableByDates.Get(El.Id);
            if(old ==null)
                return 0;

            int levEngl = 0;
            if (old.idStudent != null)
                levEngl = (int)Database.Students.Get((int)old.idStudent).idLevelOfEnglish;
            else if (old.idGroup != null)
                levEngl = Database.Groups.Get((int)old.idGroup).idLevelOfEnglish;
            var teachLavel = Database.Teachers.Get(El.idTeacher).levelsOfTeachingTeachers.Select(p => p.idLevelOfEnglish).ToList();
            if (!teachLavel.Contains(levEngl))
                return 2;


            if (!String.IsNullOrEmpty(El.Teacher))
            {
                old.homework = El.homework;
                old.passedMaterials = El.passedMaterials;
            }
            if (!String.IsNullOrEmpty(El.Admin))
            {
                old.beginTime = (DateTime)El.beginTime + El.begin;
                old.endTime = (DateTime)El.beginTime + El.end;
                old.idClassRoom = El.idClassRoom;
                old.idTeacher = El.idTeacher;
                old.isCanceled = El.isCanceled;


                var oldPauseCade = Database.Pauses.Find(p => p.idTimetableByDate == El.Id).Select(p => p.Id);
                // var oldPauseCade = Database.Pauses.GetAll().Where(p => p.idTimetableByDate == El.Id).Select(p => p.Id);
                if (oldPauseCade != null)
                {
                    foreach (var i in oldPauseCade)
                        Database.Pauses.Delete(i);
                }
                if (El.isCanceled == true)
                {
                    var elPause = new pause { idCauseOfDisruption = El.causeOfDisruption.Id, idTimetableByDate = El.Id };
                    Database.Pauses.Create(ref elPause);
                }
                old.notes = El.notes;
                old.idAdmin = Database.AdminLists.Find(p => p.IdAdmin == El.Admin).Select(p => p.Id).FirstOrDefault();

                //old.homework = El.homework;
                //old.passedMaterials = El.passedMaterials;
            }
            Database.TimetableByDates.Update(old);
            Database.Save();
            return 1;
        }

        public void Delete(int id)
        {
            var oldPauseCade = Database.Pauses.Find(p => p.idTimetableByDate == id).Select(p => p.Id);
            //var oldPauseCade = Database.Pauses.GetAll().Where(p => p.idTimetableByDate == id).Select(p => p.Id);
            if (oldPauseCade != null)
            {
                foreach (var i in oldPauseCade)
                    Database.Pauses.Delete(i);
            }
            Database.TimetableByDates.Delete(id);
            Database.Save();
        }

        public timetableByDateBL GetEmpty()
        {
            var el = new timetableByDateBL();
            var dayOfWeek = Database.DaysOfTheWeeks.GetAll();
            foreach (var i in dayOfWeek)
            {
                el.timetableDays.Add(new timetableDayBL { idDay = i.Id, NameDay = i.day });
            }
            return el;
        }

        public IEnumerable<timetableByDateBL> TakeBetweenDates(DateTime? start, DateTime? finish)
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<causeOfDisruption, causeOfDisruptionBL>()).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDate, timetableByDateBL>()
            .ForMember("ClassRoom", opt => opt.MapFrom(c => c.classroom.name))
            .ForMember("Teacher", opt => opt.MapFrom(c => c.teacher.surname + " " + c.teacher.name))
            .ForMember("Group", opt => opt.MapFrom(c => c.group.levelOfEnglish.level +" "+ c.group.name))
            .ForMember("Student", opt => opt.MapFrom(c => c.student.surname + " " + c.student.name))
            .ForMember("Admin", opt => opt.MapFrom(c => c.AdminList.surname + " " + c.AdminList.name))
            .ForMember("EducationalMaterials", opt => opt.MapFrom(c =>  c.idStudent==null? c.group.educationalMaterial.name: c.student.educationalMaterial.name))
            .ForMember("causeOfDisruption", opt => opt.MapFrom(c => mapper2.Map<causeOfDisruption, causeOfDisruptionBL>(c.pauses.FirstOrDefault().causeOfDisruption)))
            ).CreateMapper();
            return mapper.Map<IEnumerable<timetableByDate>, List<timetableByDateBL>>(Database.TimetableByDates.Find(p => p.beginTime > start && p.endTime < finish));
         }
    }
}
