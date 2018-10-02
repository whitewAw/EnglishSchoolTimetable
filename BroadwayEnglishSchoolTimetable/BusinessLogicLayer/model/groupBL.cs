using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class groupBL
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<studentBL> student { get; set; }
        public List<studentBL> studentBig { get; set; }
        public List<int> studentSelect { get; set; }
        public int idEducationalMaterials { get; set; }
        public string nameEducationalMaterials { get; set; }
        public List<timetableByDateBL> timeTableHistory { get; set; }
        public List<timetableByDateBL> nextLesson { get; set; }
        public string levelOfEnglish { get; set; }

        public string notes { get; set; }
        public bool disable { get; set; }
        public int idLevelOfEnglish { get; set; }

        public groupBL()
        {
            student = new List<studentBL>();
            studentBig = new List<studentBL>();
            studentSelect = new List<int>();
            timeTableHistory = new List<timetableByDateBL>();
            nextLesson = new List<timetableByDateBL>();
        }
    }
}
