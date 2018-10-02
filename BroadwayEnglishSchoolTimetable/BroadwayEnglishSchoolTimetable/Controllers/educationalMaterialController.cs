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
    public class educationalMaterialController : Controller
    {
        IBLService BLService_;
        public educationalMaterialController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<IEnumerable<educationalMaterialBL>, List<educationalMaterialPL>>(BLService_.educationalMaterialBL.GetAll()).OrderBy(p => p.name));
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            educationalMaterialBL el = BLService_.educationalMaterialBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<educationalMaterialBL, educationalMaterialPL>(el));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name")] educationalMaterialPL el)
        {
            if (ModelState.IsValid)
            {
                el.name = el.name.Trim();
                el.disable = false;
                if (BLService_.educationalMaterialBL.Add(Mapper.Map<educationalMaterialPL, educationalMaterialBL>(el))!=0)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "ошибка при создании, попробуйте позже");
            }
            return View(el);
        }

        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            educationalMaterialBL el = BLService_.educationalMaterialBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<educationalMaterialBL, educationalMaterialPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,disable")] educationalMaterialPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    el.name = el.name.Trim();
                    int res = BLService_.educationalMaterialBL.Update(Mapper.Map<educationalMaterialPL, educationalMaterialBL>(el));
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
            educationalMaterialBL el = BLService_.educationalMaterialBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<educationalMaterialBL, educationalMaterialPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.educationalMaterialBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
