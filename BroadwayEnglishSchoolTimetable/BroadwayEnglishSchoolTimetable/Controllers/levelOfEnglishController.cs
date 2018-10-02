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
    public class levelOfEnglishController : Controller
    {
        IBLService BLService_;
        public levelOfEnglishController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<IEnumerable<levelOfEnglishBL>, List<levelOfEnglishPL>>(BLService_.levelOfEnglishBL.GetAll()).OrderBy(p => p.level));
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            levelOfEnglishBL el = BLService_.levelOfEnglishBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<levelOfEnglishBL, levelOfEnglishPL>(el));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "level")] levelOfEnglishPL el)
        {
            if (ModelState.IsValid)
            {
                el.level = el.level.Trim();
                el.disable = false;
                if (BLService_.levelOfEnglishBL.Add(Mapper.Map<levelOfEnglishPL, levelOfEnglishBL>(el))!=0)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "ошибка при создании, попробуйте позже");
            }
            return View(el);
        }

        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            levelOfEnglishBL el = BLService_.levelOfEnglishBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<levelOfEnglishBL, levelOfEnglishPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,level,disable")] levelOfEnglishPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    el.level = el.level.Trim();
                    int res = BLService_.levelOfEnglishBL.Update(Mapper.Map<levelOfEnglishPL, levelOfEnglishBL>(el));
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

        public ActionResult Delete(int id)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            levelOfEnglishBL el = BLService_.levelOfEnglishBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<levelOfEnglishBL, levelOfEnglishPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.levelOfEnglishBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
