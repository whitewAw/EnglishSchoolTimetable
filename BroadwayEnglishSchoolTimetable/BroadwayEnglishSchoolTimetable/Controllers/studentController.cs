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
    public class studentController : Controller
    {
        IBLService BLService_;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public studentController(IBLService serv) => BLService_ = serv;
        protected override void Dispose(bool disposing)
        {
            BLService_.Dispose();
            base.Dispose(disposing);
        }

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
            ParentAsc,
            ParentDesc,
            FormOfEducationAsc,
            FormOfEducationDesc,
            EducationalMaterialsAsc,
            EducationalMaterialsDesc,
            LevelOfEnglishAsc,
            LevelOfEnglishDesc,
            DateCreateAsc,
            DateCreateDesc,
            isFirstLessonAsc,
            isFirstLessonDesc,
            idAsc,
            idDesc,
            DisableAsc,
            DisableDesc,
        }

        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        [Authorize(Roles = "admin")]
        public ActionResult Index(string levelName, string FormOfEducationName, string EducationalMaterialsName, string surnameS, string nameS, SortState sortOrder = SortState.SurnameAsc, bool isFirstLess=false)
        {

            List<studentPL> el = Mapper.Map<IEnumerable<studentBL>, List<studentPL>>(BLService_.studentBL.GetAll());
            if (!string.IsNullOrEmpty(surnameS))
            {
                el = el.Where(p => !string.IsNullOrEmpty(p.surname) && p.surname.Contains(surnameS)).ToList();
                ViewData["surnameS"] = surnameS;
            }
            if (!string.IsNullOrEmpty(nameS))
            {
                el = el.Where(p => p.name.Contains(nameS)).ToList();
                ViewData["nameS"] = nameS;
            }

            if (!string.IsNullOrEmpty(levelName))
                el = el.Where(p => p.LevelOfEnglish==levelName).ToList();
         
            if (!string.IsNullOrEmpty(FormOfEducationName))
                el = el.Where(p => p.FormOfEducation== FormOfEducationName).ToList();

            if (!string.IsNullOrEmpty(EducationalMaterialsName))
                el = el.Where(p => p.EducationalMaterials == EducationalMaterialsName).ToList();

            ViewData["isFirstLess"] = isFirstLess;
            if (isFirstLess == true)
                  el = el.Where(p => p.isFirstLesson==true).ToList();
    
            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PatronymicSort"] = sortOrder == SortState.PatronymicAsc ? SortState.PatronymicDesc : SortState.PatronymicAsc;
            ViewData["MailSort"] = sortOrder == SortState.MailAsc ? SortState.MailDesc : SortState.MailAsc;
            ViewData["PhoneNumberSort"] = sortOrder == SortState.PhoneNumberAsc ? SortState.PhoneNumberDesc : SortState.PhoneNumberAsc;
            ViewData["DateOfBirthSort"] = sortOrder == SortState.DateOfBirthAsc ? SortState.DateOfBirthDesc : SortState.DateOfBirthAsc;
            ViewData["ParentSort"] = sortOrder == SortState.ParentAsc ? SortState.ParentDesc : SortState.ParentAsc;
            ViewData["FormOfEducationSort"] = sortOrder == SortState.FormOfEducationAsc ? SortState.FormOfEducationDesc : SortState.FormOfEducationAsc;
            ViewData["EducationalMaterialsSort"] = sortOrder == SortState.EducationalMaterialsAsc ? SortState.EducationalMaterialsDesc : SortState.EducationalMaterialsAsc;
            ViewData["LevelOfEnglishSort"] = sortOrder == SortState.LevelOfEnglishAsc ? SortState.LevelOfEnglishDesc : SortState.LevelOfEnglishAsc;
            ViewData["DateCreateSort"] = sortOrder == SortState.DateCreateAsc ? SortState.DateCreateDesc : SortState.DateCreateAsc;
            ViewData["isFirstLessonSort"] = sortOrder == SortState.isFirstLessonAsc ? SortState.isFirstLessonDesc : SortState.isFirstLessonAsc;
            ViewData["idSort"] = sortOrder == SortState.idAsc ? SortState.idDesc : SortState.idAsc;
            ViewData["DateCreateSort"] = sortOrder == SortState.DateCreateAsc ? SortState.DateCreateDesc : SortState.DateCreateAsc;
            ViewData["DisableSort"] = sortOrder == SortState.DisableAsc ? SortState.DisableDesc : SortState.DisableAsc;

            switch (sortOrder)
            {
                case SortState.SurnameDesc:
                    el = el.OrderByDescending(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
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
                    el = el.OrderByDescending(s => s.dateOfBirth).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.ParentAsc:
                    el = el.OrderBy(s => s.Parent).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.ParentDesc:
                    el = el.OrderByDescending(s => s.Parent).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.FormOfEducationAsc:
                    el = el.OrderBy(s => s.FormOfEducation).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.FormOfEducationDesc:
                    el = el.OrderByDescending(s => s.FormOfEducation).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.EducationalMaterialsAsc:
                    el = el.OrderBy(s => s.EducationalMaterials).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.EducationalMaterialsDesc:
                    el = el.OrderByDescending(s => s.EducationalMaterials).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.LevelOfEnglishAsc:
                    el = el.OrderBy(s => s.LevelOfEnglish).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.LevelOfEnglishDesc:
                    el = el.OrderByDescending(s => s.LevelOfEnglish).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.DateCreateAsc:
                    el = el.OrderBy(s => s.dateCreate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.DateCreateDesc:
                    el = el.OrderByDescending(s => s.dateCreate).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;

                case SortState.isFirstLessonAsc:
                    el = el.OrderBy(s => s.isFirstLesson).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.isFirstLessonDesc:
                    el = el.OrderByDescending(s => s.isFirstLesson).ThenBy(s => s.surname).ThenBy(s => s.name).ThenBy(s => s.patronymic).ToList();
                    break;
                case SortState.idAsc:
                    el = el.OrderBy(s => s.Id).ToList();
                    break;
                case SortState.idDesc:
                    el = el.OrderByDescending(s => s.Id).ToList();
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
            var listlev = BLService_.levelOfEnglishBL.GetAll().Where(p=>p.disable==false).OrderBy(p => p.level).ToList();
            listlev.Insert(0, new levelOfEnglishBL { Id = 0, level = "" });
            ViewData["levelName"] = new SelectList(listlev, "level", "level");
            ViewData["selLevel"] = levelName;



            var listform = BLService_.formOfEducationBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.form).ToList();
            listform.Insert(0, new formOfEducationBL { Id = 0, form = "" });
            ViewData["FormOfEducationName"] = new SelectList(listform, "form", "form");
            ViewData["selFormOfEducation"] = FormOfEducationName;

            var edicmaterform = BLService_.educationalMaterialBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.name).ToList();
            edicmaterform.Insert(0, new educationalMaterialBL { Id = 0,  name= "" });
            ViewData["EducationalMaterialsName"] = new SelectList(edicmaterform, "name", "name");
            ViewData["selEducationalMaterials"] = EducationalMaterialsName;


            return View(el);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            GetListVAsync();
            return View(new studentPL { isFirstLesson= true});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "name,surname,patronymic,mail,phoneNumber,dateOfBirth,idParent,idFormOfEducation,idEducationalMaterials,idLevelOfEnglish,notes,isFirstLesson")] studentPL studEl)
        {
            if (ModelState.IsValid)
            {
                if (studEl.dateOfBirth != null)
                {
                    if ((DateTime.Now - studEl.dateOfBirth).Value.TotalDays < 18 * 365 && studEl.idParent == 0)
                    {
                        GetListVAsync();
                        ModelState.AddModelError("", "студент младше 18 лет, необходимо выбрать родителя");
                        return View(studEl);
                    }
                }
                if (studEl.idFormOfEducation == 0)
                {
                    GetListVAsync();
                    ModelState.AddModelError("", "не выбрана форма обучения");
                    return View(studEl);
                }

                studEl.name = studEl.name.Trim();
                if (!string.IsNullOrEmpty(studEl.surname))
                    studEl.surname = studEl.surname.Trim();
                if (!string.IsNullOrEmpty(studEl.patronymic))
                    studEl.patronymic = studEl.patronymic.Trim();
                if (!string.IsNullOrEmpty(studEl.mail))
                {
                    studEl.mail = studEl.mail.ToLower().Trim();
                    studEl.mail = studEl.mail.ToLower();
                }
                if (!string.IsNullOrEmpty(studEl.notes))
                    studEl.notes = studEl.notes.Trim();
                if (string.IsNullOrEmpty(studEl.surname))
                {
                    GetListVAsync();
                    ModelState.AddModelError("", "не указана фамилия");
                    return View(studEl);
                }

                if (!string.IsNullOrEmpty(studEl.mail))
                {
                    string userName = (studEl.name + " " + studEl.surname + " " + studEl.patronymic).Trim();
                    ApplicationUser userdb = new ApplicationUser { UserName = userName, Email = studEl.mail };
                    string pas = RandomString(random.Next(10, 15));
                    var result = await UserManager.CreateAsync(userdb, pas);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(userdb.Id, "learner");
                        try
                        {
                            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userdb.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userdb.Id, code = code }, protocol: Request.Url.Scheme);
                            await UserManager.SendEmailAsync(userdb.Id, "Подтверждение учетной записи", userName + " подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a> <br> Пароль для входа: " + pas);

                            await UserManager.SetLockoutEnabledAsync(userdb.Id, false);
                            await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));

                            studentBL user = Mapper.Map<studentPL, studentBL>(studEl);
                            user.IdStudent = userdb.Id;
                            if (!string.IsNullOrEmpty(user.IdStudent))
                            {
                                user.disable = false;
                                int index = BLService_.studentBL.Add(user);
                                if (index != 0)
                                    return RedirectToAction("Index");
                            }
                        }
                        catch { }
                        await UserManager.DeleteAsync(userdb);
                    }
                    foreach (var el in result.Errors)
                        ModelState.AddModelError("", el);
                }
                else
                {
                    studentBL user = Mapper.Map<studentPL, studentBL>(studEl);
                    int index = BLService_.studentBL.Add(user);
                    if (index != 0)
                        return RedirectToAction("Index");
                }
            }
            GetListVAsync();
            ModelState.AddModelError("", "ошибка при создании, попробуйте позже");
            return View(studEl);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNO0123456789opqrstuvwxyz!@";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            studentBL el = BLService_.studentBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var elEdit = Mapper.Map<studentBL, studentPL>(el);
            GetListVAsync();
            return View(elEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,name,surname,patronymic,mail,phoneNumber,dateOfBirth,idParent,idFormOfEducation,idEducationalMaterials,idLevelOfEnglish,notes,isFirstLesson,disable")] studentPL studEl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (studEl.dateOfBirth != null)
                    {
                        if ((DateTime.Now - studEl.dateOfBirth).Value.TotalDays < 18 * 365 && studEl.idParent == 0)
                        {
                            GetListVAsync();
                            ModelState.AddModelError("", "студент младше 18 лет, необходимо выбрать родителя");
                            return View(studEl);
                        }
                    }
                    if (studEl.idFormOfEducation == 0)
                    {
                        GetListVAsync();
                        ModelState.AddModelError("", "не выбрана форма обучения");
                        return View(studEl);
                    }

                    studEl.name = studEl.name.Trim();
                    if (!string.IsNullOrEmpty(studEl.surname))
                        studEl.surname = studEl.surname.Trim();
                    if (!string.IsNullOrEmpty(studEl.patronymic))
                        studEl.patronymic = studEl.patronymic.Trim();
            
                    var old = BLService_.studentBL.GetFullInfo(studEl.Id);

                    if ((studEl.name + " " + studEl.surname + " " + studEl.patronymic) != (old.name + " " + old.surname + " " + old.patronymic))
                        if (UserManager.FindByName(studEl.name + " " + studEl.surname + " " + studEl.patronymic) != null)
                        {
                            ModelState.AddModelError("", "Подобная запись уже существует");
                            GetListVAsync();
                            return View(studEl);
                        }

                    studEl.IdStudent = old.IdStudent;
                    if (!string.IsNullOrEmpty(studEl.mail))
                    {
                        studEl.mail = studEl.mail.Trim();
                        studEl.mail = studEl.mail.ToLower();
                    }
                    if (!string.IsNullOrEmpty(studEl.notes))
                        studEl.notes = studEl.notes.Trim();

                    if (!string.IsNullOrEmpty(old.mail) && !string.IsNullOrEmpty(studEl.mail))
                    {
                        ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdStudent);
                        if (userdb != null)
                        {


                            if (old.mail != studEl.mail)
                            {
                                await UserManager.SetEmailAsync(old.IdStudent, old.mail);
                                await UserManager.SendEmailAsync(old.IdStudent, "Изменение почты", "Изменение вашей учетной записи, новая почта: " + studEl.mail);
                                await UserManager.SetEmailAsync(old.IdStudent, studEl.mail);
                                string code = await UserManager.GenerateEmailConfirmationTokenAsync(old.IdStudent);
                                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = old.IdStudent, code = code }, protocol: Request.Url.Scheme);
                                await UserManager.SendEmailAsync(old.IdStudent, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a>");
                            }

                            if (studEl.name != old.name || studEl.surname != old.surname || studEl.patronymic != old.patronymic)
                            {
                                userdb.UserName = (studEl.name + " " + studEl.surname + " " + studEl.patronymic).Trim();
                                await UserManager.UpdateAsync(userdb);
                            }
                            await UserManager.SetLockoutEnabledAsync(old.IdStudent, false);
                            await UserManager.SetLockoutEndDateAsync(old.IdStudent, new DateTime(9999, 12, 30));
                        }
                        else
                            studEl.mail = null;

                    }

                    if (string.IsNullOrEmpty(old.mail) && !string.IsNullOrEmpty(studEl.mail))
                    {
                        string userName = studEl.name + " " + studEl.surname + " " + studEl.patronymic;
                        userName = userName.Trim();
                        ApplicationUser userdb = new ApplicationUser { UserName = userName, Email = studEl.mail };
                        string pas = RandomString(random.Next(10, 15));
                        var result = await UserManager.CreateAsync(userdb, pas);
                        if (result.Succeeded)
                        {
                            UserManager.AddToRole(userdb.Id, "learner");
                            try
                            {
                                string code = await UserManager.GenerateEmailConfirmationTokenAsync(userdb.Id);
                                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userdb.Id, code = code }, protocol: Request.Url.Scheme);
                                await UserManager.SendEmailAsync(userdb.Id, "Подтверждение учетной записи", userName + " подтвердите вашу учетную запись, кликните <a href=\"" + callbackUrl + "\">здесь</a> <br> Пароль для входа: " + pas);

                                await UserManager.SetLockoutEnabledAsync(userdb.Id, false);
                                await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));

                                studEl.IdStudent = userdb.Id;
                                if (string.IsNullOrEmpty(studEl.IdStudent))
                                {
                                    ModelState.AddModelError("", "не получилось добавить почту");
                                    GetListVAsync();
                                    await UserManager.DeleteAsync(userdb);
                                    return View(studEl);
                                }
                            }
                            catch { await UserManager.DeleteAsync(userdb); }
                        }
                    }
                    if (!string.IsNullOrEmpty(old.mail) && string.IsNullOrEmpty(studEl.mail))
                    {
                        ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdStudent);
                        if (userdb != null)
                        {
                            await UserManager.SetLockoutEnabledAsync(userdb.Id, true);
                            await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));
                            await UserManager.DeleteAsync(userdb);
                            studEl.IdStudent = null;
                        }
                    }

                    studentBL student = Mapper.Map<studentPL, studentBL>(studEl);
                    if(BLService_.studentBL.Update(student)==1)
                        return RedirectToAction("Index");
                }
                catch {  }
                ModelState.AddModelError("", "ошибка редактирования, попробуйте позже");
            }
            GetListVAsync();
            return View(studEl);
        }
       //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult Details(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            studentBL el = BLService_.studentBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var elEdit = Mapper.Map<studentBL, studentPL>(el);
            GetListVAsync();
            return View(elEdit);
        }

        [Authorize(Roles = "supAdmin")]
        public ActionResult Delete(int id=-1)
        {
            if (id == -1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            studentBL el = BLService_.studentBL.Get(id);
            if (el == null)
                return HttpNotFound();
            var elEdit = Mapper.Map<studentBL, studentPL>(el);
            GetListVAsync();
            return View(elEdit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "supAdmin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var old = BLService_.studentBL.GetFullInfo(id);
            if (!string.IsNullOrEmpty(old.IdStudent))
            {
                ApplicationUser userdb = await UserManager.FindByIdAsync(old.IdStudent);
                if (userdb!=null)
                {
                    await UserManager.SetLockoutEnabledAsync(userdb.Id, true);
                    await UserManager.SetLockoutEndDateAsync(userdb.Id, new DateTime(9999, 12, 30));
                    await UserManager.DeleteAsync(userdb);
                }
            }
            BLService_.studentBL.Delete(old.Id);
            return RedirectToAction("Index");
        }

        private void GetListVAsync()
        {
            var formOfEducation = BLService_.formOfEducationBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.form).ToList();
            formOfEducation.Insert(0, new formOfEducationBL { Id = 0, form = "" });
            ViewBag.formOfEducation = new SelectList(formOfEducation, "Id", "form");

            var educationalMaterials = BLService_.educationalMaterialBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.name).ToList();
            educationalMaterials.Insert(0, new educationalMaterialBL { Id = 0, name = "" });
            ViewBag.educationalMaterials = new SelectList(educationalMaterials, "Id", "name");

            var levelOfEnglish = BLService_.levelOfEnglishBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.level).ToList();
            levelOfEnglish.Insert(0, new levelOfEnglishBL { Id = 0, level = "" });
            ViewBag.levelOfEnglish = new SelectList(levelOfEnglish, "Id", "level");

            var parentsOfStudents = BLService_.parentsOfStudentBL.GetAll().Where(p => p.disable == false).OrderBy(p => p.surname).ThenBy(p => p.name).ThenBy(p => p.patronymic).ToList();
            parentsOfStudents.Insert(0, new parentsOfStudentBL { Id = 0, surname = "", name="", patronymic="" });
            ViewBag.parentsOfStudents = new SelectList(parentsOfStudents.ToDictionary(p => p.Id, p => p.surname + " " + p.name + " " + p.patronymic), "Key", "Value");
        }
    }
}
