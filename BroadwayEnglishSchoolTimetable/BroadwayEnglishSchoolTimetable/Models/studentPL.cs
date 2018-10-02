using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class studentPL
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string IdStudent { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 25 символов")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина должна составлять от 3 до 15 символов")]
        public string name { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(15, ErrorMessage = "Длина должна составлять до 15 символов")]
        public string patronymic { get; set; }

       // [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Mail")]
        [EmailAddress(ErrorMessage = "Недействительный адрес")]
        [StringLength(45)]
        public string mail { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Недействительный номер телефона")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public string phoneNumber { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        [Display(Name = "Родитель")]
        public int? idParent { get; set; }

        [Display(Name = "Форма обучения")]
        public int idFormOfEducation { get; set; }

        [Display(Name = "Учебные матириалы")]
        public int? idEducationalMaterials { get; set; }

        [Display(Name = "Уровень владения английским")]
        public int? idLevelOfEnglish { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }

        [Display(Name = "Дата создания учетки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime dateCreate { get; set; }

        [Display(Name = "Первое занятие")]
        public bool isFirstLesson { get; set; }

        [Display(Name = "Родитель")]
        public string Parent { get; set; }

        [Display(Name = "Форма обучения")]
        public string FormOfEducation { get; set; }

        [Display(Name = "Учебные матириалы")]
        public string EducationalMaterials { get; set; }

        [Display(Name = "Уровень владения английским")]
        public string LevelOfEnglish { get; set; }
        [Display(Name = "Отключен")]
        public bool disable { get; set; }

        public string FullName
        {
            get
            {
                return (surname + " " + name+" "+ patronymic).Trim();
            }
        }

        [Display(Name = "История занятий")]
        public List<timetableByDatePL> timeTableHistory { get; set; }
        [Display(Name = "Будущие занятия")]
        public List<timetableByDatePL> nextLesson { get; set; }
        public studentPL()
        {
            timeTableHistory = new List<timetableByDatePL>();
            nextLesson = new List<timetableByDatePL>();
        }



    }
}