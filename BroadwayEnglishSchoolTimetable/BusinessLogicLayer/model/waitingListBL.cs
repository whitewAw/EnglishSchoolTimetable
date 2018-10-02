using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class waitingListBL
    {
        public int Id { get; set; }
        public int idStudents { get; set; }
        public DateTime dateTarget_ { get; set; }
        public int idAdmin { get; set; }
        public string notes { get; set; }
        public studentBL studentEl { get; set; }
        public AdminListBL adminEl { get; set; }

        public bool? isCanceled { get; set; }



    }
}
