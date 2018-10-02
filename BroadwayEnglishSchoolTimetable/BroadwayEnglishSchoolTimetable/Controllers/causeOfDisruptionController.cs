using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "admin")]
    public class causeOfDisruptionController : Controller
    {
        IBLService BLService_;
        public causeOfDisruptionController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<IEnumerable<causeOfDisruptionBL>, List<causeOfDisruptionPL>>(BLService_.causeOfDisruptionBL.GetAll()).OrderBy(p => p.cause));

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cause")] causeOfDisruptionPL el)
        {
            if (ModelState.IsValid)
            {
                el.cause = el.cause.Trim();
                el.disable = false;
                if (BLService_.causeOfDisruptionBL.Add(Mapper.Map<causeOfDisruptionPL, causeOfDisruptionBL>(el)) != 0)
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
            causeOfDisruptionBL el = BLService_.causeOfDisruptionBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<causeOfDisruptionBL, causeOfDisruptionPL>(el));
        }

        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            causeOfDisruptionBL el = BLService_.causeOfDisruptionBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<causeOfDisruptionBL, causeOfDisruptionPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cause,disable")] causeOfDisruptionPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    el.cause = el.cause.Trim();
                    int res = BLService_.causeOfDisruptionBL.Update(Mapper.Map<causeOfDisruptionPL, causeOfDisruptionBL>(el));
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
            causeOfDisruptionBL el = BLService_.causeOfDisruptionBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<causeOfDisruptionBL, causeOfDisruptionPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.causeOfDisruptionBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
