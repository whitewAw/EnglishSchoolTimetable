using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class daysOfTheWeekPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название")]
        [Display(Name = "День недели")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 12 символов")]
        public string day { get; set; }
    }
}