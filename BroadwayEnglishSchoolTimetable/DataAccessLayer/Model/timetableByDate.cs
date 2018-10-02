namespace DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("timetableByDate")]
    public partial class timetableByDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public timetableByDate()
        {
            pauses = new HashSet<pause>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime beginTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime endTime { get; set; }

        public int idClassRoom { get; set; }

        public int idTeacher { get; set; }

        public int? idGroup { get; set; }

        public int? idStudent { get; set; }

        public bool isCanceled { get; set; }

        [StringLength(512)]
        public string notes { get; set; }

        public int uniqueManyNumber { get; set; }

        public int idAdmin { get; set; }

        [StringLength(512)]
        public string passedMaterials { get; set; }

        [StringLength(512)]
        public string homework { get; set; }

        public virtual AdminList AdminList { get; set; }

        public virtual classroom classroom { get; set; }

        public virtual group group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pause> pauses { get; set; }

        public virtual student student { get; set; }

        public virtual teacher teacher { get; set; }
    }
}
