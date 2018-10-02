using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class forCalendarPL
    {
        public openingHourPL openingHour { get; set; }

        [Display(Name = "Cписок ожидания")]
        public List<waitingListPL> waitingList { get; set; }

        [Display(Name = "Напоминания")]
        public List<reminderOrСommentsPL> reminderOrСomments { get; set; }

        [Display(Name = "Напоминания")]
        public List<reminderOrСommentsTeacherPL> reminderOrСommentsTeacher { get; set; }
        public forCalendarPL()
        {
            waitingList = new List<waitingListPL>();
            reminderOrСomments = new List<reminderOrСommentsPL>();
            reminderOrСommentsTeacher = new List<reminderOrСommentsTeacherPL>();
        }

    }
}