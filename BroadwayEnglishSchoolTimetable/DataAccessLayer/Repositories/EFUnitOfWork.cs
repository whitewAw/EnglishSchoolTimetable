using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;

namespace DataAccessLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private BESTDBModel db;

        private accessibilityOfTeacherRepository accessibilityOfTeacherRepository_;
        private causeOfDisruptionRepository causeOfDisruptionRepository_;
        private classroomRepository classroomRepository_;
        private daysOfTheWeekRepository daysOfTheWeekRepository_;
        private educationalMaterialRepository educationalMaterialRepository_;
        private formOfEducationRepository formOfEducationRepository_;
        private groupRepository groupRepository_;
        private levelOfEnglishRepository levelOfEnglishRepository_;
        private parentsOfStudentRepository parentsOfStudentRepository_;
        private pauseRepository pauseRepository_;
        private reminderOrСommentsRepository reminderOrСommentsRepository_;
        private studentRepository studentRepository_;
        private studentsInGroupRepository studentsInGroupRepository_;
        private teacherRepository teacherRepository_;
        private timetableByDateRepository timetableByDateRepository_;
        private waitingListRepository waitingListRepository_;
        private adminListRepository adminListRepository_;
        private mainMailRepository mainMailRepository_;
        private levelsOfTeachingTeacherRepository levelsOfTeachingTeacherRepository_;
        private openingHourRepository openingHourRepository_;
        private reminderOrСommentsTeacherRepository reminderOrСommentsTeacherRepository_;

        public EFUnitOfWork(string connectionString)
        {
            db = new BESTDBModel(connectionString);
        }
        public IRepository<accessibilityOfTeacher> AccessibilityOfTeachers
        {
            get
            {
                if (accessibilityOfTeacherRepository_ == null)
                    accessibilityOfTeacherRepository_ = new accessibilityOfTeacherRepository(db);
                return accessibilityOfTeacherRepository_;
            }
        }
        public IRepository<causeOfDisruption> CauseOfDisruptions
        {
            get
            {
                if (causeOfDisruptionRepository_ == null)
                    causeOfDisruptionRepository_ = new causeOfDisruptionRepository(db);
                return causeOfDisruptionRepository_;
            }
        }
        public IRepository<classroom> Classrooms
        {
            get
            {
                if (classroomRepository_ == null)
                    classroomRepository_ = new classroomRepository(db);
                return classroomRepository_;
            }
        }
        public IRepository<daysOfTheWeek> DaysOfTheWeeks
        {
            get
            {
                if (daysOfTheWeekRepository_ == null)
                    daysOfTheWeekRepository_ = new daysOfTheWeekRepository(db);
                return daysOfTheWeekRepository_;
            }
        }
        public IRepository<educationalMaterial> EducationalMaterials
        {
            get
            {
                if (educationalMaterialRepository_ == null)
                    educationalMaterialRepository_ = new educationalMaterialRepository(db);
                return educationalMaterialRepository_;
            }
        }
        public IRepository<formOfEducation> FormOfEducations
        {
            get
            {
                if (formOfEducationRepository_ == null)
                    formOfEducationRepository_ = new formOfEducationRepository(db);
                return formOfEducationRepository_;
            }
        }
        public IRepository<group> Groups
        {
            get
            {
                if (groupRepository_ == null)
                    groupRepository_ = new groupRepository(db);
                return groupRepository_;
            }
        }

        public IRepository<levelOfEnglish> LevelOfEnglishes
        {
            get
            {
                if (levelOfEnglishRepository_ == null)
                    levelOfEnglishRepository_ = new levelOfEnglishRepository(db);
                return levelOfEnglishRepository_;
            }
        }

        public IRepository<parentsOfStudent> ParentsOfStudents
        {
            get
            {
                if (parentsOfStudentRepository_ == null)
                    parentsOfStudentRepository_ = new parentsOfStudentRepository(db);
                return parentsOfStudentRepository_;
            }
        }

        public IRepository<pause> Pauses
        {
            get
            {
                if (pauseRepository_ == null)
                    pauseRepository_ = new pauseRepository(db);
                return pauseRepository_;
            }
        }

        public IRepository<reminderOrСomments> ReminderOrСomments
        {
            get
            {
                if (reminderOrСommentsRepository_ == null)
                    reminderOrСommentsRepository_ = new reminderOrСommentsRepository(db);
                return reminderOrСommentsRepository_;
            }
        }

        public IRepository<student> Students
        {
            get
            {
                if (studentRepository_ == null)
                    studentRepository_ = new studentRepository(db);
                return studentRepository_;
            }
        }

        public IRepository<studentsInGroup> StudentsInGroups
        {
            get
            {
                if (studentsInGroupRepository_ == null)
                    studentsInGroupRepository_ = new studentsInGroupRepository(db);
                return studentsInGroupRepository_;
            }
        }

        public IRepository<teacher> Teachers
        {
            get
            {
                if (teacherRepository_ == null)
                    teacherRepository_ = new teacherRepository(db);
                return teacherRepository_;
            }
        }

        public IRepository<timetableByDate> TimetableByDates
        {
            get
            {
                if (timetableByDateRepository_ == null)
                    timetableByDateRepository_ = new timetableByDateRepository(db);
                return timetableByDateRepository_;
            }
        }

        public IRepository<waitingList> WaitingLists
        {
            get
            {
                if (waitingListRepository_ == null)
                    waitingListRepository_ = new waitingListRepository(db);
                return waitingListRepository_;
            }
        }

        public IRepository<AdminList> AdminLists
        {
            get
            {
                if (adminListRepository_ == null)
                    adminListRepository_ = new adminListRepository(db);
                return adminListRepository_;
            }
        }
        public IRepository<mainMail> MainMail
        {
            get
            {
                if (mainMailRepository_ == null)
                    mainMailRepository_ = new mainMailRepository(db);
                return mainMailRepository_;
            }
        }

        public IRepository<levelsOfTeachingTeacher> LevelsOfTeachingTeacher
        {
            get
            {
                if (levelsOfTeachingTeacherRepository_ == null)
                    levelsOfTeachingTeacherRepository_ = new levelsOfTeachingTeacherRepository(db);
                return levelsOfTeachingTeacherRepository_;
            }
        }

        public IRepository<openingHour> OpeningHour
        {
            get
            {
                if (openingHourRepository_ == null)
                    openingHourRepository_ = new openingHourRepository(db);
                return openingHourRepository_;
            }
        }

        public IRepository<reminderOrСommentsTeacher> ReminderOrСommentsTeacher
        {
            get
            {
                if (reminderOrСommentsTeacherRepository_ == null)
                    reminderOrСommentsTeacherRepository_ = new reminderOrСommentsTeacherRepository(db);
                return reminderOrСommentsTeacherRepository_;
            }
        }
       

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
