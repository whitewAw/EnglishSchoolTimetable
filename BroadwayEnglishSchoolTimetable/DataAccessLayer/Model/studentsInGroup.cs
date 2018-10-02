namespace DataAccessLayer.Model
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("studentsInGroup")]
    public partial class studentsInGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int idStudents { get; set; }

        public int idGroup { get; set; }

        public virtual group group { get; set; }

        public virtual student student { get; set; }
    }
}
