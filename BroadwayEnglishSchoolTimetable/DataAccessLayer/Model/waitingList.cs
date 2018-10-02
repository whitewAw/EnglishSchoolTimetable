namespace DataAccessLayer.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("waitingList")]
    public partial class waitingList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int idStudents { get; set; }

        [Column("dateTarget ", TypeName = "datetime2")]
        public DateTime dateTarget_ { get; set; }

        public int idAdmin { get; set; }

        [StringLength(256)]
        public string notes { get; set; }

        public virtual AdminList AdminList { get; set; }

        public virtual student student { get; set; }
    }
}
