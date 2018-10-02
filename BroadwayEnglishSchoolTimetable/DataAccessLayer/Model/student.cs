namespace DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public student()
        {
            studentsInGroups = new HashSet<studentsInGroup>();
            timetableByDates = new HashSet<timetableByDate>();
            waitingLists = new HashSet<waitingList>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(128)]
        public string IdStudent { get; set; }

        [StringLength(25)]
        public string surname { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [StringLength(15)]
        public string patronymic { get; set; }

        [StringLength(45)]
        public string mail { get; set; }

        [StringLength(14)]
        public string phoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfBirth { get; set; }

        public int? idParent { get; set; }

        public int idFormOfEducation { get; set; }

        public int? idEducationalMaterials { get; set; }

        public int? idLevelOfEnglish { get; set; }

        [StringLength(512)]
        public string notes { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateCreate { get; set; }

        public bool isFirstLesson { get; set; }

        public bool disable { get; set; }

        public virtual educationalMaterial educationalMaterial { get; set; }

        public virtual formOfEducation formOfEducation { get; set; }

        public virtual levelOfEnglish levelOfEnglish { get; set; }

        public virtual parentsOfStudent parentsOfStudent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<studentsInGroup> studentsInGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<timetableByDate> timetableByDates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<waitingList> waitingLists { get; set; }
    }
}
