using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using System.Web.Mvc;
using System.Web.UI;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "supAdmin")]
    public class mainMailController : Controller
    {
        IBLService BLService_;
        public mainMailController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index() => View(Mapper.Map<mainMailBL, mainMailPL>(BLService_.mainMailBL.Get(1)));

        public ActionResult Edit()
        {
            return View(Mapper.Map<mainMailBL, mainMailPL>(BLService_.mainMailBL.Get(1)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mail,password")] mainMailPL el)
        {
            if (ModelState.IsValid)
            {
                if (el.mail.Contains("@outlook.") || el.mail.Contains("@hotmail."))
                {
                    BLService_.mainMailBL.Update(Mapper.Map<mainMailPL, mainMailBL>(el));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Только @outlook.com или @hotmail.com");
            }
            return View(el);
        }
    }
}
