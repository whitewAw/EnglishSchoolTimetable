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
    [Authorize(Roles = "admin")]
    public class timetableByDateController : Controller
    {
        IBLService BLService_;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public timetableByDateController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
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
            teacherAsc,
            teacherDesc,
            adminAsc,
            adminDesc,
            cancelAsc,
            cancelDesc,
            EducationalMaterialsAsc,
            EducationalMaterialsDesc,
        }


       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index(DateTime? StartDateSort, DateTime? EndDateSort, SortState sortOrder = SortState.beginAsc)
        {
            if (StartDateSort == null)
                StartDateSort = DateTime.Today;
            if (EndDateSort == null)
                EndDateSort = DateTime.Today.AddDays(7);

            List<timetableByDatePL> el = Mapper.Map<IEnumerable<timetableByDateBL>, List<timetableByDatePL>>(BLService_.timetableByDateBL.TakeBetweenDates(StartDateSort, EndDateSort.Value.AddDays(1)));

            ViewData["beginSort"] = sortOrder == SortState.beginAsc ? SortState.beginDesc : SortState.beginAsc;
            ViewData["endSort"] = sortOrder == SortState.endAsc ? SortState.endDesc : SortState.endAsc;
            ViewData["timeSort"] = sortOrder == SortState.timeAsc ? SortState.timeDesc : SortState.timeAsc;
            ViewData["groupSort"] = sortOrder == SortState.groupAsc ? SortState.groupDesc : SortState.groupAsc;
            ViewData["studentSort"] = sortOrder == SortState.studentAsc ? SortState.studentDesc : SortState.studentAsc;
            ViewData["roomSort"] = sortOrder == SortState.roomAsc ? SortState.roomDesc : SortState.roomAsc;
            ViewData["teacherSort"] = sortOrder == SortState.teacherAsc ? SortState.teacherDesc : SortState.teacherAsc;
            ViewData["adminSort"] = sortOrder == SortState.adminAsc ? SortState.adminDesc : SortState.adminAsc;
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
                case SortState.teacherAsc:
                    el = el.OrderBy(s => s.Teacher).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.teacherDesc:
                    el = el.OrderByDescending(s => s.Teacher).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.adminAsc:
                    el = el.OrderBy(s => s.Admin).ThenBy(s => s.beginTime).ToList();
                    break;
                case SortState.adminDesc:
                    el = el.OrderByDescending(s => s.Admin).ThenBy(s => s.beginTime).ToList();
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
           if(StartDateSort==null)
                ViewData["StartDateSort"] = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
           else
                ViewData["StartDateSort"] = StartDateSort.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (EndDateSort == null)
                ViewData["EndDateSort"] = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            else
                ViewData["EndDateSort"] = EndDateSort.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            return View(el);
        }

        public ActionResult Create(int levGroup = 0, int levStud = 0)
        {
            var el = Mapper.Map<timetableByDateBL, timetableByDatePL>(BLService_.timetableByDateBL.GetEmpty());
            if (levGroup != 0)
                el.idGroup = levGroup;
            if (levStud != 0)
                el.idStudent = levStud;
            getListVAsync();
            return View(el);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "beginTime,endTime,idGroup,idStudent,timetableDays")] timetableByDatePL El)
        {
            bool check = false;
            bool IshaveDays = false;
            bool endday = false;

            if (El.endTime == null)
            {
                endday = true;
                El.endTime = El.beginTime;
            }


            if (ModelState.IsValid)
            {
                if (El.beginTime > El.endTime)
                {
                    ModelState.AddModelError("beginTime", "начало занятий не может быть позже чем конец");
                    ModelState.AddModelError("endTime", "начало занятий не может быть позже чем конец");
                    check = true;
                }

                if (El.idGroup == 0 && El.idStudent == 0)
                {
                    ModelState.AddModelError("idGroup", "не выбрана группа или студент");
                    ModelState.AddModelError("idStudent", "не выбрана группа или студент");
                    check = true;
                }
                if (El.idGroup != 0 && El.idStudent != 0)
                {
                    ModelState.AddModelError("idGroup", "можно выбрать либо группу либо студента");
                    ModelState.AddModelError("idStudent", "можно выбрать либо группу либо студента");
                    check = true;
                }
                var timeShcool = BLService_.openingHourBL.Get(1);
                foreach (var el in El.timetableDays)
                {
                    if (el.begin >= el.end)
                    {
                        ModelState.AddModelError("", "проверьте время по дням недели");
                        check = true;
                    }

                    if ((el.begin == null && el.end != null) || (el.begin != null && el.end == null))
                    {
                        ModelState.AddModelError("", "заняти должно иметь время начала и окончания");
                        check = true;
                    }
                    if (el.begin != null && el.begin < timeShcool.open)
                    {
                        ModelState.AddModelError("", "время начало занятий не может быть раньше времени начала работы школы");
                        check = true;
                    }
                    if (el.end != null && el.end > timeShcool.close)
                    {
                        ModelState.AddModelError("", "время окончания занятия не может быть позже времени окончания работы школы");
                        check = true;
                    }

                    if (el.begin != null && el.end != null && el.idTeacher == 0)
                    {
                        ModelState.AddModelError("", "не выбран проподователь");
                        check = true;
                    }
                    if (el.begin != null && el.end != null && el.idClassRoom == 0)
                    {
                        ModelState.AddModelError("", "не выбран учебный кабинет");
                        check = true;
                    }
                    if (!string.IsNullOrEmpty(el.notes))
                        el.notes = el.notes.Trim();
                    if (el.begin != null && el.end != null)
                        IshaveDays = true;
                    if (check == true)
                        break;
                }
                if (IshaveDays == false)
                {
                    ModelState.AddModelError("", "не выбран не один день занятий");
                    check = true;
                }
                if (check == true)
                {
                    getListVAsync();
                    return View(El);
                }
                El.Admin = User.Identity.GetUserId();

                int res = BLService_.timetableByDateBL.Add(Mapper.Map<timetableByDatePL, timetableByDateBL>(El));


                if (res == 1)
                    return RedirectToAction("Index");
                else if(res == 2)
                    ModelState.AddModelError("", "Преподаватель не имеет доступ до нужного уровня языка");
                else if (res == 3)
                    ModelState.AddModelError("", "Выбранная дата не соответствует дням недели");
                else if (res == 4)
                    ModelState.AddModelError("", "У студента не выбранын учебные материалы");
                else
                    ModelState.AddModelError("", "ошибка при создании");
            }
            if (endday == false)
                El.endTime = null;


            getListVAsync();
            return View(El);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            timetableByDateBL el = BLService_.timetableByDateBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<timetableByDateBL, timetableByDatePL>(el));
        }
   
        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            timetableByDateBL el = BLService_.timetableByDateBL.Get(id);
            if (el == null)
                return HttpNotFound();
            getListVAsync();

            var listCauseofDist = BLService_.causeOfDisruptionBL.GetAll().OrderBy(p => p.cause).ToList();
            listCauseofDist.Insert(0, new causeOfDisruptionBL { cause = "" });
            ViewBag.CauseofDistName = new SelectList(listCauseofDist.ToDictionary(p => p.Id, p => p.cause), "Key", "Value");

            return View(Mapper.Map<timetableByDateBL, timetableByDatePL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,beginTime,begin,end,idClassRoom,idTeacher,isCanceled,causeOfDisruption,notes,passedMaterials,homework")] timetableByDatePL el)
        {
            ModelState.Remove("endTime");
            ModelState.Remove("causeOfDisruption.cause");

            if (ModelState.IsValid)
            {
                bool check = false;
                if (el.idClassRoom == 0)
                {
                    ModelState.AddModelError("idClassRoom", "не выбран учебный кабинет");
                    check = true;
                }
                if (el.idTeacher == 0)
                {
                    ModelState.AddModelError("idTeacher", "не выбран проподователь");
                    check = true;
                }
                if (el.isCanceled == true && el.causeOfDisruption.Id == 0)
                {
                    ModelState.AddModelError("isCanceled", "не выбрана причина отмены занятия");
                    check = true;
                }
                if (el.begin >= el.end)
                {
                    ModelState.AddModelError("begin", "начало занятий не может быть позже чем конец");
                    ModelState.AddModelError("end", "начало занятий не может быть позже чем конец");
                    check = true;
                }
                var timeShcool = BLService_.openingHourBL.Get(1);
                if (el.begin < timeShcool.open)
                {
                    ModelState.AddModelError("begin", "время начало занятий не может быть раньше времени начала работы школы");
                    check = true;
                }
                if (el.end > timeShcool.close)
                {
                    ModelState.AddModelError("end", "время окончания занятия не может быть позже времени окончания работы школы");
                    check = true;
                }
                if (check == false)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(el.notes))
                            el.notes = el.notes.Trim();
                        if (!string.IsNullOrEmpty(el.passedMaterials))
                            el.passedMaterials = el.passedMaterials.Trim();
                        if (!string.IsNullOrEmpty(el.homework))
                            el.homework = el.homework.Trim();

                        if (User.IsInRole("admin"))
                        {
                            el.Admin = User.Identity.GetUserId();
                            el.Teacher = null;
                        }
                        if (User.IsInRole("teacher") && !User.IsInRole("admin"))
                        {
                            el.Admin = null;
                            el.Teacher = User.Identity.GetUserId();
                        }
                        int res = BLService_.timetableByDateBL.Update(Mapper.Map<timetableByDatePL, timetableByDateBL>(el));


                        if (res == 1)
                            return RedirectToAction("Index");
                        else if (res == 2)
                            ModelState.AddModelError("", "Преподаватель не имеет доступ до нужного уровня языка");
                        else
                            ModelState.AddModelError("", "ошибка при редактировании");
                    }
                    catch { ModelState.AddModelError("", "ошибка редактирования, попробуйте позже"); }
                }
            }
            var listCauseofDist = BLService_.causeOfDisruptionBL.GetAll().OrderBy(p => p.cause).ToList();
            listCauseofDist.Insert(0, new causeOfDisruptionBL { cause = "" });
            ViewBag.CauseofDistName = new SelectList(listCauseofDist.ToDictionary(p => p.Id, p => p.cause), "Key", "Value");
            getListVAsync();
            return View(el);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            timetableByDateBL el = BLService_.timetableByDateBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<timetableByDateBL, timetableByDatePL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "supAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.timetableByDateBL.Delete(id);
            return RedirectToAction("Index");
        }




        private void getListVAsync()
        {

            var listTeach = BLService_.teacherBL.GetAll().Where(p => p.endDate == null || p.endDate>DateTime.Now).OrderBy(p => p.surname).ToList();
            listTeach.Insert(0, new teacherBL { name = "", surname = "" });
            ViewBag.TeacName = new SelectList(listTeach.ToDictionary(p => p.Id, p => p.surname + " " + p.name), "Key", "Value");

            var listStud = BLService_.studentBL.GetAll().Where(p=>(p.idFormOfEducation== (int)FormEdication.Private || p.idFormOfEducation == (int)FormEdication.GroupPrivate) && p.disable==false && p.idLevelOfEnglish !=null).OrderBy(p => p.surname).ToList();
            listStud.Insert(0, new studentBL { name = "", surname = "" });
            ViewBag.StudName = new SelectList(listStud.ToDictionary(p => p.Id, p => p.surname + " " + p.name), "Key", "Value");

            var listGroup = BLService_.groupBL.GetAll().Where(p=>p.disable==false).OrderByDescending(p => p.Id).ToList();
            listGroup.Insert(0, new groupBL { name = "" });
            ViewBag.GroupName = new SelectList(listGroup.ToDictionary(p => p.Id, p => p.levelOfEnglish+" "+p.name), "Key", "Value");

            var listCRoom = BLService_.classroomBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.name).ToList();
            listCRoom.Insert(0, new classroomBL { name = "" });
            ViewBag.CRoomName = new SelectList(listCRoom.ToDictionary(p => p.Id, p => p.name), "Key", "Value");
        }
    }
}
