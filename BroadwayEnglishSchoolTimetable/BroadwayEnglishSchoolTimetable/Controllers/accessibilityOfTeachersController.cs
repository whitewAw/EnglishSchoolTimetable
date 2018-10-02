using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "admin")]
    public class accessibilityOfTeachersController : Controller
    {
    
        IBLService BLService_;
        public accessibilityOfTeachersController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<forCalendarBL, forCalendarPL>(BLService_.forCalendarBL.Get(0)));

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public JsonResult GetResources(string pointStar, string pointEnd)
        {
            DateTime startdate = DateTime.ParseExact(pointStar, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.ParseExact(pointEnd, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int numDay = (int)startdate.DayOfWeek;
            if (numDay == 0)
                numDay = 7;
            var el = Json(Mapper.Map<IEnumerable<teacherBL>, List<teacherPL>>(BLService_.teacherBL.TakeBetweenDates(startdate, enddate)).Select(p => new {
                Id = p.Id,
                Title = p.surname + " " + p.name,
                startT = (p.accessibilityOfTeacher.Where(c => c.idDaysOfTheWeek == numDay).Select(c => c.startTime).FirstOrDefault() == null) ? new TimeSpan(0, 0, 0) : p.accessibilityOfTeacher.Where(c => c.idDaysOfTheWeek == numDay).Select(c => c.startTime).FirstOrDefault(),
                endT = (p.accessibilityOfTeacher.Where(c => c.idDaysOfTheWeek == numDay).Select(c => c.endTime).FirstOrDefault() == null ? new TimeSpan(0, 1, 0) : p.accessibilityOfTeacher.Where(c => c.idDaysOfTheWeek == numDay).Select(c => c.endTime).FirstOrDefault())
            }), JsonRequestBehavior.AllowGet);

            return el;
        }


       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public JsonResult GetEvents(string pointStar, string pointEnd)
        {
            DateTime startdate = DateTime.ParseExact(pointStar, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.ParseExact(pointEnd, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            startdate = new DateTime(startdate.Year, startdate.Month, startdate.Day);
            enddate = new DateTime(enddate.Year, enddate.Month, enddate.Day);

            List<timetableByDatePL> el = Mapper.Map<IEnumerable<timetableByDateBL>, List<timetableByDatePL>>(BLService_.timetableByDateBL.TakeBetweenDates(startdate, enddate));
            var elEvent = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDatePL, classEvent>()
           .ForMember("id", opt => opt.MapFrom(c => c.Id))
           .ForMember("resourceId", opt => opt.MapFrom(c => c.idTeacher))
           .ForMember("title", opt => opt.MapFrom(c =>c.ClassRoom+"\n"+c.Student+c.Group + "\n" + c.EducationalMaterials + "\n"+ c.notes))
           .ForMember("start", opt => opt.MapFrom(c => classEvent.ConvertToUnixTimestamp(c.beginTime.Value)))
            .ForMember("end", opt => opt.MapFrom(c => classEvent.ConvertToUnixTimestamp(c.endTime.Value)))
            .ForMember("color", opt => opt.MapFrom(c => c.isCanceled==true?ColorEvent.canceled:c.Student!=null? ColorEvent.individual: ColorEvent.group))
            .ForMember("textColor", opt => opt.MapFrom(c => c.isCanceled == true ? ColorEventText.canceled : c.Student != null ? ColorEventText.individual : ColorEventText.group))
           ).CreateMapper().Map< List<timetableByDatePL>, List<classEvent>>(el);
           return Json(elEvent, JsonRequestBehavior.AllowGet);
        }
    }
}
