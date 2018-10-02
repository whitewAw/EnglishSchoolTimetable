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
    [Authorize(Roles = "admin")]
    public class AdminListsController : Controller
    {
        public enum SortState
        {
            SurnameAsc,
            SurnameDesc,
            NameAsc,
            NameDesc,
            PhoneNumberAsc,
            PhoneNumberDesc,
            MailAsc,
            MailDesc,
            isInStaffAsc,
            isInStaffDesc,
            DateOfBirthAsc,
            DateOfBirthDesc,
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

   
        public AdminListsController(IBLService serv) => BLService_ = serv;

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Index(string surnameS, string nameS, SortState sortOrder = SortState.SurnameAsc)
        {
            List<AdminListPL> el = Mapper.Map<IEnumerable<AdminListBL>, List<AdminListPL>>(BLService_.adminListBL.GetAll());

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
            ViewData["PhoneNumberSort"] = sortOrder == SortState.PhoneNumberAsc ? SortState.PhoneNumberDesc : SortState.PhoneNumberAsc;
            ViewData["MailSort"] = sortOrder == SortState.MailAsc ? SortState.MailDesc : SortState.MailAsc;
            ViewData["isInStaffSort"] = sortOrder == SortState.isInStaffAsc ? SortState.isInStaffDesc : SortState.isInStaffAsc;
            ViewData["DateOfBirthSort"] = sortOrder == SortState.DateOfBirthAsc ? SortState.DateOfBirthDesc : SortState.DateOfBirthAsc;
            ViewData["DateCreateSort"] = sortOrder == SortState.DateCreateAsc ? SortState.DateCreateDesc : SortState.DateCreateAsc;


            switch (sortOrder)
            {
                case SortState.SurnameDesc:
                    el = el.OrderByDescending(s => s.surname).ThenBy(s=>s.name).ToList();
                    break;
                case SortState.NameAsc:
                    el = el.OrderBy(s => s.name).ThenBy(s => s.surname).ToList();
                    break;
                case SortState.NameDesc:
                    el = el.OrderByDescending(s => s.name).ThenBy(s => s.surname).ToList();
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
                case SortState.isInStaffAsc:
                    el = el.OrderBy(s => s.isInStaff).ToList();
                    break;
                case SortState.isInStaffDesc:
                    el = el.OrderByDescending(s => s.isInStaff).ToList();
                    break;
                case SortState.DateOfBirthAsc:
                    el = el.OrderBy(s => s.dateOfBirth).ToList();
                    break;
                case SortState.DateOfBirthDesc:
                    el = el.OrderByDescending(s => s.dateOfBirth).ToList();
                    break;
                case SortState.DateCreateAsc:
                    el = el.OrderBy(s => s.dateCreate).ToList();
                    break;
                case SortState.DateCreateDesc:
                    el = el.OrderByDescending(s => s.dateCreate).ToList();
                    break;
                default:
                    el = el.OrderBy(s => s.surname).ThenBy(s => s.name).ToList();
                    break;
            }

            return View(el);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Create()
        {
            AdminListPL adminEl = new AdminListPL();
            adminEl.isInStaff=true;
            return View(adminEl);
        }

        [Authorize(Roles = "supAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "surname,name,mail,phoneNumber,dateOfBirth,notes,isInStaff")] AdminListPL adminEl)
        {
            if (ModelState.IsValid)
            {
                adminEl.mail = adminEl.mail.ToLower().Trim();
                adminEl.name = adminEl.name.Trim();
                adminEl.surname = adminEl.surname.Trim();
                if (!string.IsNullOrEmpty(adminEl.notes))
                    adminEl.notes = adminEl.notes.Trim();

                ApplicationUser userdb = new ApplicationUser { UserName = (adminEl.name + " " + adminEl.surname), Email = adminEl.mail };
                string pas = RandomString(random.Next(10, 15));
                var result = await UserManager.CreateAsync(userdb, pas);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(userdb.Id, "admin");
                    UserManager.AddToRole(userdb.Id, "teacher");
                    UserManager.AddToRole(userdb.Id, "learner");
                    if (adminEl.mail.Contains("paderno.victoria@gmail.com"))
                        UserManager.AddToRole(userdb.Id, "supAdmin");

                    try
                    {
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(userdb.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userdb.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(userdb.Id, "Подтверждение учетной записи", userdb.UserName + " подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a> <br> Пароль для входа: " + pas);

                        await UserManager.SetLockoutEnabledAsync(userdb.Id, !adminEl.isInStaff);
                        await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));

                        AdminListBL user = Mapper.Map<AdminListPL, AdminListBL>(adminEl);
                        user.IdAdmin = userdb.Id;
                        if (!string.IsNullOrEmpty(user.IdAdmin))
                            if (BLService_.adminListBL.Add(user)!=0)
                                return RedirectToAction("Index");
                    }
                    catch {  }
                    await UserManager.DeleteAsync(userdb);
                    ModelState.AddModelError("", "ошибка при создании, попробуйте позже");
                }
                foreach (var el in result.Errors)
                    ModelState.AddModelError("", el);
            }
            return View(adminEl);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNO0123456789opqrstuvwxyz!@";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AdminListBL el = BLService_.adminListBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<AdminListBL, AdminListPL>(el));
        }

        [Authorize(Roles = "supAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,surname,name,mail,phoneNumber,dateOfBirth,notes,isInStaff")] AdminListPL adminEl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    adminEl.name = adminEl.name.Trim();
                    adminEl.surname = adminEl.surname.Trim();
            
                    var old = BLService_.adminListBL.GetFullInfo(adminEl.Id);
                    if ((adminEl.name + " " + adminEl.surname) != (old.name + " " + old.surname))
                        if (UserManager.FindByName(adminEl.name + " " + adminEl.surname) != null)
                        {
                            ModelState.AddModelError("", "Подобная запись уже существует");
                            return View(adminEl);
                        }
                    adminEl.mail = adminEl.mail.Trim();
                    if (!string.IsNullOrEmpty(adminEl.notes))
                        adminEl.notes = adminEl.notes.Trim();

                    if (old.mail != adminEl.mail)
                    {
                        await UserManager.SetEmailAsync(old.IdAdmin, old.mail);
                        await UserManager.SendEmailAsync(old.IdAdmin, "Изменение почты", "Изменение вашей учетной записи, новая почта: " + adminEl.mail);
                        await UserManager.SetEmailAsync(old.IdAdmin, adminEl.mail);
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(old.IdAdmin);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = old.IdAdmin, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(old.IdAdmin, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a>");
                    }
                   
                    if (adminEl.name != old.name || adminEl.surname != old.surname)
                    {
                        ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdAdmin);
                        userdb.UserName = adminEl.name + " " + adminEl.surname;
                        await UserManager.UpdateAsync(userdb);
                    }

                    await UserManager.SetLockoutEnabledAsync(old.IdAdmin, !adminEl.isInStaff);
                    await UserManager.SetLockoutEndDateAsync(old.IdAdmin, new DateTime(9999, 12, 30));
                    AdminListBL admin = Mapper.Map<AdminListPL, AdminListBL>(adminEl);
                    int res = BLService_.adminListBL.Update(admin);
                    if (res == 1)
                        return RedirectToAction("Index");
                    else if (res == 0)
                    {
                        ModelState.AddModelError("", "Подобная запись уже существует");
                        return View(adminEl);
                    }
                }
                catch { }
                ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
            }
            return View(adminEl);
        }

       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AdminListBL el = BLService_.adminListBL.Get(id);
            if(el == null)
                return HttpNotFound();
            return View(Mapper.Map<AdminListBL, AdminListPL>(el));
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AdminListBL el = BLService_.adminListBL.Get(id);
            if (el == null)
                return HttpNotFound();
            return View(Mapper.Map<AdminListBL, AdminListPL>(el));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "supAdmin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var old = BLService_.adminListBL.GetFullInfo(id);
            ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdAdmin);
            await UserManager.SetLockoutEnabledAsync(userdb.Id, true);
            await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));
            await UserManager.DeleteAsync(userdb);
            BLService_.adminListBL.Delete(id);
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }
    }
}