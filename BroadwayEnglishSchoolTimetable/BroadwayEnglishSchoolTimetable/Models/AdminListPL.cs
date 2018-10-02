using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class AdminListPL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 15 символов")]
        public string name { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 25 символов")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
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

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }
     
        [Display(Name = "Работает?")]
        public bool isInStaff { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime dateCreate { get; set; }
        [HiddenInput(DisplayValue = false)]

        [Display(Name = "Имя")]
        public string NameSearch { get; set; }

        [Display(Name = "Фамилия")]
        public string SurNameSearch { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string IdAdmin { get; set; }
    }
}