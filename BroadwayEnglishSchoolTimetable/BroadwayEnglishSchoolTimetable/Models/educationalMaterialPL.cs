using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class educationalMaterialPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название")]
        [Display(Name = "Название учебника/учебного курса")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 50 символов")]
        public string name { get; set; }

        [Display(Name = "Отключен")]
        public bool disable { get; set; }
    }
}