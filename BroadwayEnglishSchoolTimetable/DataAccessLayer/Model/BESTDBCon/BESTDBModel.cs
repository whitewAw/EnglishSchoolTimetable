using System.Data.Entity;
using DataAccessLayer.Model.BESTDBCon;
namespace DataAccessLayer.Model
{
    public partial class BESTDBModel : DbContext
    {
        public BESTDBModel(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;

        }
        static BESTDBModel()
        {
           Database.SetInitializer<BESTDBModel>(new DataLevelDbInitializer());
        }
        public virtual DbSet<accessibilityOfTeacher> accessibilityOfTeachers { get; set; }
        public virtual DbSet<AdminList> AdminLists { get; set; }
        public virtual DbSet<causeOfDisruption> causeOfDisruptions { get; set; }
        public virtual DbSet<classroom> classrooms { get; set; }
        public virtual DbSet<daysOfTheWeek> daysOfTheWeeks { get; set; }
        public virtual DbSet<educationalMaterial> educationalMaterials { get; set; }
        public virtual DbSet<formOfEducation> formOfEducations { get; set; }
        public virtual DbSet<group> groups { get; set; }
        public virtual DbSet<levelOfEnglish> levelOfEnglishes { get; set; }
        public virtual DbSet<levelsOfTeachingTeacher> levelsOfTeachingTeachers { get; set; }
        public virtual DbSet<mainMail> mainMails { get; set; }
        public virtual DbSet<openingHour> openingHours { get; set; }
        public virtual DbSet<parentsOfStudent> parentsOfStudents { get; set; }
        public virtual DbSet<pause> pauses { get; set; }
        public virtual DbSet<reminderOrСomments> reminderOrСomments { get; set; }
        public virtual DbSet<reminderOrСommentsTeacher> reminderOrСommentsTeacher { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<studentsInGroup> studentsInGroups { get; set; }
        public virtual DbSet<teacher> teachers { get; set; }
        public virtual DbSet<timetableByDate> timetableByDates { get; set; }
        public virtual DbSet<waitingList> waitingLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accessibilityOfTeacher>()
                .Property(e => e.startTime)
                .HasPrecision(0);

            modelBuilder.Entity<accessibilityOfTeacher>()
                .Property(e => e.endTime)
                .HasPrecision(0);

            modelBuilder.Entity<AdminList>()
                .HasMany(e => e.reminderOrСomments)
                .WithRequired(e => e.AdminList)
                .HasForeignKey(e => e.idAdmin);

            modelBuilder.Entity<AdminList>()
                .HasMany(e => e.timetableByDates)
                .WithRequired(e => e.AdminList)
                .HasForeignKey(e => e.idAdmin);

            modelBuilder.Entity<AdminList>()
                .HasMany(e => e.waitingLists)
                .WithRequired(e => e.AdminList)
                .HasForeignKey(e => e.idAdmin);

            modelBuilder.Entity<causeOfDisruption>()
                .HasMany(e => e.pauses)
                .WithRequired(e => e.causeOfDisruption)
                .HasForeignKey(e => e.idCauseOfDisruption);

            modelBuilder.Entity<classroom>()
                .HasMany(e => e.timetableByDates)
                .WithRequired(e => e.classroom)
                .HasForeignKey(e => e.idClassRoom);

            modelBuilder.Entity<daysOfTheWeek>()
                .HasMany(e => e.accessibilityOfTeachers)
                .WithRequired(e => e.daysOfTheWeek)
                .HasForeignKey(e => e.idDaysOfTheWeek);

            modelBuilder.Entity<educationalMaterial>()
                .HasMany(e => e.groups)
                .WithRequired(e => e.educationalMaterial)
                .HasForeignKey(e => e.idEducationalMaterials);

            modelBuilder.Entity<educationalMaterial>()
                .HasMany(e => e.students)
                .WithOptional(e => e.educationalMaterial)
                .HasForeignKey(e => e.idEducationalMaterials)
                .WillCascadeOnDelete();

            modelBuilder.Entity<formOfEducation>()
                .HasMany(e => e.students)
                .WithRequired(e => e.formOfEducation)
                .HasForeignKey(e => e.idFormOfEducation);

            modelBuilder.Entity<group>()
                .HasMany(e => e.studentsInGroups)
                .WithRequired(e => e.group)
                .HasForeignKey(e => e.idGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group>()
                .HasMany(e => e.timetableByDates)
                .WithOptional(e => e.group)
                .HasForeignKey(e => e.idGroup)
                .WillCascadeOnDelete();

            modelBuilder.Entity<levelOfEnglish>()
                .HasMany(e => e.groups)
                .WithRequired(e => e.levelOfEnglish)
                .HasForeignKey(e => e.idLevelOfEnglish);

            modelBuilder.Entity<levelOfEnglish>()
                .HasMany(e => e.levelsOfTeachingTeachers)
                .WithRequired(e => e.levelOfEnglish)
                .HasForeignKey(e => e.idLevelOfEnglish);

            modelBuilder.Entity<levelOfEnglish>()
                .HasMany(e => e.students)
                .WithOptional(e => e.levelOfEnglish)
                .HasForeignKey(e => e.idLevelOfEnglish)
                .WillCascadeOnDelete();

            modelBuilder.Entity<parentsOfStudent>()
                .HasMany(e => e.students)
                .WithOptional(e => e.parentsOfStudent)
                .HasForeignKey(e => e.idParent)
                .WillCascadeOnDelete();

            modelBuilder.Entity<reminderOrСomments>()
                .Property(e => e.date)
                .HasPrecision(0);

            modelBuilder.Entity<reminderOrСommentsTeacher>()
                .Property(e => e.date)
                .HasPrecision(0);

            modelBuilder.Entity<student>()
                .HasMany(e => e.studentsInGroups)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.idStudents);

            modelBuilder.Entity<student>()
                .HasMany(e => e.timetableByDates)
                .WithOptional(e => e.student)
                .HasForeignKey(e => e.idStudent);

            modelBuilder.Entity<student>()
                .HasMany(e => e.waitingLists)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.idStudents);

            modelBuilder.Entity<teacher>()
                .Property(e => e.phoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<teacher>()
                .HasMany(e => e.accessibilityOfTeachers)
                .WithRequired(e => e.teacher)
                .HasForeignKey(e => e.idTeachers);

            modelBuilder.Entity<teacher>()
                .HasMany(e => e.levelsOfTeachingTeachers)
                .WithRequired(e => e.teacher)
                .HasForeignKey(e => e.idTeacher);

            modelBuilder.Entity<teacher>()
                .HasMany(e => e.reminderOrСommentsTeacher)
                .WithRequired(e => e.teacher)
                .HasForeignKey(e => e.idTeachers);

            modelBuilder.Entity<teacher>()
                .HasMany(e => e.timetableByDates)
                .WithRequired(e => e.teacher)
                .HasForeignKey(e => e.idTeacher);

            modelBuilder.Entity<timetableByDate>()
                .Property(e => e.beginTime)
                .HasPrecision(0);

            modelBuilder.Entity<timetableByDate>()
                .Property(e => e.endTime)
                .HasPrecision(0);

            modelBuilder.Entity<timetableByDate>()
                .HasMany(e => e.pauses)
                .WithRequired(e => e.timetableByDate)
                .HasForeignKey(e => e.idTimetableByDate);

            modelBuilder.Entity<waitingList>()
                .Property(e => e.dateTarget_)
                .HasPrecision(0);
        }
    }
}
