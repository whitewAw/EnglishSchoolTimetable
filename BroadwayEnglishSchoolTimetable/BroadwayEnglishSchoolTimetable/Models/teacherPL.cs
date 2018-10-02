using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class teacherPL
    {
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string IdTeacher { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 15 символов")]
        public string name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 25 символов")]
        public string surname { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(15, ErrorMessage = "Длина должна составлять до 15 символов")]
        public string patronymic { get; set; }

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

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Начало сотрудничества")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Display(Name = "Окончание сотрудничества")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }

        [Display(Name = "Назначить уроки с сайта?")]
        public bool forBeginners { get; set; }
       
        [Display(Name = "Дата создания")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime dateCreate { get; set; }
    

        [Display(Name = "Имя")]
        public string NameSearch { get; set; }

        [Display(Name = "Фамилия")]
        public string SurNameSearch { get; set; }

        [Display(Name = "Уровени преподавания")]
        public List<levelOfEnglishPL> levelOfEnglish { get; set; }
        public List<int> levelOfEnglishInt { get; set; }

        [Display(Name = "Уровени преподавания")]
        public List<string> NamelevelOfEnglish { get; set; }

        [Display(Name = "Доступный для школы")]
        public List<accessibilityOfTeacherPL> accessibilityOfTeacher { get; set; }
        [Display(Name = "Доступный для школы")]
        public List<accessibilityOfTeacherPL> accessibilityOfTeacherBig { get; set; }

        [Display(Name = "Cотрудник?")]
        public bool inStaff { get; set; }

        [Display(Name = "Уровень")]
        public bool levelEnglish { get; set; }

        public teacherPL()
        {
            levelOfEnglish = new List<levelOfEnglishPL>();
            levelOfEnglishInt = new List<int>();
            NamelevelOfEnglish = new List<string>();
            accessibilityOfTeacher = new List<accessibilityOfTeacherPL>();
            accessibilityOfTeacherBig = new List<accessibilityOfTeacherPL>();
        }
    }
}