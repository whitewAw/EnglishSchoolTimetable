using System;

namespace BusinessLogicLayer.model
{
    public class reminderOrСommentsTeacherBL
    {
        public int Id { get; set; }
        public int idTeachers { get; set; }
        public string TeachersName { get; set; }
        public DateTime date { get; set; }
        public DateTime? datePL { get; set; }
        public TimeSpan? timePL { get; set; }
        public string notes { get; set; }
    }
}
