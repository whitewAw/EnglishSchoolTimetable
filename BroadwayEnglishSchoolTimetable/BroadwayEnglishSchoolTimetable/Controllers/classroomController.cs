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
    [Authorize(Roles = "admin")]
    public class classroomController : Controller
    {
        IBLService BLService_;
        public classroomController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<IEnumerable<classroomBL>, List<classroomPL>>(BLService_.classroomBL.GetAll()).OrderBy(p => p.name));

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name ")] classroomPL el)
        {
            if (ModelState.IsValid)
            {
                el.name = el.name.Trim();
                el.disable = false;
                if (BLService_.classroomBL.Add(Mapper.Map<classroomPL, classroomBL>(el)) != 0)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "ошибка при создании, возможно объект уже существует");
            }
            return View(el);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            classroomBL el = BLService_.classroomBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<classroomBL, classroomPL>(el));
        }

        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            classroomBL el = BLService_.classroomBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<classroomBL, classroomPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,disable")] classroomPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    el.name = el.name.Trim();
                    int res = BLService_.classroomBL.Update(Mapper.Map<classroomPL, classroomBL>(el));
                    if (res == 1)
                        return RedirectToAction("Index");
                    else if (res == 0)
                    {
                        ModelState.AddModelError("", "Подобная запись уже существует");
                        return View(el);
                    }
                }
                catch {  }
                ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
            }
            return View(el);
        }

        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            classroomBL el = BLService_.classroomBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<classroomBL, classroomPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.classroomBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
