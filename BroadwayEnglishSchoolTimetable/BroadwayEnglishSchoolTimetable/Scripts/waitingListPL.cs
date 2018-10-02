using System;
using System.ComponentModel.DataAnnotations;


namespace BroadwayEnglishSchoolTimetable.Models
{
    public class waitingListPL
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Студент")]
        [Range(1, int.MaxValue, ErrorMessage = "Не выбрано значение")]
        public int idStudents { get; set; }


        [Display(Name = "Целевая дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateTarget_ { get; set; }

        //[Required]
        [Display(Name = "Администратор")]
        //[Range(1, int.MaxValue, ErrorMessage = "Не выбрано значение")]
        public int idAdmin { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "Длина должна составлять от 2 до 256 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }

        [Display(Name = "Студент")]
        public studentPL studentEl { get; set; }

        [Display(Name = "Администратор")]
        public AdminListPL adminEl { get; set; }

        public string FullNameStud
        {
            get
            {
                return (studentEl.surname + " " + studentEl.name + " " + studentEl.patronymic).Trim();
            }
        }

        public string FullNameAdmin
        {
            get
            {
                return (adminEl.surname + " " + adminEl.name + " ").Trim();
            }
        }

    }
}