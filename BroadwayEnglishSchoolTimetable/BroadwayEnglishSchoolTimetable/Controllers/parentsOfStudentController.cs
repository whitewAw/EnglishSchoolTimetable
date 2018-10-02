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
    public class parentsOfStudentController : Controller
    {
        public enum SortState
        {
            SurnameAsc,
            SurnameDesc,
            NameAsc,
            NameDesc,
            PatronymicAsc,
            PatronymicDesc,
            PhoneNumberAsc,
            PhoneNumberDesc,
            MailAsc,
            MailDesc,
            DisableAsc,
            DisableDesc,
        }

        IBLService BLService_;
        public parentsOfStudentController(IBLService serv)
        {
            BLService_ = serv;
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index(string surnameS, string nameS, SortState sortOrder = SortState.SurnameAsc)
        {
            List<parentsOfStudentPL> el = Mapper.Map<IEnumerable<parentsOfStudentBL>, List<parentsOfStudentPL>>(BLService_.parentsOfStudentBL.GetAll());
            if (!string.IsNullOrEmpty(surnameS))
            {
                el = el.Where(p => p.surname.Contains(surnameS)).ToList();
                ViewData["surnameS"] = surnameS;
            }
            if (!string.IsNullOrEmpty(nameS))
            {
                el = el.Where(p => p.name.Contains(nameS)).ToList();
                ViewData["nameS"] = nameS;
            }

            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PatronymicSort"] = sortOrder == SortState.PatronymicAsc ? SortState.PatronymicDesc : SortState.PatronymicAsc;
            ViewData["PhoneNumberSort"] = sortOrder == SortState.PhoneNumberAsc ? SortState.PhoneNumberDesc : SortState.PhoneNumberAsc;
            ViewData["MailSort"] = sortOrder == SortState.MailAsc ? SortState.MailDesc : SortState.MailAsc;
            ViewData["DisableSort"] = sortOrder == SortState.DisableAsc ? SortState.DisableDesc : SortState.DisableAsc;

            switch (sortOrder)
            {
                case SortState.SurnameDesc:
                    el = el.OrderByDescending(s => s.surname).ThenBy(s => s.name).ThenBy(s=>s.patronymic).ToList();
                    break;
                case SortState.NameAsc:
                    el = el.OrderBy(s => s.name).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.NameDesc:
                    el = el.OrderByDescending(s => s.name).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.PhoneNumberAsc:
                    el = el.OrderBy(s => s.phoneNumber).ToList();
                    break;
                case SortState.PhoneNumberDesc:
                    el = el.OrderByDescending(s => s.phoneNumber).ToList();
                    break;
                case SortState.MailAsc:
                    el = el.OrderBy(s => s.mail).ToList();
                    break;
                case SortState.MailDesc:
                    el = el.OrderByDescending(s => s.mail).ToList();
                    break;
                case SortState.PatronymicAsc:
                    el = el.OrderBy(s => s.patronymic).ToList();
                    break;
                case SortState.PatronymicDesc:
                    el = el.OrderByDescending(s => s.patronymic).ToList();
                    break;
                case SortState.DisableAsc:
                    el = el.OrderBy(s => s.disable).ToList();
                    break;
                case SortState.DisableDesc:
                    el = el.OrderByDescending(s => s.disable).ToList();
                    break;
                default:
                    el = el.OrderBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
            }
            return View(el);
        }
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            parentsOfStudentBL el = BLService_.parentsOfStudentBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<parentsOfStudentBL, parentsOfStudentPL>(el));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "surname,name,patronymic,mail,phoneNumber")] parentsOfStudentPL el)
        {
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrEmpty(el.surname))
                    el.surname = el.surname.Trim();
                el.name = el.name.Trim();
                if (!string.IsNullOrEmpty(el.patronymic))
                    el.patronymic = el.patronymic.Trim();
                if (!string.IsNullOrEmpty(el.mail))
                    el.mail = el.mail.Trim();
                el.phoneNumber = el.phoneNumber.Trim();
                el.disable = false;
                parentsOfStudentBL parent = Mapper.Map<parentsOfStudentPL, parentsOfStudentBL>(el);
                if (BLService_.parentsOfStudentBL.Add(parent)!=0)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "ошибка при создании, попробуйте позже");
            }
            return View(el);
        }

        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            parentsOfStudentBL el = BLService_.parentsOfStudentBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<parentsOfStudentBL, parentsOfStudentPL>(el));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,surname,name,patronymic,mail,phoneNumber,disable")] parentsOfStudentPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(el.surname))
                        el.surname = el.surname.Trim();
                    el.name = el.name.Trim();
                    if (!string.IsNullOrEmpty(el.patronymic))
                        el.patronymic = el.patronymic.Trim();
                    if (!string.IsNullOrEmpty(el.mail))
                        el.mail = el.mail.Trim();
                    el.phoneNumber = el.phoneNumber.Trim();
                    parentsOfStudentBL parent = Mapper.Map<parentsOfStudentPL, parentsOfStudentBL>(el);
                    int res = BLService_.parentsOfStudentBL.Update(parent);
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

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            parentsOfStudentBL el = BLService_.parentsOfStudentBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<parentsOfStudentBL, parentsOfStudentPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "supAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.parentsOfStudentBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
