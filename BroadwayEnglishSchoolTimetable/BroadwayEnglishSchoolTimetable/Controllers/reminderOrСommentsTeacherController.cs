using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "supAdmin, teacher")]
    public class reminderOrСommentsTeacherController : Controller
    {
        IBLService BLService_;
        public reminderOrСommentsTeacherController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

        public enum SortState
        {
            SurnameAsc,
            SurnameDesc,
            DateAsc,
            DateDesc,
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Index(SortState sortOrder = SortState.DateDesc)
        {
            var el = Mapper.Map<IEnumerable<reminderOrСommentsTeacherBL>, List<reminderOrСommentsTeacherPL>>(BLService_.reminderOrСommentsTeacherBL.GetAll()).OrderByDescending(p => p.date).ToList();
            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            switch (sortOrder)
            {
                case SortState.SurnameAsc:
                    el = el.OrderBy(s => s.TeachersName).ThenBy(s => s.date).ToList();
                    break;
                case SortState.SurnameDesc:
                    el = el.OrderByDescending(s => s.TeachersName).ThenBy(s => s.date).ToList();
                    break;
                case SortState.DateAsc:
                    el = el.OrderBy(s => s.date).ThenBy(s => s.TeachersName).ToList();
                    break;
                default:
                    el = el.OrderByDescending(s => s.date).ThenBy(s => s.TeachersName).ToList();
                    break;
            }
            return View(el);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Create()
        {
            getListVAsync();
            return View();
        }

        [Authorize(Roles = "supAdmin")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "datePL,timePL,notes,idTeachers")] reminderOrСommentsTeacherPL el)
        {
            if (ModelState.IsValid)
            {
                   if (!string.IsNullOrEmpty(el.notes))
                        el.notes = el.notes.Trim();
                   if (BLService_.reminderOrСommentsTeacherBL.Add(Mapper.Map<reminderOrСommentsTeacherPL, reminderOrСommentsTeacherBL>(el)) != 0)
                        return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "ошибка при создании.");
            getListVAsync();
            return View(el);
        }

        [Authorize(Roles = "supAdmin, teacher")]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            reminderOrСommentsTeacherBL el = BLService_.reminderOrСommentsTeacherBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<reminderOrСommentsTeacherBL, reminderOrСommentsTeacherPL>(el));
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Edit(int id)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            reminderOrСommentsTeacherBL el = BLService_.reminderOrСommentsTeacherBL.Get(id);
            if (el == null)
                return HttpNotFound();
            getListVAsync();
            return View(Mapper.Map<reminderOrСommentsTeacherBL, reminderOrСommentsTeacherPL>(el));
        }

        [Authorize(Roles = "supAdmin")]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,datePL,timePL,notes,idTeachers")] reminderOrСommentsTeacherPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(el.notes))
                        el.notes = el.notes.Trim();
                    int res = BLService_.reminderOrСommentsTeacherBL.Update(Mapper.Map<reminderOrСommentsTeacherPL, reminderOrСommentsTeacherBL>(el));
                    if (res == 1)
                        return RedirectToAction("Index");
                }
                catch { }
            }
            ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
            getListVAsync();
            return View(el);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            reminderOrСommentsTeacherBL el = BLService_.reminderOrСommentsTeacherBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<reminderOrСommentsTeacherBL, reminderOrСommentsTeacherPL>(el));
        }

        [Authorize(Roles = "supAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.reminderOrСommentsTeacherBL.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "supAdmin")]
        private void getListVAsync()
        {
            var list = BLService_.teacherBL.GetAll().Where(p => p.endDate == null || p.endDate>DateTime.Now).OrderBy(p => p.surname).ToList();
            list.Insert(0, new teacherBL { name = "", surname = "", patronymic=""});
            ViewBag.TeachName = new SelectList(list.ToDictionary(p => p.Id, p => p.surname + " " + p.name + " " + p.patronymic), "Key", "Value");
        }

    }
}
