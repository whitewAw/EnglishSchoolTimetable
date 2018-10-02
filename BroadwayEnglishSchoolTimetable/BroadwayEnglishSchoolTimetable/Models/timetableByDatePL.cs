using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BroadwayEnglishSchoolTimetable.Models
{
    public class timetableByDatePL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Дата первого занятия")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? beginTime { get; set; }

        //[Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Дата последнего занятия")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? endTime { get; set; }
        [Display(Name = "Кабинет")]
        public int idClassRoom { get; set; }
        [Display(Name = "Преподователь")]
        public int idTeacher { get; set; }

        [Display(Name = "Группа (групповое занятие)")]
        public int idGroup { get; set; }
        [Display(Name = "Студент (индивидульное занятие)")]
        public int idStudent { get; set; }
        [Display(Name = "Отмененное")]
        public bool isCanceled { get; set; }


        [Display(Name = "Учебные материалы")]
        public string EducationalMaterials { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        [Display(Name = "Заметки")]
        public string notes { get; set; }
        public int uniqueManyNumber { get; set; }
        [Display(Name = "Администратор")]
        public int idAdmin { get; set; }

        public string ClassRoom { get; set; }
        public string Teacher { get; set; }
        public string Group { get; set; }
        public string Student { get; set; }
        public string Admin { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время начала занятия")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan begin { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время окончания занятия")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan end { get; set; }

        [Display(Name = "Продол-жительность (мин.)")]
        public int TimeofMinuts
        {
            get
            {
                return (endTime == null || beginTime==null)?0:(int)(endTime - beginTime).Value.TotalMinutes;
            }
        }

        public causeOfDisruptionPL causeOfDisruption { get; set; }
        public string NameCauseOfDisruption { get; set; }

        [Display(Name = "По дням")]
        public List<timetableDayPL> timetableDays { get; set; }

        public timetableByDatePL()
        {
            timetableDays = new List<timetableDayPL>();
        }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Пройденные материалы")]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        public string passedMaterials { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Домашнее задание")]
        [StringLength(512, ErrorMessage = "Максимум 512 символов")]
        public string homework { get; set; }
    }
}