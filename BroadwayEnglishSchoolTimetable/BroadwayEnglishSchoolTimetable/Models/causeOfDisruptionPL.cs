using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class causeOfDisruptionPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана причина")]
        [Display(Name = "Причина отмены занятия")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна составлять от 2 до 50 символов")]
        public string cause { get; set; }

        [Display(Name = "Отключен")]
        public bool disable { get; set; }
    }
}