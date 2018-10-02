using System;

namespace BusinessLogicLayer.model
{
    public class accessibilityOfTeacherBL
    {
        public int Id { get; set; }

        public int idTeachers { get; set; }

        public int idDaysOfTheWeek { get; set; }

        public string DaysOfTheWeekName { get; set; }

        public string notes { get; set; }

        public TimeSpan? startTime { get; set; }

        public TimeSpan? endTime { get; set; }
    }
}
