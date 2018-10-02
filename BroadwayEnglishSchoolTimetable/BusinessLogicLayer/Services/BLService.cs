using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using BusinessLogicLayer.Repository;
using DataAccessLayer.Interfaces;
using System;


namespace BusinessLogicLayer.Services
{
    public class BLService : IBLService, IDisposable
    {
        private IUnitOfWork Database;
        public BLService(IUnitOfWork uow) => Database = uow;

        private AdminsListBLRepository adminsListBLRepository_;
        private mainMailBLRepository mainMailBLRepository_;
        private parentsOfStudentBLRepository parentsOfStudentBLRepository_;
        private educationalMaterialBLRepository educationalMaterialBLRepository_;
        private levelOfEnglishBLRepository levelOfEnglishBLRepository_;
        private formOfEducationBLRepository formOfEducationBLRepository_;
        private daysOfTheWeekBLRepository daysOfTheWeekBLRepository_;
        private groupBLRepository groupBLRepository_;
        private teacherBLRepository teacherBLRepository_;
        private openingHourBLRepository openingHourBLRepository_;
        private studentBLRepository studentBLRepository_;
        private classroomBLRepository classroomBLRepository_;
        private causeOfDisruptionBLRepository causeOfDisruptionBLRepository_;
        private reminderOrСommentsBLRepository reminderOrСommentsBLRepository_;
        private waitingListBLRepository waitingListBLRepository;
        private timetableByDateBLRepository timetableByDateBLRepository;
        private forCalendarBLRepository forCalendarBLRepository;
        private reminderOrСommentsTeacherBLRepository reminderOrСommentsTeacherBLRepository;


        public IRepositoryBL<AdminListBL> adminListBL
       {
            get
            {
                if (adminsListBLRepository_ == null)
                    adminsListBLRepository_ = new AdminsListBLRepository(ref Database);
                return adminsListBLRepository_;
            }

        }
        public IRepositoryBL<mainMailBL> mainMailBL
        {
            get
            {
                if (mainMailBLRepository_ == null)
                    mainMailBLRepository_ = new mainMailBLRepository(ref Database);
                return mainMailBLRepository_;
            }
        }
        public IRepositoryBL<parentsOfStudentBL> parentsOfStudentBL {
            get
            {
                if(parentsOfStudentBLRepository_==null)
                    parentsOfStudentBLRepository_= new parentsOfStudentBLRepository(ref Database);
                return parentsOfStudentBLRepository_;
            }
        }
        public IRepositoryBL<educationalMaterialBL> educationalMaterialBL
        {
            get
            {
                if(educationalMaterialBLRepository_==null)
                    educationalMaterialBLRepository_=new educationalMaterialBLRepository(ref Database);
                return educationalMaterialBLRepository_;
            }
        }
        public IRepositoryBL<levelOfEnglishBL> levelOfEnglishBL
        {
            get
            {
                if(levelOfEnglishBLRepository_==null)
                    levelOfEnglishBLRepository_=new levelOfEnglishBLRepository(ref Database);
                return levelOfEnglishBLRepository_;
            }
        }

        public IRepositoryBL<formOfEducationBL> formOfEducationBL
        {
            get
            {
                if (formOfEducationBLRepository_ == null)
                    formOfEducationBLRepository_ = new formOfEducationBLRepository(ref Database);
                return formOfEducationBLRepository_;
            }
        }

        public IRepositoryBL<daysOfTheWeekBL> daysOfTheWeekBL
        {
            get
            {
                if (daysOfTheWeekBLRepository_ == null)
                    daysOfTheWeekBLRepository_ = new daysOfTheWeekBLRepository(ref Database);
                return daysOfTheWeekBLRepository_;
            }
        }

        public IRepositoryBL<groupBL> groupBL
        {
            get
            {
                if (groupBLRepository_ == null)
                    groupBLRepository_ = new groupBLRepository(ref Database);
                return groupBLRepository_;
            }
        }

        public IRepositoryBL<teacherBL> teacherBL
        {
            get
            {
                if (teacherBLRepository_ == null)
                    teacherBLRepository_ = new teacherBLRepository(ref Database);
                return teacherBLRepository_;
            }
        }

        public IRepositoryBL<openingHourBL> openingHourBL
        {
            get
            {
                if (openingHourBLRepository_ == null)
                    openingHourBLRepository_ = new openingHourBLRepository(ref Database);
                return openingHourBLRepository_;
            }
        }

        public IRepositoryBL<studentBL> studentBL
        {
            get
            {
                if (studentBLRepository_ == null)
                    studentBLRepository_ = new studentBLRepository(ref Database);
                return studentBLRepository_;
            }
        }

        public IRepositoryBL<classroomBL> classroomBL
        {
            get
            {
                if (classroomBLRepository_ == null)
                    classroomBLRepository_ = new classroomBLRepository(ref Database);
                return classroomBLRepository_;
            }
        }

        public IRepositoryBL<causeOfDisruptionBL> causeOfDisruptionBL
        {
            get
            {
                if (causeOfDisruptionBLRepository_ == null)
                    causeOfDisruptionBLRepository_ = new causeOfDisruptionBLRepository(ref Database);
                return causeOfDisruptionBLRepository_;
            }
        }

        public IRepositoryBL<reminderOrСommentsBL> reminderOrСommentsBL
        {
            get
            {
                if (reminderOrСommentsBLRepository_ == null)
                    reminderOrСommentsBLRepository_ = new reminderOrСommentsBLRepository(ref Database);
                return reminderOrСommentsBLRepository_;
            }
        }

        public IRepositoryBL<waitingListBL> waitingListBL
        {
            get
            {
                if (waitingListBLRepository == null)
                    waitingListBLRepository = new waitingListBLRepository(ref Database);
                return waitingListBLRepository;
            }
        }

        public IRepositoryBL<timetableByDateBL> timetableByDateBL
        {
            get
            {
                if (timetableByDateBLRepository == null)
                    timetableByDateBLRepository = new timetableByDateBLRepository(ref Database);
                return timetableByDateBLRepository;
            }
        }

        public IRepositoryBL<forCalendarBL> forCalendarBL
        {
            get
            {
                if (forCalendarBLRepository == null)
                    forCalendarBLRepository = new forCalendarBLRepository(ref Database);
                return forCalendarBLRepository;
            }
        }

        public IRepositoryBL<reminderOrСommentsTeacherBL> reminderOrСommentsTeacherBL
        {
            get
            {
                if (reminderOrСommentsTeacherBLRepository == null)
                    reminderOrСommentsTeacherBLRepository = new reminderOrСommentsTeacherBLRepository(ref Database);
                return reminderOrСommentsTeacherBLRepository;
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Database.Dispose();
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
