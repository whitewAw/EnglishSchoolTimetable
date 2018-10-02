using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class reminderOrСommentsBL
    {
        public int Id { get; set; }
        public int idAdmin { get; set; }
        public string idAdminStr { get; set; }
        public string AdminName { get; set; }
        public DateTime date { get; set; }
        public DateTime datePL { get; set; }
        public TimeSpan timePL { get; set; }
        public string notes { get; set; }
    }
}
