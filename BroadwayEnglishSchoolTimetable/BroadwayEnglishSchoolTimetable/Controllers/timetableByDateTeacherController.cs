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
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "teacher")]
    public class timetableByDateTeacherController : Controller
    {

        IBLService BLService_;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public timetableByDateTeacherController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

        int idTeacher;
        int IdTeacher
        {
            get
            {
                if (idTeacher == 0)
                    idTeacher = BLService_.teacherBL.GetAll().Where(p => p.IdTeacher == User.Identity.GetUserId()).Select(p => p.Id).FirstOrDefault();
                return idTeacher;
            }
        }

        public enum SortState
        {
            beginAsc,
            beginDesc,
            endAsc,
            endDesc,
            timeAsc,
            timeDesc,
            groupAsc,
            groupDesc,
            studentAsc,
            studentDesc,
            roomAsc,
            roomDesc,
            cancelAsc,
            cancelDesc,
            EducationalMaterialsAsc,
            EducationalMaterialsDesc,
        }


        public ActionResult Index(DateTime? StartDateSort, DateTime? EndDateSort, SortState sortOrder = SortState.beginAsc)
        {
            if (StartDateSort == null)
                StartDateSort = DateTime.Today;
            if (EndDateSort == null)
                EndDateSort = DateTime.Today.AddDays(7);
           
            List<timetableByDatePL> el = Mapper.Map<IEnumerable<timetableByDateBL>, List<timetableByDatePL>>(BLService_.timetableByDateBL.TakeBetweenDates(StartDateSort, EndDateSort.Value.AddDays(1)).Where(p => p.idTeacher == IdTeacher));
            ViewData["beginSort"] = sortOrder == SortState.beginAsc ? SortState.beginDesc : SortState.beginAsc;
            ViewData["endSort"] = sortOrder == SortState.endAsc ? SortState.endDesc : SortState.endAsc;
            ViewData["timeSort"] = sortOrder == SortState.timeAsc ? SortState.timeDesc : SortState.timeAsc;
            ViewData["groupSort"] = sortOrder == SortState.groupAsc ? SortState.groupDesc : SortState.groupAsc;
            ViewData["studentSort"] = sortOrder == SortState.studentAsc ? SortState.studentDesc : SortState.studentAsc;
            ViewData["roomSort"] = sortOrder == SortState.roomAsc ? SortState.roomDesc : SortState.roomAsc;
            ViewData["cancelSort"] = sortOrder == SortState.cancelAsc ? SortState.cancelDesc : SortState.cancelAsc;
            ViewData["EducationalMaterialsSort"] = sortOrder == SortState.EducationalMaterialsAsc ? SortState.EducationalMaterialsDesc : SortState.EducationalMaterialsAsc;

            switch (sortOrder)
            {
                case SortState.beginDesc:
                    el = el.OrderByDescending(s => s.beginTime).ThenBy(s => s.Student).ToList();
                    break;
                case SortState.endAsc:
                    el = el.OrderBy(s => s.endTime).ThenBy(s => s.Student).ToList();
                    break;
                case SortState.endDesc:
                    el = el.OrderByDescending(s => s.endTime).ThenBy(s => s.Student).ToList();
                    break;
                case SortState.timeAsc:
                    el = el.OrderBy(s => s.TimeofMinuts).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.timeDesc:
                    el = el.OrderByDescending(s => s.TimeofMinuts).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.groupAsc:
                    el = el.OrderBy(s => s.Group).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.groupDesc:
                    el = el.OrderByDescending(s => s.Group).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.studentAsc:
                    el = el.OrderBy(s => s.Student).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.studentDesc:
                    el = el.OrderByDescending(s => s.Student).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.roomAsc:
                    el = el.OrderBy(s => s.ClassRoom).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.roomDesc:
                    el = el.OrderByDescending(s => s.ClassRoom).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.cancelAsc:
                    el = el.OrderBy(s => s.isCanceled).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.cancelDesc:
                    el = el.OrderByDescending(s => s.isCanceled).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.EducationalMaterialsAsc:
                    el = el.OrderBy(s => s.EducationalMaterials).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.EducationalMaterialsDesc:
                    el = el.OrderByDescending(s => s.EducationalMaterials).ThenBy(s => s.beginTime).ToList();
                    break;
                default:
                    el = el.OrderBy(s => s.beginTime).ThenBy(s => s.Student).ToList();
                    break;
            }
            if (StartDateSort == null)
                ViewData["StartDateSort"] = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            else
                ViewData["StartDateSort"] = StartDateSort.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (EndDateSort == null)
                ViewData["EndDateSort"] = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            else
                ViewData["EndDateSort"] = EndDateSort.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            return View(el);
        }

        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            timetableByDateBL el = BLService_.timetableByDateBL.Get(id);
            if (el == null || el.idTeacher!= IdTeacher)
                return HttpNotFound();
            return View(Mapper.Map<timetableByDateBL, timetableByDatePL>(el));
        }
        public ActionResult Edit(int id)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            timetableByDateBL el = BLService_.timetableByDateBL.Get(id);
            if (el == null || el.idTeacher != IdTeacher)
                return HttpNotFound();
            return View(Mapper.Map<timetableByDateBL, timetableByDatePL>(el));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,beginTime,begin,end,idClassRoom,idTeacher,isCanceled,causeOfDisruption,notes,passedMaterials,homework")] timetableByDatePL el)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(el.passedMaterials))
                    el.passedMaterials = el.passedMaterials.Trim();
                if (!string.IsNullOrEmpty(el.homework))
                    el.homework = el.homework.Trim();
                timetableByDateBL El = BLService_.timetableByDateBL.Get(el.Id);
                if (El == null || El.idTeacher != IdTeacher)
                {
                    ModelState.AddModelError("", "ошибка при редактировании");
                    return View(el);
                }
                El.passedMaterials = el.passedMaterials;
                El.homework = el.homework;
                El.Teacher = User.Identity.GetUserId();
                El.Admin = null;

                int res = BLService_.timetableByDateBL.Update(El);
                if (res == 1)
                    return RedirectToAction("Index");
                else if (res == 2)
                    ModelState.AddModelError("", "Преподаватель не имеет доступ до нужного уровня языка");
                else
                    ModelState.AddModelError("", "ошибка при редактировании");
            }
            return View(el);
        }




    }
}
