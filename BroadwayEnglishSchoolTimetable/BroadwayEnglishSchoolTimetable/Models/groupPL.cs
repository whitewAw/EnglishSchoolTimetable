using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class groupPL
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Уровень английского языка")]
        public string levelOfEnglish { get; set; }

        [Required(ErrorMessage = "Не укзано название")]
        [Display(Name = "Название группы")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 30 символов")]
        public string name { get; set; }

        [Display(Name = "Студенты в группе")]
        public List<studentPL> student { get; set; }

        [Display(Name = "Студенты в группе")]
        public List<studentPL> studentBig { get; set; }

        public List<int> studentSelect { get; set; }

        [Display(Name = "Группа")]
        public string GroupSearch { get; set; }

        [Display(Name = "Студент")]
        public string StudentSearch { get; set; }

        [Required]
        [Display(Name = "Учебные матириалы")]
        [Range(1, int.MaxValue, ErrorMessage = "Не выбрано значение")]
        public int idEducationalMaterials { get; set; }
        public string nameEducationalMaterials { get; set; }

        [Display(Name = "История занятий")]
        public List<timetableByDatePL> timeTableHistory { get; set; }
        [Display(Name = "Будущие занятия")]
        public List<timetableByDatePL> nextLesson { get; set; }



        [Display(Name = "Отключен")]
        public bool disable { get; set; }
        
        [DataType(DataType.MultilineText)]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }

        [Required]
        [Display(Name = "Уровень английского языка")]
        [Range(1, int.MaxValue, ErrorMessage = "Не выбрано значение")]
        public int idLevelOfEnglish { get; set; }

        [Display(Name = "Название группы")]
        public string nameGroup 
        {
            get
            {
                return (levelOfEnglish + " " + name);
            }
        }




        public groupPL()
        {
            student = new List<studentPL>();
            studentBig = new List<studentPL>();
            studentSelect = new List<int>();
            timeTableHistory = new List<timetableByDatePL>();
            nextLesson = new List<timetableByDatePL>();
        }

    }
}