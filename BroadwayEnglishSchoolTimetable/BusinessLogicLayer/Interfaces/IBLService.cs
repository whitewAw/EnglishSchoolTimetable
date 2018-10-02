using BusinessLogicLayer.model;
using System;


namespace BusinessLogicLayer.Interfaces
{
    public interface IBLService: IDisposable
    {
        IRepositoryBL<AdminListBL> adminListBL { get; }
        IRepositoryBL<mainMailBL> mainMailBL { get; }
        IRepositoryBL<parentsOfStudentBL> parentsOfStudentBL { get; }
        IRepositoryBL<educationalMaterialBL> educationalMaterialBL { get; }
        IRepositoryBL<levelOfEnglishBL> levelOfEnglishBL { get; }
        IRepositoryBL<formOfEducationBL> formOfEducationBL { get; }
        IRepositoryBL<daysOfTheWeekBL> daysOfTheWeekBL { get; }
        IRepositoryBL<groupBL> groupBL { get; }
        IRepositoryBL<teacherBL> teacherBL { get; }
        IRepositoryBL<openingHourBL> openingHourBL { get; }
        IRepositoryBL<studentBL> studentBL { get; }
        IRepositoryBL<classroomBL> classroomBL { get; }
        IRepositoryBL<causeOfDisruptionBL> causeOfDisruptionBL { get; }
        IRepositoryBL<reminderOrСommentsBL> reminderOrСommentsBL { get; }
        IRepositoryBL<waitingListBL> waitingListBL { get; }
        IRepositoryBL<timetableByDateBL> timetableByDateBL { get; }
        IRepositoryBL<forCalendarBL> forCalendarBL { get; }
        IRepositoryBL<reminderOrСommentsTeacherBL> reminderOrСommentsTeacherBL { get; }
    }
}
