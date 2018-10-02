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
    public class accessibilityOfRoomController : Controller
    {

        IBLService BLService_;
        public accessibilityOfRoomController(IBLService serv)
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
            var timeShcool = BLService_.openingHourBL.Get(1);


            return Json(Mapper.Map<IEnumerable<classroomBL>, List<classroomPL>>(BLService_.classroomBL.GetAll().Where(p=>p.disable==false)).Select(p => new {
                Id = p.Id,
                Title = p.name,
                startT = timeShcool.open,
                endT = timeShcool.close
            }), JsonRequestBehavior.AllowGet);
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
           .ForMember("resourceId", opt => opt.MapFrom(c => c.idClassRoom))
           .ForMember("title", opt => opt.MapFrom(c => c.Teacher + "\n" + c.Student + c.Group + "\n"+c.EducationalMaterials+"\n" + c.notes))
           .ForMember("start", opt => opt.MapFrom(c => classEvent.ConvertToUnixTimestamp(c.beginTime.Value)))
           .ForMember("end", opt => opt.MapFrom(c => classEvent.ConvertToUnixTimestamp(c.endTime.Value)))
           .ForMember("color", opt => opt.MapFrom(c => c.isCanceled == true ? ColorEvent.canceled : c.Student != null ? ColorEvent.individual : ColorEvent.group))
           .ForMember("textColor", opt => opt.MapFrom(c => c.isCanceled == true ? ColorEventText.canceled : c.Student != null ? ColorEventText.individual : ColorEventText.group))
           ).CreateMapper().Map<List<timetableByDatePL>, List<classEvent>>(el);
            return Json(elEvent, JsonRequestBehavior.AllowGet);
        }
    }
}
