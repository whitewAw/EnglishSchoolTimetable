using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using System.Web.Mvc;
using System.Web.UI;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "supAdmin")]
    public class openingHourController : Controller
    {
        IBLService BLService_;
        public openingHourController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<openingHourBL, openingHourPL>(BLService_.openingHourBL.Get(1)));
        public ActionResult Edit(int id) => View(Mapper.Map<openingHourBL, openingHourPL>(BLService_.openingHourBL.Get(1)));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "open,close")] openingHourPL el)
        {
            if (ModelState.IsValid)
            {
                if (el.close > el.open)
                {
                    BLService_.openingHourBL.Update(Mapper.Map<openingHourPL, openingHourBL>(el));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "ошибка проверьте указанное время");
            }
            return View(el);
        }
    }
}
