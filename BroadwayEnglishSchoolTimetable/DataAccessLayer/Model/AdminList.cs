namespace DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AdminList")]
    public partial class AdminList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminList()
        {
            pauses = new HashSet<pause>();
            reminderOrСomments = new HashSet<reminderOrСomments>();
            timetableByDates = new HashSet<timetableByDate>();
            waitingLists = new HashSet<waitingList>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string IdAdmin { get; set; }

        [StringLength(25)]
        public string surname { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [StringLength(45)]
        public string mail { get; set; }

        [StringLength(14)]
        public string phoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfBirth { get; set; }

        [StringLength(512)]
        public string notes { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateCreate { get; set; }

        public bool isInStaff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pause> pauses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reminderOrСomments> reminderOrСomments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<timetableByDate> timetableByDates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<waitingList> waitingLists { get; set; }
    }
}
