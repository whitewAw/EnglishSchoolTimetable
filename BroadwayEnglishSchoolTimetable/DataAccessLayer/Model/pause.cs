namespace DataAccessLayer.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("pause")]
    public partial class pause
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int idTimetableByDate { get; set; }

        public int idCauseOfDisruption { get; set; }

        public virtual causeOfDisruption causeOfDisruption { get; set; }

        public virtual timetableByDate timetableByDate { get; set; }
    }
}
