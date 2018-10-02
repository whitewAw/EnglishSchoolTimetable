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
using System.Web.UI;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "admin")]
    public class waitingListController : Controller
    {
        IBLService BLService_;
        public waitingListController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public enum SortState
        {
            StudentAsc,
            StudentDesc,
            AdminAsc,
            AdminDesc,
            DateAsc,
            DateDesc,
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index(string StudentSearchS, SortState sortOrder = SortState.DateDesc)
        {
            var el = Mapper.Map<IEnumerable<waitingListBL>, List<waitingListPL>>(BLService_.waitingListBL.GetAll());
            if (!string.IsNullOrEmpty(StudentSearchS))
            {
                el = el.Where(p=>p.FullNameStud.ToLower().Contains(StudentSearchS.ToLower())).ToList();
                ViewData["StudentSearchS"] = StudentSearchS;
            }

            ViewData["StudentSort"] = sortOrder == SortState.StudentAsc ? SortState.StudentDesc : SortState.StudentAsc;
            ViewData["AdminSort"] = sortOrder == SortState.AdminAsc ? SortState.AdminDesc : SortState.AdminAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            switch (sortOrder)
            {
                case SortState.StudentAsc:
                    el = el.OrderBy(s => s.FullNameStud).ThenBy(s => s.dateTarget_).ToList();
                    break;
                case SortState.StudentDesc:
                    el = el.OrderByDescending(s => s.FullNameStud).ThenBy(s => s.dateTarget_).ToList();
                    break;
                case SortState.AdminAsc:
                    el = el.OrderBy(s => s.FullNameAdmin).ThenBy(s => s.dateTarget_).ToList();
                    break;
                case SortState.AdminDesc:
                    el = el.OrderByDescending(s => s.FullNameAdmin).ThenBy(s => s.dateTarget_).ToList();
                    break;
                case SortState.DateAsc:
                    el = el.OrderBy(s => s.dateTarget_).ThenBy(s => s.dateTarget_).ToList();
                    break;
                default:
                    el = el.OrderByDescending(s => s.dateTarget_).ThenBy(s => s.dateTarget_).ToList();
                    break;
            }
            return View(el);
        }

        public ActionResult Create()
        {
            getListVAsync();
            var el = new waitingListPL();
            if (User.IsInRole("supAdmin"))
                    el.idAdmin = BLService_.adminListBL.GetAll().Where(p => BLService_.adminListBL.GetFullInfo(p.Id).IdAdmin == User.Identity.GetUserId()).Select(p => p.Id).FirstOrDefault();
             return View(el);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStudents,dateTarget_,notes,idAdmin")] waitingListPL el)
        {
            if (ModelState.IsValid)
            {
                if ((el.idAdmin > 0 && User.IsInRole("supAdmin")) || !User.IsInRole("supAdmin"))
                {
                    if (!string.IsNullOrEmpty(el.notes))
                        el.notes = el.notes.Trim();
                    if (!User.IsInRole("supAdmin"))
                        el.idAdmin = BLService_.adminListBL.GetAll().Where(p => BLService_.adminListBL.GetFullInfo(p.Id).IdAdmin == User.Identity.GetUserId()).FirstOrDefault().Id;
                    if (BLService_.waitingListBL.Add(Mapper.Map<waitingListPL, waitingListBL>(el)) != 0)
                        return RedirectToAction("Index");
                    ModelState.AddModelError("", "ошибка при создании, возможно объект уже существует");
                }
                ModelState.AddModelError("", "не выбрано администратора");
            }
            getListVAsync();
            return View(el);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            waitingListBL el = BLService_.waitingListBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var idAdmin = BLService_.adminListBL.GetFullInfo(el.idAdmin).IdAdmin;
            //if (!User.IsInRole("supAdmin") && !idAdmin.Equals(User.Identity.GetUserId()))
            //{
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            return View(Mapper.Map<waitingListBL, waitingListPL>(el));
        }

        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            waitingListBL el = BLService_.waitingListBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var idAdmin = BLService_.adminListBL.GetFullInfo(el.idAdmin).IdAdmin;
            //if (!User.IsInRole("supAdmin") && !idAdmin.Equals(User.Identity.GetUserId()))
            //{
            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            getListVAsync();
            return View(Mapper.Map<waitingListBL, waitingListPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,idStudents,dateTarget_,notes,idAdmin")] waitingListPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((el.idAdmin > 0 && User.IsInRole("supAdmin")) || !User.IsInRole("supAdmin"))
                    {
                        if (!string.IsNullOrEmpty(el.notes))
                            el.notes = el.notes.Trim();
                        if (!User.IsInRole("supAdmin"))
                            el.idAdmin = BLService_.adminListBL.GetAll().Where(p => BLService_.adminListBL.GetFullInfo(p.Id).IdAdmin == User.Identity.GetUserId()).FirstOrDefault().Id;
                        BLService_.waitingListBL.Update(Mapper.Map<waitingListPL, waitingListBL>(el));
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "не выбрано администратора");
                }
                catch { ModelState.AddModelError("", "ошибка редактирования, попробуйте позже"); }
            }
            getListVAsync();
            return View(el);
        }

        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            waitingListBL el = BLService_.waitingListBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var idAdmin = BLService_.adminListBL.GetFullInfo(el.idAdmin).IdAdmin;
            //if (!User.IsInRole("supAdmin") && !idAdmin.Equals(User.Identity.GetUserId()))
            //{
            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            return View(Mapper.Map<waitingListBL, waitingListPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.waitingListBL.Delete(id);
            return RedirectToAction("Index");
        }

        private void getListVAsync()
        {
            if (User.IsInRole("supAdmin"))
            {
                var listAdm = BLService_.adminListBL.GetAll().Where(p => p.isInStaff == true).OrderBy(p => p.surname).ToList();
                listAdm.Insert(0, new AdminListBL { name = "", surname = "" });
                ViewBag.AdmName = new SelectList(listAdm.ToDictionary(p => p.Id, p => p.surname + " " + p.name), "Key", "Value");
            }

            var listStud = BLService_.studentBL.GetAll().Where(p=>p.disable==false).OrderBy(p => p.surname).ThenBy(p=>p.name).ToList();
            listStud.Insert(0, new studentBL { name = "", surname = "" });
            ViewBag.StudName = new SelectList(listStud.ToDictionary(p => p.Id, p => p.surname + " " + p.name), "Key", "Value");
        }
    }
}
