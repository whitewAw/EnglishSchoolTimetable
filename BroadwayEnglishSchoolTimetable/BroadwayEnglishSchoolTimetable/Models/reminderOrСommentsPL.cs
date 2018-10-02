using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace BroadwayEnglishSchoolTimetable.Models
{
    public class reminderOrСommentsPL
    {
        public int Id { get; set; }

        [Display(Name = "Администратор")]
        [HiddenInput(DisplayValue = false)]
        public int idAdmin { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string idAdminStr { get; set; }
        [Display(Name = "Автор заметки")]
        public string AdminName { get; set; }

        [Display(Name = "Дата напоминания")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Не указана дата")]
        [Display(Name = "Дата напоминания")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime? datePL { get; set; }

        [Required(ErrorMessage = "Не указано время")]
        [Display(Name = "Время напоминания")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? timePL { get; set; }


        [Required(ErrorMessage = "Не заполненно")]
        [DataType(DataType.MultilineText)]
        [StringLength(512, MinimumLength = 2, ErrorMessage = "Длина должна составлять от 2 до 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }
    }
}