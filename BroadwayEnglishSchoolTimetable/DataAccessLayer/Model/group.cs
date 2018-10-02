namespace DataAccessLayer.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class group
    {
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public group()
        {
            studentsInGroups = new HashSet<studentsInGroup>();
            timetableByDates = new HashSet<timetableByDate>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        public int idEducationalMaterials { get; set; }

        public int idLevelOfEnglish { get; set; }

        [StringLength(512)]
        public string notes { get; set; }

        public bool disable { get; set; }

        public virtual educationalMaterial educationalMaterial { get; set; }

        public virtual levelOfEnglish levelOfEnglish { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<studentsInGroup> studentsInGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<timetableByDate> timetableByDates { get; set; }
    }
}
