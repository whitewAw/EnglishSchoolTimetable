﻿namespace DataAccessLayer.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class reminderOrСommentsTeacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int idTeachers { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime date { get; set; }

        [Required]
        [StringLength(512)]
        public string notes { get; set; }

        public virtual teacher teacher { get; set; }
    }
}
