using AutoMapper;
using BroadwayEnglishSchoolTimetable.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    [Authorize(Roles = "admin,teacher")]
    public class teacherController : Controller
    {
        public enum SortState
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
            StartDateAsc,
            StartDateDesc,
            EndDateAsc,
            EndDateDesc,
            isforBeginnersAsc,
            isforBeginnersDesc,
            DateCreateAsc,
            DateCreateDesc
        }

        IBLService BLService_;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public teacherController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        [Authorize(Roles = "admin")]
        public ActionResult Index(string levelName, string surnameS, string nameS, SortState sortOrder = SortState.SurnameAsc, bool inStaff = false)
        {
            List<teacherPL> el = Mapper.Map<IEnumerable<teacherBL>, List<teacherPL>>(BLService_.teacherBL.GetAll());

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

            ViewData["inStaff"] = inStaff;
            if(inStaff==true)
            {
                el= el.Where(p=>p.endDate==null).ToList();
            }

            if (!string.IsNullOrEmpty(levelName))
            {
                el = el.Where(p => p.NamelevelOfEnglish.Contains(levelName)).ToList();
            }




            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PatronymicSort"] = sortOrder == SortState.PatronymicAsc ? SortState.PatronymicDesc : SortState.PatronymicAsc;
            ViewData["MailSort"] = sortOrder == SortState.MailAsc ? SortState.MailDesc : SortState.MailAsc;
            ViewData["PhoneNumberSort"] = sortOrder == SortState.PhoneNumberAsc ? SortState.PhoneNumberDesc : SortState.PhoneNumberAsc;
            ViewData["DateOfBirthSort"] = sortOrder == SortState.DateOfBirthAsc ? SortState.DateOfBirthDesc : SortState.DateOfBirthAsc;
            ViewData["StartDateSort"] = sortOrder == SortState.StartDateAsc ? SortState.StartDateDesc : SortState.StartDateAsc;
            ViewData["EndDateSort"] = sortOrder == SortState.EndDateAsc ? SortState.EndDateDesc : SortState.EndDateAsc;
            ViewData["isforBeginners"] = sortOrder == SortState.isforBeginnersAsc ? SortState.isforBeginnersDesc : SortState.isforBeginnersAsc;
            ViewData["DateCreateSort"] = sortOrder == SortState.DateCreateAsc ? SortState.DateCreateDesc : SortState.DateCreateAsc;



            switch (sortOrder)
            {
                case SortState.SurnameDesc:
                    el = el.OrderByDescending(s => s.surname).ThenBy(s => s.name).ThenBy(s=>s.patronymic).ToList();
                    break;
                case SortState.NameAsc:
                    el = el.OrderBy(s => s.name).ThenBy(s => s.surname).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.NameDesc:
                    el = el.OrderByDescending(s => s.name).ThenBy(s => s.surname).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.PatronymicAsc:
                    el = el.OrderBy(s => s.patronymic).ThenBy(s => s.surname).ThenBy(s => s.name).ToList();
                    break;
                case SortState.PatronymicDesc:
                    el = el.OrderByDescending(s => s.patronymic).ThenBy(s => s.surname).ThenBy(s => s.name).ToList();
                    break;
                case SortState.MailAsc:
                    el = el.OrderBy(s => s.mail).ToList();
                    break;
                case SortState.MailDesc:
                    el = el.OrderByDescending(s => s.mail).ToList();
                    break;
                case SortState.PhoneNumberAsc:
                    el = el.OrderBy(s => s.phoneNumber).ToList();
                    break;
                case SortState.PhoneNumberDesc:
                    el = el.OrderByDescending(s => s.phoneNumber).ToList();
                    break;
                case SortState.DateOfBirthAsc:
                    el = el.OrderBy(s => s.dateOfBirth).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.DateOfBirthDesc:
                    el = el.OrderByDescending(s => s.dateOfBirth).ThenBy(s=>s.surname).ThenBy(s=>s.name).ThenBy(s=>s.patronymic).ToList();
                    break;
                case SortState.StartDateAsc:
                    el = el.OrderBy(s => s.startDate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.StartDateDesc:
                    el = el.OrderByDescending(s => s.startDate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.EndDateAsc:
                    el = el.OrderBy(s => s.endDate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.EndDateDesc:
                    el = el.OrderByDescending(s => s.endDate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.isforBeginnersAsc:
                    el = el.OrderBy(s => s.forBeginners).ToList();
                    break;
                case SortState.isforBeginnersDesc:
                    el = el.OrderByDescending(s => s.forBeginners).ToList();
                    break;
               case SortState.DateCreateAsc:
                    el = el.OrderBy(s => s.dateCreate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.DateCreateDesc:
                    el = el.OrderByDescending(s => s.dateCreate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                default:
                    el = el.OrderBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
            }
            var listlev = BLService_.levelOfEnglishBL.GetAll().OrderBy(p => p.level).ToList();
            listlev.Insert(0, new levelOfEnglishBL { Id = 0, level = "" });
            ViewData["levelName"] = new SelectList(listlev, "level", "level");
            ViewData["selLevel"] = levelName;



            return View(el);
        }
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id = -1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            teacherBL el = BLService_.teacherBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<teacherBL, teacherPL>(el));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create() => View(Mapper.Map<teacherBL, teacherPL>(BLService_.teacherBL.GetEmpty()));


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "name,surname,patronymic,mail,phoneNumber,dateOfBirth,notes,startDate,forBeginners,levelOfEnglishInt,accessibilityOfTeacherBig")] teacherPL teachEl)
        {
            if (ModelState.IsValid)
            {
                teachEl.name = teachEl.name.Trim();
                teachEl.surname = teachEl.surname.Trim();
                if (!string.IsNullOrEmpty(teachEl.patronymic))
                    teachEl.patronymic = teachEl.patronymic.Trim();
                teachEl.mail = teachEl.mail.ToLower().Trim();
                if (!string.IsNullOrEmpty(teachEl.notes))
                    teachEl.notes = teachEl.notes.Trim();

                bool flag = false;
                var timeShcool = BLService_.openingHourBL.Get(1);
                foreach (var i in teachEl.accessibilityOfTeacherBig)
                {
                    if(i.startTime!=null && i.endTime == null)
                    {
                        ModelState.AddModelError("", "проверте время, указано начало но не указано конец");
                        flag = true;
                    }

                    if (i.startTime == null && i.endTime != null)
                    {
                        ModelState.AddModelError("", "проверте время, указано конец но не указано начало");
                        flag = true;
                    }
                    if (i.startTime != null && i.endTime != null)
                    {
                        if (i.startTime >= i.endTime)
                        {
                            ModelState.AddModelError("", "проверте время, начало не может быть позже окончания");
                            flag = true;
                        }
                        if (i.startTime < timeShcool.open || i.endTime > timeShcool.close)
                        {
                            ModelState.AddModelError("", String.Format("время выходит за пределы рабочего времени школы {0} - {1}",timeShcool.open.ToString(@"hh\:mm"), timeShcool.close.ToString(@"hh\:mm")));
                            flag = true;
                        }
                    }
                 
                }
                if (flag == true)
                    return View(teachEl);



                ApplicationUser userdb = new ApplicationUser { UserName = ((teachEl.name + " " + teachEl.surname+" "+ teachEl.patronymic).Trim()), Email = teachEl.mail };
                string pas = RandomString(random.Next(10, 15));
                var result = await UserManager.CreateAsync(userdb, pas);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(userdb.Id, "teacher");
                    UserManager.AddToRole(userdb.Id, "learner");
                    try
                    {
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(userdb.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userdb.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(userdb.Id, "Подтверждение учетной записи", userdb.UserName + " подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a> <br> Пароль для входа: " + pas);

                        await UserManager.SetLockoutEnabledAsync(userdb.Id, false);
                        await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));

                        teacherBL user = Mapper.Map<teacherPL, teacherBL>(teachEl);
                        user.IdTeacher = userdb.Id;
                        if (!string.IsNullOrEmpty(user.IdTeacher))
                        {
                            int index = BLService_.teacherBL.Add(user);
                            if (index != 0)
                                 return RedirectToAction("Index");
                        }
                    }
                    catch { }
                    await UserManager.DeleteAsync(userdb);
                    ModelState.AddModelError("", "ошибка при создании, попробуйте позже");
                }
                foreach (var el in result.Errors)
                    ModelState.AddModelError("", el);
            }
            return View(teachEl);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNO0123456789opqrstuvwxyz!@";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = -1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            teacherBL el = BLService_.teacherBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var elEdit = Mapper.Map<teacherBL, teacherPL>(el);
            return View(elEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,name,surname,patronymic,mail,phoneNumber,dateOfBirth,notes,startDate,forBeginners,endDate,levelOfEnglishInt,accessibilityOfTeacherBig")] teacherPL teachEl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var old = BLService_.teacherBL.GetFullInfo(teachEl.Id);

                    teachEl.name = teachEl.name.Trim();
                    if (!string.IsNullOrEmpty(teachEl.surname))
                        teachEl.surname = teachEl.surname.Trim();

                    if ((teachEl.name + " " + teachEl.surname ) != (old.name + " " + old.surname))
                        if (UserManager.FindByName(teachEl.name + " " + teachEl.surname) != null)
                        {
                            ModelState.AddModelError("", "Подобная запись уже существует");
                            return View(teachEl);
                        }

                    teachEl.mail = teachEl.mail.Trim();
                   
                    if (old.mail != teachEl.mail)
                    {
                        await UserManager.SetEmailAsync(old.IdTeacher, old.mail);
                        await UserManager.SendEmailAsync(old.IdTeacher, "Изменение почты", "Изменение вашей учетной записи, новая почта: " + teachEl.mail);
                        await UserManager.SetEmailAsync(old.IdTeacher, teachEl.mail);
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(old.IdTeacher);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = old.IdTeacher, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(old.IdTeacher, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a>");
                    }
                    teachEl.name = teachEl.name.Trim();
                    teachEl.surname = teachEl.surname.Trim();
                    if (!string.IsNullOrEmpty(teachEl.patronymic))
                        teachEl.patronymic = teachEl.patronymic.Trim();
                    if (!string.IsNullOrEmpty(teachEl.notes))
                        teachEl.notes = teachEl.notes.Trim();
                    if (teachEl.name != old.name || teachEl.surname != old.surname || teachEl.patronymic != old.patronymic)
                    {
                        ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdTeacher);
                        userdb.UserName = (teachEl.name + " " + teachEl.surname + " " + teachEl.patronymic).Trim();
                        await UserManager.UpdateAsync(userdb);
                    }

                    if (teachEl.endDate == null)
                        await UserManager.SetLockoutEnabledAsync(old.IdTeacher, false);
                    else
                        await UserManager.SetLockoutEnabledAsync(old.IdTeacher, true);
                    await UserManager.SetLockoutEndDateAsync(old.IdTeacher, new DateTime(9999, 12, 30));
                    teacherBL teach = Mapper.Map<teacherPL, teacherBL>(teachEl);
                    BLService_.teacherBL.Update(teach);
                    return RedirectToAction("Index");
                }
                catch { ModelState.AddModelError("", "ошибка редактирования, попробуйте позже"); }
            }
            return View(teachEl);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id = -1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            teacherBL el = BLService_.teacherBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<teacherBL, teacherPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "supAdmin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var old = BLService_.teacherBL.GetFullInfo(id);
            ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdTeacher);
            await UserManager.SetLockoutEnabledAsync(userdb.Id, true);
            await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));
            await UserManager.DeleteAsync(userdb);
            BLService_.teacherBL.Delete(old.Id);
            return RedirectToAction("Index");
        }
    }
}
