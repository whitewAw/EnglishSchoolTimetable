using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class levelOfEnglishPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан уровень")]
        [Display(Name = "Уровень английского языка")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 25 символов")]
        public string level { get; set; }
        [Display(Name = "Отключен")]
        public bool disable { get; set; }
    }
}
