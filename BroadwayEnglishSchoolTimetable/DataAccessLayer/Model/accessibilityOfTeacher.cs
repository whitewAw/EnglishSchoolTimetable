namespace DataAccessLayer.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class accessibilityOfTeacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int idTeachers { get; set; }

        public int idDaysOfTheWeek { get; set; }

        [StringLength(512)]
        public string notes { get; set; }

        public TimeSpan startTime { get; set; }

        public TimeSpan endTime { get; set; }

        public virtual daysOfTheWeek daysOfTheWeek { get; set; }

        public virtual teacher teacher { get; set; }
    }
}
