using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class parentsOfStudentPL
    {
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(25, ErrorMessage = "Максимальная длина 25 символов")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 15 символов")]
        public string name { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(15, ErrorMessage = "Максимальная длина 15 символов")]
        public string patronymic { get; set; }

        [Display(Name = "Mail")]
        [EmailAddress(ErrorMessage = "Недействительный адрес")]
        [StringLength(45)]
        public string mail { get; set; }

        [Required(ErrorMessage = "Вы должны указать номер телефона")]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Недействительный номер телефона")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]

        public string phoneNumber { get; set; }
        [Display(Name = "Фамилия")]
        public string SurNameSearch { get; set; }
        [Display(Name = "Имя")]
        public string NameSearch { get; set; }
        [Display(Name = "Отключен")]
        public bool disable { get; set; }

    }
}
