using System;
using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class openingHourPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано значение")]
        [Display(Name = "Начало рабочего дня")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan open { get; set; }

        [Required(ErrorMessage = "Не указано значение")]
        [Display(Name = "Конец рабочего дня")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan close { get; set; }
    }
}