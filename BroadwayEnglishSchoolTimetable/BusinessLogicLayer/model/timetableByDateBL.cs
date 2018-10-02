using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class timetableByDateBL
    {
        public int Id { get; set; }
        public DateTime? beginTime { get; set; }
        public DateTime? endTime { get; set; }
        public int idClassRoom { get; set; }
        public int idTeacher { get; set; }
        public int idGroup { get; set; }
        public int idStudent { get; set; }
        public bool isCanceled { get; set; }
        public string notes { get; set; }
        public int uniqueManyNumber { get; set; }
        public int idAdmin { get; set; }



        public string ClassRoom { get; set; }
        public string Teacher { get; set; }
        public string Group { get; set; }
        public string Student { get; set; }
        public string Admin { get; set; }


        public causeOfDisruptionBL causeOfDisruption { get; set; }
        public int idcauseOfDisruption { get; set; }

        public List<timetableDayBL> timetableDays { get; set; }

        public timetableByDateBL()
        {
            timetableDays = new List<timetableDayBL>();
        }

        public TimeSpan begin { get; set; }
        public TimeSpan end { get; set; }
        public string passedMaterials { get; set; }
        public string homework { get; set; }

        public string EducationalMaterials { get; set; }


    }
}
