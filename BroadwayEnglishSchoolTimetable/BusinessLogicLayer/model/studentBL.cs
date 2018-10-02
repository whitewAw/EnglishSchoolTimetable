using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class studentBL
    {
        public int Id { get; set; }
        public string IdStudent { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string mail { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public int? idParent { get; set; }
        public int idFormOfEducation { get; set; }
        public int? idEducationalMaterials { get; set; }
        public int? idLevelOfEnglish { get; set; }
        public string notes { get; set; }
        public DateTime dateCreate { get; set; }
        public bool isFirstLesson { get; set; }
        public string Parent { get; set; }
        public string FormOfEducation { get; set; }
        public string EducationalMaterials { get; set; }
        public string LevelOfEnglish { get; set; }
        public bool disable { get; set; }


        public List<timetableByDateBL> timeTableHistory { get; set; }
        public List<timetableByDateBL> nextLesson { get; set; }
        public studentBL()
        {
            timeTableHistory = new List<timetableByDateBL>();
            nextLesson = new List<timetableByDateBL>();
        }

        public IEnumerable<studentBL> toList()
        {
            throw new NotImplementedException();
        }
    }
}
