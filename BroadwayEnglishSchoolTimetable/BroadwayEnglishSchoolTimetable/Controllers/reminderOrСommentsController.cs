using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "admin")]
    public class reminderOrСommentsController : Controller
    {
        IBLService BLService_;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public reminderOrСommentsController(IBLService serv) => BLService_ = serv;
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

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index(SortState sortOrder = SortState.DateDesc)
        {
            var el = Mapper.Map<IEnumerable<reminderOrСommentsBL>, List<reminderOrСommentsPL>>(BLService_.reminderOrСommentsBL.GetAll()).OrderByDescending(p => p.date).ToList();
            if (!User.IsInRole("supAdmin"))
            {
                el = el.Where(p => p.idAdminStr == User.Identity.GetUserId()).ToList();
            }
            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            switch (sortOrder)
            {
                case SortState.SurnameAsc:
                    el = el.OrderBy(s => s.idAdminStr).ThenBy(s=>s.date).ToList();
                    break;
                case SortState.SurnameDesc:
                    el = el.OrderByDescending(s => s.idAdminStr).ThenBy(s => s.date).ToList();
                    break;
                case SortState.DateAsc:
                    el = el.OrderBy(s => s.date).ThenBy(s => s.idAdminStr).ToList();
                    break;
                default:
                    el = el.OrderByDescending(s => s.date).ThenBy(s => s.idAdminStr).ToList();
                    break;
            }
            return View(el);
        }

        public ActionResult Create()
        {
            var el = new reminderOrСommentsPL();
            if (User.IsInRole("supAdmin"))
            {
                getListVAsync();
                el.idAdmin = BLService_.adminListBL.GetAll().Where(p => BLService_.adminListBL.GetFullInfo(p.Id).IdAdmin == User.Identity.GetUserId()).Select(p => p.Id).FirstOrDefault();
            }
            return View(el);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "datePL,timePL,notes,idAdmin")] reminderOrСommentsPL el)
        {
            if (ModelState.IsValid)
            {
                if ((el.idAdmin > 0 && User.IsInRole("supAdmin")) || !User.IsInRole("supAdmin"))
                {
                    if (!string.IsNullOrEmpty(el.notes))
                        el.notes = el.notes.Trim();

                    if (User.IsInRole("supAdmin"))
                        el.idAdminStr = BLService_.adminListBL.GetFullInfo(el.idAdmin).IdAdmin;
                    else
                        el.idAdminStr = User.Identity.GetUserId();
                    if (BLService_.reminderOrСommentsBL.Add(Mapper.Map<reminderOrСommentsPL, reminderOrСommentsBL>(el)) != 0)
                        return RedirectToAction("Index");
                    ModelState.AddModelError("", "ошибка при создании, возможно объект уже существует");
                }
                ModelState.AddModelError("", "не выбрано администратора");
            }
            if (User.IsInRole("supAdmin"))
                getListVAsync();
            return View(el);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            reminderOrСommentsBL el = BLService_.reminderOrСommentsBL.Get(id);
            if (el == null)
                return HttpNotFound();
            if (!User.IsInRole("supAdmin") && !el.idAdminStr.Equals(User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Mapper.Map<reminderOrСommentsBL, reminderOrСommentsPL>(el));
        }

        public ActionResult Edit(int id=1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            reminderOrСommentsBL el = BLService_.reminderOrСommentsBL.Get(id);
            if (el == null)
                return HttpNotFound();
            if (!User.IsInRole("supAdmin") && !el.idAdminStr.Equals(User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.IsInRole("supAdmin"))
                getListVAsync();
            return View(Mapper.Map<reminderOrСommentsBL, reminderOrСommentsPL>(el));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,datePL,timePL,notes,idAdmin")] reminderOrСommentsPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((el.idAdmin > 0 && User.IsInRole("supAdmin")) || !User.IsInRole("supAdmin"))
                    {
                        if (!string.IsNullOrEmpty(el.notes))
                            el.notes = el.notes.Trim();
                        if (User.IsInRole("supAdmin"))
                            el.idAdminStr = "admin";
                        int res = BLService_.reminderOrСommentsBL.Update(Mapper.Map<reminderOrСommentsPL, reminderOrСommentsBL>(el));
                        if (res == 1)
                            return RedirectToAction("Index");
                        if (res == 2)
                        {
                            ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
                            if (User.IsInRole("supAdmin"))
                                getListVAsync();
                            return View(el);
                        }
                    }
                    ModelState.AddModelError("", "не выбрано администратора");
                }
                catch { ModelState.AddModelError("", "ошибка редактирования, попробуйте позже"); }
            }
            if (User.IsInRole("supAdmin"))
                getListVAsync();
            return View(el);
        }

        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            reminderOrСommentsBL el = BLService_.reminderOrСommentsBL.Get(id);
            if (el == null)
                return HttpNotFound();
            if (!User.IsInRole("supAdmin") && !el.idAdminStr.Equals(User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Mapper.Map<reminderOrСommentsBL, reminderOrСommentsPL>(el));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.reminderOrСommentsBL.Delete(id);
            return RedirectToAction("Index");
        }

        private void getListVAsync()
        {
            var list = BLService_.adminListBL.GetAll().Where(p => p.isInStaff == true).OrderBy(p => p.surname).ToList();
            list.Insert(0, new AdminListBL { name = "", surname = "" });
            ViewBag.AdmName = new SelectList(list.ToDictionary(p => p.Id, p => p.surname + " " + p.name), "Key", "Value");
        }
    }
}
