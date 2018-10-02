namespace DataAccessLayer.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mainMail")]
    public partial class mainMail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string mail { get; set; }

        [Required]
        [StringLength(25)]
        public string password { get; set; }
    }
}
