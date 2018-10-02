using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class formOfEducationPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана форма")]
        [Display(Name = "Форма обучения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 50 символов")]
        public string form { get; set; }
        [Display(Name = "Отключен")]
        public bool disable { get; set; }
    }
}