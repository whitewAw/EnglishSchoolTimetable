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
    [Authorize(Roles = "admin,teacher")]
    public class groupController : Controller
    {
        public enum SortState
        {
            IdAsc,
            IdDesc,
            NameGrAsc,
            NameGRDesc,
            EducationalMaterialsAsc,
            EducationalMaterialsDesc,
            DisableAsc,
            DisableDesc,

        }

        public enum SortStateCreatEdit
        {
            SurnameAsc,
            SurnameDesc,
            NameAsc,
            NameDesc,
            PatronymicAsc,
            PatronymicDesc,
            MailAsc,
            MailDesc,
            PhoneNumberAsc,
            PhoneNumberDesc,
            DateOfBirthAsc,
            DateOfBirthDesc,
            LevelOfEnglishAsc,
            LevelOfEnglishDesc,
            DateCreateAsc,
            DateCreateDesc,
            isFirstLessonAsc,
            isFirstLessonDesc,
            idAsc,
            idDesc,
        }


        IBLService BLService_;
        public groupController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        [Authorize(Roles = "admin")]
        public ActionResult Index(string GroupSearchS, string StudentSearchS, SortState sortOrder = SortState.IdDesc)
        {
            var el = Mapper.Map<IEnumerable<groupBL>, List<groupPL>>(BLService_.groupBL.GetAll()).ToList();

            if (!string.IsNullOrEmpty(GroupSearchS))
            {
                el = el.Where(p => p.name.Contains(GroupSearchS)).ToList();
                ViewData["GroupSearchS"] = GroupSearchS;
            }
            if (!string.IsNullOrEmpty(StudentSearchS))
            {
                el = el.Where(p => p.student.Any(c => c.FullName.ToLower().Contains(StudentSearchS.ToLower()))).ToList();
                ViewData["StudentSearchS"] = StudentSearchS;
            }

            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameGrAsc ? SortState.NameGRDesc : SortState.NameGrAsc;
            ViewData["EducationalMaterialsSort"] = sortOrder == SortState.EducationalMaterialsAsc ? SortState.EducationalMaterialsDesc : SortState.EducationalMaterialsAsc;
            ViewData["DisableSort"] = sortOrder == SortState.DisableAsc ? SortState.DisableDesc : SortState.DisableAsc;
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    el = el.OrderBy(s => s.Id).ToList();
                    break;
                case SortState.NameGrAsc:
                    el = el.OrderBy(s => s.name).ToList();
                    break;
                case SortState.NameGRDesc:
                    el = el.OrderByDescending(s => s.name).ToList();
                    break;

                case SortState.EducationalMaterialsAsc:
                    el = el.OrderBy(s => s.nameEducationalMaterials).ToList();
                    break;
                case SortState.EducationalMaterialsDesc:
                    el = el.OrderByDescending(s => s.nameEducationalMaterials).ToList();
                    break;
                case SortState.DisableAsc:
                    el = el.OrderBy(s => s.disable).ToList();
                    break;
                case SortState.DisableDesc:
                    el = el.OrderByDescending(s => s.disable).ToList();
                    break;
                default:
                    el = el.OrderByDescending(s => s.Id).ToList();
                    break;
            }
            return View(el);
        }
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id = -1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            groupBL el = BLService_.groupBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<groupBL, groupPL>(el));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create(SortStateCreatEdit sortOrder = SortStateCreatEdit.SurnameAsc, int lev = 0)
        {
            var el = Mapper.Map<groupBL, groupPL>(BLService_.groupBL.GetEmpty());
            if (lev != 0)
            {
                el.studentBig = el.studentBig.Where(p => p.idLevelOfEnglish != null && p.idLevelOfEnglish == lev).ToList();
                el.idLevelOfEnglish = lev;

            }
            ViewData["lev"] = lev;
            GetListVAsync();
           return View(sort(ref el, sortOrder));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "name,idLevelOfEnglish,studentSelect,idEducationalMaterials,notes, ")] groupPL el)
        {
            if (ModelState.IsValid)
            {
                if (el.studentSelect.Count != 0)
                {
                    el.name = el.name.Trim();
                    el.disable = false;
                    if (BLService_.groupBL.Add(Mapper.Map<groupPL, groupBL>(el)) != 0)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "ошибка при создании, возможно объект уже существует");
                }
                else
                    ModelState.AddModelError("", "группа не может быть без студентов");
            }
            GetListVAsync();
            return View(el);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = -1, SortStateCreatEdit sortOrder = SortStateCreatEdit.SurnameAsc)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            groupBL el = BLService_.groupBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var elform = Mapper.Map<groupBL, groupPL>(el);
            elform.studentBig = elform.studentBig.Where(p => (p.idLevelOfEnglish != null && p.idLevelOfEnglish == el.idLevelOfEnglish) || elform.studentSelect.Contains(p.Id)).ToList();
            GetListVAsync();
            return View(sort(ref elform, sortOrder));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,name,studentSelect,idEducationalMaterials,notes,disable,levelOfEnglish,idLevelOfEnglish")] groupPL el)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (el.studentSelect.Count == 0)
                    {
                        ModelState.AddModelError("", "группа не может быть без студентов");
                        GetListVAsync();
                        el.studentBig = Mapper.Map<groupBL, groupPL>(BLService_.groupBL.Get(el.Id)).studentBig.Where(p => (p.idLevelOfEnglish != null && p.idLevelOfEnglish == el.idLevelOfEnglish) || el.studentSelect.Contains(p.Id)).ToList();

                        return View(el);
                    }

                    int res = BLService_.groupBL.Update(Mapper.Map<groupPL, groupBL>(el));
                    if (res == 1)
                        return RedirectToAction("Index");
                    else if (res == 0)
                    {
                        ModelState.AddModelError("", "Подобная запись уже существует");
                        GetListVAsync();
                        return View(el);
                    }
                }
                catch { }
                ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
            }
            GetListVAsync();
            el.studentBig = Mapper.Map<groupBL, groupPL>(BLService_.groupBL.Get(el.Id)).studentBig.Where(p => (p.idLevelOfEnglish != null && p.idLevelOfEnglish == el.idLevelOfEnglish) || el.studentSelect.Contains(p.Id)).ToList();
            return View(el);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id = -1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            groupBL el = BLService_.groupBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<groupBL, groupPL>(el));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "supAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BLService_.groupBL.Delete(id);
            return RedirectToAction("Index");
        }

        private groupPL sort(ref groupPL el, SortStateCreatEdit sortOrder)
        {
            ViewData["SurnameSort"] = sortOrder == SortStateCreatEdit.SurnameAsc ? SortStateCreatEdit.SurnameDesc : SortStateCreatEdit.SurnameAsc;
            ViewData["NameSort"] = sortOrder == SortStateCreatEdit.NameAsc ? SortStateCreatEdit.NameDesc : SortStateCreatEdit.NameAsc;
            ViewData["PatronymicSort"] = sortOrder == SortStateCreatEdit.PatronymicAsc ? SortStateCreatEdit.PatronymicDesc : SortStateCreatEdit.PatronymicAsc;
            ViewData["MailSort"] = sortOrder == SortStateCreatEdit.MailAsc ? SortStateCreatEdit.MailDesc : SortStateCreatEdit.MailAsc;
            ViewData["PhoneNumberSort"] = sortOrder == SortStateCreatEdit.PhoneNumberAsc ? SortStateCreatEdit.PhoneNumberDesc : SortStateCreatEdit.PhoneNumberAsc;
            ViewData["DateOfBirthSort"] = sortOrder == SortStateCreatEdit.DateOfBirthAsc ? SortStateCreatEdit.DateOfBirthDesc : SortStateCreatEdit.DateOfBirthAsc;
            ViewData["LevelOfEnglishSort"] = sortOrder == SortStateCreatEdit.LevelOfEnglishAsc ? SortStateCreatEdit.LevelOfEnglishDesc : SortStateCreatEdit.LevelOfEnglishAsc;
            ViewData["DateCreateSort"] = sortOrder == SortStateCreatEdit.DateCreateAsc ? SortStateCreatEdit.DateCreateDesc : SortStateCreatEdit.DateCreateAsc;
            ViewData["isFirstLessonSort"] = sortOrder == SortStateCreatEdit.isFirstLessonAsc ? SortStateCreatEdit.isFirstLessonDesc : SortStateCreatEdit.isFirstLessonAsc;
            ViewData["idSort"] = sortOrder == SortStateCreatEdit.idAsc ? SortStateCreatEdit.idDesc : SortStateCreatEdit.idAsc;
            switch (sortOrder)
            {
                case SortStateCreatEdit.SurnameDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.NameAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.name).ThenBy(s => s.surname).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.NameDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.name).ThenBy(s => s.surname).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.PatronymicAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.patronymic).ThenBy(s => s.surname).ThenBy(s => s.name).ToList();
                    break;
                case SortStateCreatEdit.PatronymicDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.patronymic).ThenBy(s => s.surname).ThenBy(s => s.name).ToList();
                    break;
                case SortStateCreatEdit.MailAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.mail).ToList();
                    break;
                case SortStateCreatEdit.MailDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.mail).ToList();
                    break;
                case SortStateCreatEdit.PhoneNumberAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.phoneNumber).ToList();
                    break;
                case SortStateCreatEdit.PhoneNumberDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.phoneNumber).ToList();
                    break;
                case SortStateCreatEdit.DateOfBirthAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.dateOfBirth).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.DateOfBirthDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.dateOfBirth).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.LevelOfEnglishAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.LevelOfEnglish).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.LevelOfEnglishDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.LevelOfEnglish).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.DateCreateAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.dateCreate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.DateCreateDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.dateCreate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;

                case SortStateCreatEdit.isFirstLessonAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.isFirstLesson).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.isFirstLessonDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.isFirstLesson).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortStateCreatEdit.idAsc:
                    el.studentBig = el.studentBig.OrderBy(s => s.Id).ToList();
                    break;
                case SortStateCreatEdit.idDesc:
                    el.studentBig = el.studentBig.OrderByDescending(s => s.Id).ToList();
                    break;

                default:
                    el.studentBig = el.studentBig.OrderBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
            }
            return el;
        }
        [Authorize(Roles = "admin")]
        private void GetListVAsync()
        {
            //var ellev = Mapper.Map<IEnumerable<levelOfEnglishBL>, List<levelOfEnglishPL>>(BLService_.levelOfEnglishBL.GetAll().Where(p=>p.disable=false)).OrderBy(p => p.level);
            //ViewBag.levelOfEnglishSL = new SelectList(ellev.OrderBy(p => p.level), "level", "level");
            var ellev = BLService_.levelOfEnglishBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.level).ToList();
            ellev.Insert(0, new levelOfEnglishBL { Id = 0, level = "" });
            ViewBag.levelOfEnglishSL = new SelectList(ellev, "Id", "level");


            var educationalMaterials = BLService_.educationalMaterialBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.name).ToList();
            educationalMaterials.Insert(0, new educationalMaterialBL { Id = 0, name = "" });
            ViewBag.educationalMaterials = new SelectList(educationalMaterials, "Id", "name");
        }
    }
}
