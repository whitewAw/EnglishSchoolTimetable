namespace DataAccessLayer.Model
{
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class levelsOfTeachingTeacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int idTeacher { get; set; }

        public int idLevelOfEnglish { get; set; }

        public virtual levelOfEnglish levelOfEnglish { get; set; }

        public virtual teacher teacher { get; set; }
    }
}
