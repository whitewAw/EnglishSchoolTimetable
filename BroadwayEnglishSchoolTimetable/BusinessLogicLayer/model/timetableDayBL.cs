using System;

namespace BusinessLogicLayer.model
{
    public class timetableDayBL
    {
        public TimeSpan? begin { get; set; }
        public TimeSpan? end { get; set; }
        public int idClassRoom { get; set; }
        public int idTeacher { get; set; }
        public int idDay { get; set; }
        public string NameDay { get; set; }
        public string notes { get; set; }
     
    }
}
