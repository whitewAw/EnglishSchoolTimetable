using System;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
                return Redirect("/accessibilityOfRoom");
            else if (User.IsInRole("teacher"))
                return Redirect("/calendarForTeachersOnly");
            else if (User.IsInRole("learner"))
                return Redirect("/calendarForStudents");
            return HttpNotFound();
        }
    }
}