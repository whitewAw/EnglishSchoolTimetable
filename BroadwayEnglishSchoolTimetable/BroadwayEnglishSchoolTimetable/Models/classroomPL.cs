using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class classroomPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан кабинет")]
        [Display(Name = "Учебный кабинет")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина должна составлять от 2 до 30 символов")]
        public string name { get; set; }
        [Display(Name = "Отключен")]
        public bool disable { get; set; }
    }
}