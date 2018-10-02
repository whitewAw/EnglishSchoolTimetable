using System;
using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class accessibilityOfTeacherPL
    {
        public int Id { get; set; }
        public int idTeachers { get; set; }
        public int idDaysOfTheWeek { get; set; }
        public string DaysOfTheWeekName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }

        [Display(Name = "Начало дня")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? startTime { get; set; }

        [Display(Name = "Конец дня")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? endTime { get; set; }
    }
}