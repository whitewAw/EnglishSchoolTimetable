using DataAccessLayer.Model;
using System;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<accessibilityOfTeacher> AccessibilityOfTeachers { get; }
        IRepository<causeOfDisruption> CauseOfDisruptions { get; }
        IRepository<classroom> Classrooms { get; }
        IRepository<daysOfTheWeek> DaysOfTheWeeks { get; }
        IRepository<educationalMaterial> EducationalMaterials { get; }
        IRepository<formOfEducation> FormOfEducations { get; }
        IRepository<group> Groups { get; }
        IRepository<levelOfEnglish> LevelOfEnglishes { get; }
        IRepository<parentsOfStudent> ParentsOfStudents { get; }
        IRepository<pause> Pauses { get; }
        IRepository<reminderOrСomments> ReminderOrСomments { get; }
        IRepository<student> Students { get; }
        IRepository<studentsInGroup> StudentsInGroups { get; }
        IRepository<teacher> Teachers { get; }
        IRepository<timetableByDate> TimetableByDates { get; }
        IRepository<waitingList> WaitingLists { get; }
        IRepository<AdminList> AdminLists { get; }
        IRepository<mainMail> MainMail { get; }
        IRepository<levelsOfTeachingTeacher> LevelsOfTeachingTeacher { get; }
        IRepository<openingHour> OpeningHour { get; }
        IRepository<reminderOrСommentsTeacher> ReminderOrСommentsTeacher { get; }

        void Save();
    }
}
