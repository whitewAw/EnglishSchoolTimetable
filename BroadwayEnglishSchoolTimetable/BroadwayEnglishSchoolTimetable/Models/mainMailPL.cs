using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class mainMailPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано прочта")]
        [Display(Name = "Почта")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 50 символов")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Пароль")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 25 символов")]
        public string password { get; set; }
    }
}