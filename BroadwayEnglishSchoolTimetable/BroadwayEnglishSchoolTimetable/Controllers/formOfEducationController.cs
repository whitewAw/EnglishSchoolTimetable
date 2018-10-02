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
    public class formOfEducationController : Controller
    {
        IBLService BLService_;
        public formOfEducationController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<IEnumerable<formOfEducationBL>, List<formOfEducationPL>>(BLService_.formOfEducationBL.GetAll()).OrderBy(p => p.form));
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            formOfEducationBL el = BLService_.formOfEducationBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<formOfEducationBL, formOfEducationPL>(el));
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "form")] formOfEducationPL el)
        {
            if (ModelState.IsValid)
            {
                el.form = el.form.Trim();
                el.disable = false;
                if (BLService_.formOfEducationBL.Add(Mapper.Map<formOfEducationPL, formOfEducationBL>(el))!=0)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "ошибка при создании, возможно объект уже существует");
            }
            return View(el);
        }
        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            formOfEducationBL el = BLService_.formOfEducationBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<formOfEducationBL, formOfEducationPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,form,disable")] formOfEducationPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    el.form = el.form.Trim();
                    int res = BLService_.formOfEducationBL.Update(Mapper.Map<formOfEducationPL, formOfEducationBL>(el));
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
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            formOfEducationBL el = BLService_.formOfEducationBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<formOfEducationBL, formOfEducationPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.formOfEducationBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
