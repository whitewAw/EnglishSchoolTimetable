namespace DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public teacher()
        {
            accessibilityOfTeachers = new HashSet<accessibilityOfTeacher>();
            levelsOfTeachingTeachers = new HashSet<levelsOfTeachingTeacher>();
            reminderOr—ommentsTeacher = new HashSet<reminderOr—ommentsTeacher>();
            timetableByDates = new HashSet<timetableByDate>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string IdTeacher { get; set; }

        [StringLength(25)]
        public string surname { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [StringLength(15)]
        public string patronymic { get; set; }

        [Required]
        [StringLength(45)]
        public string mail { get; set; }

        [Required]
        [StringLength(14)]
        public string phoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateOfBirth { get; set; }

        [StringLength(1024)]
        public string notes { get; set; }

        [Column(TypeName = "date")]
        public DateTime startDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? endDate { get; set; }

        public bool forBeginners { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateCreate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<accessibilityOfTeacher> accessibilityOfTeachers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<levelsOfTeachingTeacher> levelsOfTeachingTeachers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reminderOr—ommentsTeacher> reminderOr—ommentsTeacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<timetableByDate> timetableByDates { get; set; }
    }
}
