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
    public class teacherBLRepository : IRepositoryBL<teacherBL>
    {
        IUnitOfWork Database { get; set; }
        public teacherBLRepository(ref IUnitOfWork uow) => Database = uow;
        public int Add(teacherBL elBL)
        {
            try
            {
                elBL.name = elBL.name.Trim();
                elBL.mail = elBL.mail.Trim();
                elBL.phoneNumber = elBL.phoneNumber.Trim();
                if (!string.IsNullOrEmpty(elBL.patronymic))
                    elBL.patronymic = elBL.patronymic.Trim();
                if (!string.IsNullOrEmpty(elBL.surname))
                    elBL.surname = elBL.surname.Trim();
                var searchTemp = elBL.name + elBL.surname + elBL.mail + elBL.phoneNumber + elBL.patronymic;
                if (Database.Teachers.Find(p => (p.name + p.surname + p.mail + p.phoneNumber + p.patronymic) == searchTemp).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<teacherBL, teacher>()).CreateMapper();
                    teacher el = mapper.Map<teacherBL, teacher>(elBL);
                    el.dateCreate = DateTime.Today;
                    Database.Teachers.Create(ref el);
                    Database.Save();
                    foreach (var elEnLev in elBL.levelOfEnglishInt)
                    {
                        levelsOfTeachingTeacher LOE = new levelsOfTeachingTeacher { idTeacher = el.Id, idLevelOfEnglish = elEnLev };
                        Database.LevelsOfTeachingTeacher.Create(ref LOE);
                    }
                    var openH = Database.OpeningHour.GetAll().FirstOrDefault();
                    foreach (var availableTime in elBL.accessibilityOfTeacherBig)
                        if (availableTime.startTime != null && availableTime.endTime != null && availableTime.endTime > availableTime.startTime && availableTime.startTime >= openH.open && availableTime.endTime <= openH.close)
                        {
                            if (!string.IsNullOrEmpty(availableTime.notes))
                                availableTime.notes = availableTime.notes.Trim();
                            accessibilityOfTeacher aOT = new accessibilityOfTeacher { idTeachers = el.Id, idDaysOfTheWeek = availableTime.idDaysOfTheWeek, notes = availableTime.notes, startTime = (TimeSpan)availableTime.startTime, endTime = (TimeSpan)availableTime.endTime };
                            Database.AccessibilityOfTeachers.Create(ref aOT);
                        }

                    Database.Save();

                    return el.Id;
                }
            }
            catch { }
            return 0;
        }

        public void Delete(int id)
        {
            teacher old = Database.Teachers.Get(id);
            var listOldLev = old.levelsOfTeachingTeachers.Select(p => p.Id).ToList();
            foreach(var i in listOldLev)
                Database.LevelsOfTeachingTeacher.Delete(i);

            var listAOT = old.accessibilityOfTeachers.Select(p => p.Id).ToList();
            foreach (var i in listAOT)
                Database.AccessibilityOfTeachers.Delete(i);

            Database.Teachers.Delete(id);
            Database.Save();
        }

        public teacherBL Get(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<teacher, teacherBL>().ForMember("IdTeacher", opt => opt.MapFrom(c => ""))).CreateMapper();
            var elDL = Database.Teachers.Get(id);
            var el = mapper.Map<teacher, teacherBL>(elDL);
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<levelOfEnglish, levelOfEnglishBL>()).CreateMapper();
            el.levelOfEnglish =  mapper.Map<IEnumerable<levelOfEnglish>, List<levelOfEnglishBL>>(Database.LevelOfEnglishes.GetAll().OrderBy(p => p.level));
            el.levelOfEnglishInt = elDL.levelsOfTeachingTeachers.Select(p => p.idLevelOfEnglish).ToList();
            el.NamelevelOfEnglish = elDL.levelsOfTeachingTeachers.Select(p => p.levelOfEnglish.level).ToList();

            mapper = new MapperConfiguration(cfg => cfg.CreateMap<accessibilityOfTeacher, accessibilityOfTeacherBL>()).CreateMapper();
            el.accessibilityOfTeacher = mapper.Map<IEnumerable<accessibilityOfTeacher>, List<accessibilityOfTeacherBL>>(Database.AccessibilityOfTeachers.Find(p => p.idTeachers == id).OrderBy(p => p.idDaysOfTheWeek));
            var day = Database.DaysOfTheWeeks.GetAll();
            foreach (var i in day)
            {
                if (el.accessibilityOfTeacher.Where(p => p.idDaysOfTheWeek == i.Id).FirstOrDefault() == null)
                    el.accessibilityOfTeacherBig.Add(new accessibilityOfTeacherBL { DaysOfTheWeekName = i.day, idDaysOfTheWeek = i.Id });
                else
                {
                    var elAdd = el.accessibilityOfTeacher.Where(p => p.idDaysOfTheWeek == i.Id).FirstOrDefault();
                    elAdd.DaysOfTheWeekName = i.day;
                    elAdd.idDaysOfTheWeek = i.Id;
                    el.accessibilityOfTeacherBig.Add(elAdd);
                }
                  
            }
            return el;
        }

        public IEnumerable<teacherBL> GetAll()
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<accessibilityOfTeacher, accessibilityOfTeacherBL>()).CreateMapper();
             
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<teacher, teacherBL>()
            //.ForMember("IdTeacher", opt => opt.MapFrom(c => ""))
            .ForMember("accessibilityOfTeacher", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<accessibilityOfTeacher>, List<accessibilityOfTeacherBL>>(c.accessibilityOfTeachers)))
            .ForMember("NamelevelOfEnglish", opt => opt.MapFrom(c => c.levelsOfTeachingTeachers.Select(p => p.levelOfEnglish.level)))
            ).CreateMapper();
            var el = mapper.Map<IEnumerable<teacher>, List<teacherBL>>(Database.Teachers.GetAll());
            return el;
            
        }

        public teacherBL GetFullInfo(int id) => new MapperConfiguration(cfg => cfg.CreateMap<teacher, teacherBL>()).CreateMapper().Map<teacher, teacherBL>(Database.Teachers.Get(id));

        public int Update(teacherBL El)
        {
            El.name = El.name.Trim();
            El.mail = El.mail.Trim();
            El.phoneNumber = El.phoneNumber.Trim();
            if (!string.IsNullOrEmpty(El.patronymic))
                El.patronymic = El.patronymic.Trim();
            if (!string.IsNullOrEmpty(El.surname))
                El.surname = El.surname.Trim();
            var searchTemp = El.name + El.surname + El.mail + El.phoneNumber + El.patronymic;
            if (Database.Teachers.Find(p => (p.name + p.surname + p.mail + p.phoneNumber + p.patronymic) == searchTemp && p.Id != El.Id).FirstOrDefault() == null)
            {
                teacher old = Database.Teachers.Get(El.Id);
                if(old==null)
                    return 2;
                old.surname = El.surname;
                old.name = El.name;
                old.patronymic = El.patronymic;
                old.mail = El.mail;
                old.phoneNumber = El.phoneNumber;
                old.dateOfBirth = (DateTime)El.dateOfBirth;
                old.notes = El.notes;
                old.startDate = El.startDate;
                old.endDate = El.endDate;
                old.forBeginners = El.forBeginners;
                Database.Teachers.Update(old);
                var listOldLev = old.levelsOfTeachingTeachers.Select(p => p.Id).ToList();
                List<int> needDelete = new List<int>();
                List<int> needCreate = new List<int>();
                foreach (var i in listOldLev)
                    if (!El.levelOfEnglishInt.Contains(i))
                        needDelete.Add(i);
                foreach (var i in El.levelOfEnglishInt)
                    if (!listOldLev.Contains(i))
                        needCreate.Add(i);
                foreach (var i in needDelete)
                    Database.LevelsOfTeachingTeacher.Delete(i);
                foreach (var i in needCreate)
                {
                    levelsOfTeachingTeacher LOE = new levelsOfTeachingTeacher { idTeacher = El.Id, idLevelOfEnglish = i };
                    Database.LevelsOfTeachingTeacher.Create(ref LOE);
                }

                List<int> aOTCreate = new List<int>();
                List<int> aOTUpdate = new List<int>();
                List<int> aOTDelete = new List<int>();
                var openH = Database.OpeningHour.GetAll().FirstOrDefault();

                for (int i = 0, j = El.accessibilityOfTeacherBig.Count; i < j; i++)
                {
                    if (El.accessibilityOfTeacherBig[i].Id == 0)
                    {
                        if (El.accessibilityOfTeacherBig[i].startTime != null && El.accessibilityOfTeacherBig[i].endTime != null && El.accessibilityOfTeacherBig[i].endTime > El.accessibilityOfTeacherBig[i].startTime && El.accessibilityOfTeacherBig[i].startTime >= openH.open && El.accessibilityOfTeacherBig[i].endTime <= openH.close)
                            aOTCreate.Add(i);
                    }
                    else
                    {
                        if (El.accessibilityOfTeacherBig[i].startTime == null || El.accessibilityOfTeacherBig[i].endTime == null || El.accessibilityOfTeacherBig[i].endTime <= El.accessibilityOfTeacherBig[i].startTime || El.accessibilityOfTeacherBig[i].startTime < openH.open || El.accessibilityOfTeacherBig[i].endTime > openH.close)
                            aOTDelete.Add(i);
                        else
                            aOTUpdate.Add(i);
                    }
                }

                foreach (var i in aOTDelete)
                    Database.AccessibilityOfTeachers.Delete(El.accessibilityOfTeacherBig[i].Id);

                foreach (var i in aOTUpdate)
                {
                    var elaOT = Database.AccessibilityOfTeachers.Get(El.accessibilityOfTeacherBig[i].Id);
                    elaOT.startTime = (TimeSpan)El.accessibilityOfTeacherBig[i].startTime;
                    elaOT.endTime = (TimeSpan)El.accessibilityOfTeacherBig[i].endTime;
                    if (!string.IsNullOrEmpty(El.accessibilityOfTeacherBig[i].notes))
                        El.accessibilityOfTeacherBig[i].notes = El.accessibilityOfTeacherBig[i].notes.Trim();
                    elaOT.notes = El.accessibilityOfTeacherBig[i].notes;
                    Database.AccessibilityOfTeachers.Update(elaOT);
                }

                foreach (var i in aOTCreate)
                {
                    if (!string.IsNullOrEmpty(El.accessibilityOfTeacherBig[i].notes))
                        El.accessibilityOfTeacherBig[i].notes = El.accessibilityOfTeacherBig[i].notes.Trim();
                    accessibilityOfTeacher aOT = new accessibilityOfTeacher { idTeachers = El.Id, idDaysOfTheWeek = El.accessibilityOfTeacherBig[i].idDaysOfTheWeek, notes = El.accessibilityOfTeacherBig[i].notes, startTime = (TimeSpan)El.accessibilityOfTeacherBig[i].startTime, endTime = (TimeSpan)El.accessibilityOfTeacherBig[i].endTime };
                    Database.AccessibilityOfTeachers.Create(ref aOT);
                }
                Database.Save();
                return 1;
            }
            return 0;
        }

        public teacherBL GetEmpty()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<levelOfEnglish, levelOfEnglishBL>()).CreateMapper();
            var el = new teacherBL { levelOfEnglish = mapper.Map<IEnumerable<levelOfEnglish>, List<levelOfEnglishBL>>(Database.LevelOfEnglishes.GetAll()).OrderBy(p => p.level).ToList(), startDate = DateTime.Today };
            var day = Database.DaysOfTheWeeks.GetAll();
            foreach (var i in day)
                el.accessibilityOfTeacherBig.Add(new accessibilityOfTeacherBL { DaysOfTheWeekName = i.day, idDaysOfTheWeek=i.Id });
            return el;
        }

        public IEnumerable<teacherBL> TakeBetweenDates(DateTime? start, DateTime? finish)
        {
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<accessibilityOfTeacher, accessibilityOfTeacherBL>()).CreateMapper();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<teacher, teacherBL>()
            //.ForMember("IdTeacher", opt => opt.MapFrom(c => ""))
            .ForMember("accessibilityOfTeacher", opt => opt.MapFrom(c => mapper2.Map<IEnumerable<accessibilityOfTeacher>, List<accessibilityOfTeacherBL>>(c.accessibilityOfTeachers)))
            .ForMember("NamelevelOfEnglish", opt => opt.MapFrom(c => c.levelsOfTeachingTeachers.Select(p => p.levelOfEnglish.level)))
            ).CreateMapper();
            var el = mapper.Map<IEnumerable<teacher>, List<teacherBL>>(Database.Teachers.Find(p=>p.startDate< start && (p.endDate == null || p.endDate > finish)));
            return el;
        }
    }
}

