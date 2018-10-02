using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class forCalendarBL
    {
        public openingHourBL openingHour { get; set; }
        public List<waitingListBL> waitingList { get; set; }
        public List<reminderOrСommentsBL> reminderOrСomments { get; set; }
        public List<reminderOrСommentsTeacherBL> reminderOrСommentsTeacher { get; set; }

        public forCalendarBL()
        {
            waitingList = new List<waitingListBL>();
            reminderOrСomments = new List<reminderOrСommentsBL>();
            reminderOrСommentsTeacher = new List<reminderOrСommentsTeacherBL>();
        }
    }
}
