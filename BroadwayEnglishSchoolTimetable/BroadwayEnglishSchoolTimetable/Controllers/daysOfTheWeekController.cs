using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "supAdmin")]
    public class daysOfTheWeekController : Controller
    {
        IBLService BLService_;
        public daysOfTheWeekController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<IEnumerable<daysOfTheWeekBL>, List<daysOfTheWeekPL>>(BLService_.daysOfTheWeekBL.GetAll()).OrderBy(x => x.Id));

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            daysOfTheWeekBL el = BLService_.daysOfTheWeekBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<daysOfTheWeekBL, daysOfTheWeekPL>(el));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "day")] daysOfTheWeekPL el)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //if (ModelState.IsValid)
            //{
            //    el.day = el.day.Trim();
            //    if (BLService_.daysOfTheWeekBL.Add(Mapper.Map<daysOfTheWeekPL, daysOfTheWeekBL>(el))!=0)
            //        return RedirectToAction("Index");
            //    ModelState.AddModelError("", "ошибка при создании, возможно объект уже существует");
            //}
            //return View(el);
        }
        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            daysOfTheWeekBL el = BLService_.daysOfTheWeekBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<daysOfTheWeekBL, daysOfTheWeekPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,day")] daysOfTheWeekPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    el.day = el.day.Trim();
                    int res = BLService_.daysOfTheWeekBL.Update(Mapper.Map<daysOfTheWeekPL, daysOfTheWeekBL>(el));
                    if (res == 1)
                        return RedirectToAction("Index");
                    else if (res == 0)
                    {
                        ModelState.AddModelError("", "Подобная запись уже существует");
                        return View(el);
                    }
                }
                catch { }
                ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
            }
            return View(el);
        }
        public ActionResult Delete(int id=-1)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //if (id == -1)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //daysOfTheWeekBL el = BLService_.daysOfTheWeekBL.Get(id);
            //if (el == null)
            //    return HttpNotFound();
            //return View(Mapper.Map<daysOfTheWeekBL, daysOfTheWeekPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //BLService_.daysOfTheWeekBL.Delete(id);
            //return RedirectToAction("Index");
        }
    }
}
