using System;
using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class timetableDayPL
    {
        [Display(Name = "Время начала занятия")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? begin { get; set; }
        [Display(Name = "Время окончания занятия")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? end { get; set; }

        [Display(Name = "Учебный кабинет")]
        public int idClassRoom { get; set; }

        [Display(Name = "Преподаватель")]
        public int idTeacher { get; set; }
        public int idDay { get; set; }
        public string NameDay { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }
    }
}

