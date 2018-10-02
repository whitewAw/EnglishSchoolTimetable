namespace DataAccessLayer.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class openingHour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public TimeSpan open { get; set; }

        public TimeSpan close { get; set; }
    }
}
