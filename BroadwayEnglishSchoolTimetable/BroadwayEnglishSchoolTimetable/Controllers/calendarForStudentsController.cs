using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BroadwayEnglishSchoolTimetable.Controllers
{
    public class calendarForStudentsController : Controller
    {
        IBLService BLService_;
        public calendarForStudentsController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        int idStudent;
        int IdStudent
        {
            get
            {
                if (idStudent == 0)
                    idStudent = BLService_.studentBL.GetAll().Where(p => p.IdStudent == User.Identity.GetUserId()).Select(p => p.Id).FirstOrDefault();
                return idStudent;
            }
        }

        public ActionResult Index() => View(Mapper.Map<forCalendarBL, forCalendarPL>(BLService_.forCalendarBL.Get(-1)));



        public JsonResult GetResources(string pointStar, string pointEnd)
        {
            var el = Json(Mapper.Map<IEnumerable<studentBL>, List<studentPL>>(BLService_.studentBL.GetAll().Where(p=>p.Id==IdStudent)).Select(p => new {
                Id = p.Id,
                Title = p.surname + " " + p.name+" "+p.patronymic,
                startT = BLService_.openingHourBL.Get(1).open,
                endT = BLService_.openingHourBL.Get(1).close
            }), JsonRequestBehavior.AllowGet);
            return el;
        }


        public JsonResult GetEvents(string pointStar, string pointEnd)
        {
            DateTime startdate = DateTime.ParseExact(pointStar, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.ParseExact(pointEnd, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            startdate = new DateTime(startdate.Year, startdate.Month, startdate.Day);
            enddate = new DateTime(enddate.Year, enddate.Month, enddate.Day);

            List<timetableByDatePL> el = Mapper.Map<IEnumerable<timetableByDateBL>, List<timetableByDatePL>>(BLService_.timetableByDateBL.TakeBetweenDates(startdate, enddate).Where(p => p.idStudent == IdStudent));
            var elEvent = new MapperConfiguration(cfg => cfg.CreateMap<timetableByDatePL, classEvent>()
           .ForMember("id", opt => opt.MapFrom(c => c.Id))
           .ForMember("resourceId", opt => opt.MapFrom(c => c.idStudent))
           .ForMember("title", opt => opt.MapFrom(c => c.ClassRoom + "\n"+c.Teacher +"\n" + c.EducationalMaterials))
           .ForMember("start", opt => opt.MapFrom(c => classEvent.ConvertToUnixTimestamp(c.beginTime.Value)))
            .ForMember("end", opt => opt.MapFrom(c => classEvent.ConvertToUnixTimestamp(c.endTime.Value)))
            .ForMember("color", opt => opt.MapFrom(c => c.isCanceled == true ? ColorEvent.canceled : c.Student != null ? ColorEvent.individual : ColorEvent.group))
            .ForMember("textColor", opt => opt.MapFrom(c => c.isCanceled == true ? ColorEventText.canceled : c.Student != null ? ColorEventText.individual : ColorEventText.group))
           ).CreateMapper().Map<List<timetableByDatePL>, List<classEvent>>(el);
            return Json(elEvent, JsonRequestBehavior.AllowGet);
        }


    }
}
