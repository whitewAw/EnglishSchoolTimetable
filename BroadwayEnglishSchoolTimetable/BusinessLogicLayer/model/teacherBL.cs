using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.model
{
    public class teacherBL
    {
        public int Id { get; set; }
        public string IdTeacher { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string mail { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string notes { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool forBeginners { get; set; }
        public DateTime dateCreate { get; set; }
        public List<levelOfEnglishBL> levelOfEnglish { get; set; }
        public List<string> NamelevelOfEnglish { get; set; }
        public List<int> levelOfEnglishInt { get; set; }
        public List<accessibilityOfTeacherBL> accessibilityOfTeacher { get; set; }
        public List<accessibilityOfTeacherBL> accessibilityOfTeacherBig { get; set; }

        public teacherBL()
        {
            levelOfEnglish = new List<levelOfEnglishBL>();
            levelOfEnglishInt = new List<int>();
            NamelevelOfEnglish = new List<string>();
            accessibilityOfTeacher = new List<accessibilityOfTeacherBL>();
            accessibilityOfTeacherBig = new List<accessibilityOfTeacherBL>();
        }
    }
}
