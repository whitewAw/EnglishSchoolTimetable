using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.model;

namespace BroadwayEnglishSchoolTimetable.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AdminListBL, AdminListPL>();
                cfg.CreateMap<AdminListPL, AdminListBL>();
                cfg.CreateMap<mainMailBL, mainMailPL>();
                cfg.CreateMap<mainMailPL, mainMailBL>();
                cfg.CreateMap<parentsOfStudentBL, parentsOfStudentPL>();
                cfg.CreateMap<parentsOfStudentPL, parentsOfStudentBL>();
                cfg.CreateMap<educationalMaterialBL, educationalMaterialPL>();
                cfg.CreateMap<educationalMaterialPL, educationalMaterialBL>();
                cfg.CreateMap<levelOfEnglishBL, levelOfEnglishPL>();
                cfg.CreateMap<levelOfEnglishPL, levelOfEnglishBL>();
                cfg.CreateMap<formOfEducationBL, formOfEducationPL>();
                cfg.CreateMap<formOfEducationPL, formOfEducationBL>();
                cfg.CreateMap<daysOfTheWeekBL, daysOfTheWeekPL>();
                cfg.CreateMap<daysOfTheWeekPL, daysOfTheWeekBL>();
                cfg.CreateMap<groupBL, groupPL>();
                cfg.CreateMap<groupPL, groupBL>();
                cfg.CreateMap<teacherBL, teacherPL>();
                cfg.CreateMap<teacherPL, teacherBL>();
                cfg.CreateMap<openingHourBL, openingHourPL>();
                cfg.CreateMap<openingHourPL, openingHourBL>();
                cfg.CreateMap<studentBL, studentPL>();
                cfg.CreateMap<studentPL, studentBL>();
                cfg.CreateMap<classroomBL, classroomPL>();
                cfg.CreateMap<classroomPL, classroomBL>();
                cfg.CreateMap<causeOfDisruptionBL, causeOfDisruptionPL>();
                cfg.CreateMap<causeOfDisruptionPL, causeOfDisruptionBL>();
                cfg.CreateMap<reminderOrСommentsBL, reminderOrСommentsPL>();
                cfg.CreateMap<reminderOrСommentsPL, reminderOrСommentsBL>();
                cfg.CreateMap<waitingListBL, waitingListPL>();
                cfg.CreateMap<waitingListPL, waitingListBL>();
                cfg.CreateMap<timetableByDateBL, timetableByDatePL>();
                cfg.CreateMap<timetableByDatePL, timetableByDateBL>();
                cfg.CreateMap<timetableDayBL, timetableDayPL>();
                cfg.CreateMap<timetableDayPL, timetableDayBL>();
                cfg.CreateMap<forCalendarBL, forCalendarPL>();
                cfg.CreateMap<forCalendarPL, forCalendarBL>();
                cfg.CreateMap<reminderOrСommentsTeacherBL, reminderOrСommentsTeacherPL>();
                cfg.CreateMap<reminderOrСommentsTeacherPL, reminderOrСommentsTeacherBL>();


            });
        }
    }
}

