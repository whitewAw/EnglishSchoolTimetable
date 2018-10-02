namespace DataAccessLayer.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class parentsOfStudent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parentsOfStudent()
        {
            students = new HashSet<student>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25)]
        public string surname { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [StringLength(15)]
        public string patronymic { get; set; }

        [StringLength(45)]
        public string mail { get; set; }

        [Required]
        [StringLength(14)]
        public string phoneNumber { get; set; }

        public bool disable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student> students { get; set; }
    }
}

