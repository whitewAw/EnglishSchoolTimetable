using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.model;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Repository
{
    public class AdminsListBLRepository: IRepositoryBL<AdminListBL>
    {
        IUnitOfWork Database { get; set; }
        public AdminsListBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<AdminListBL> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AdminList, AdminListBL>()
            .ForMember("IdAdmin", opt => opt.MapFrom(c => ""))
            ).CreateMapper();
            return mapper.Map<IEnumerable<AdminList>, List<AdminListBL>>(Database.AdminLists.GetAll());
        }

        public int Add(AdminListBL AdminEl)
        {
            try
            {
                AdminEl.name = AdminEl.name.Trim();
                if (!string.IsNullOrEmpty(AdminEl.surname))
                    AdminEl.surname = AdminEl.surname.Trim();
                if (!string.IsNullOrEmpty(AdminEl.mail))
                    AdminEl.mail = AdminEl.mail.Trim();
                if (!string.IsNullOrEmpty(AdminEl.phoneNumber))
                    AdminEl.phoneNumber = AdminEl.phoneNumber.Trim();
                var searchTemp = AdminEl.name + AdminEl.surname + AdminEl.mail + AdminEl.phoneNumber;
                if (Database.AdminLists.Find(p => (p.name + p.surname + p.mail + p.phoneNumber)== searchTemp).FirstOrDefault() == null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AdminListBL, AdminList>()).CreateMapper();
                    AdminList el = mapper.Map<AdminListBL, AdminList>(AdminEl);
                    el.dateCreate = DateTime.Today;
                    Database.AdminLists.Create(ref el);
                    Database.Save();
                    return el.Id;
                }
            }
            catch {  }
            return 0;
        }

        public AdminListBL Get(int el)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AdminList, AdminListBL>().ForMember("IdAdmin", opt => opt.MapFrom(c => ""))).CreateMapper();
            return mapper.Map<AdminList, AdminListBL>(Database.AdminLists.Get(el));
        }

        public int Update(AdminListBL AdminEl)
        {
            AdminEl.name = AdminEl.name.Trim();
            if (!string.IsNullOrEmpty(AdminEl.surname))
                AdminEl.surname = AdminEl.surname.Trim();
            if (!string.IsNullOrEmpty(AdminEl.mail))
                AdminEl.mail = AdminEl.mail.Trim();
            if (!string.IsNullOrEmpty(AdminEl.phoneNumber))
                AdminEl.phoneNumber = AdminEl.phoneNumber.Trim();
            var searchTemp = AdminEl.name + AdminEl.surname + AdminEl.mail + AdminEl.phoneNumber;
            if (Database.AdminLists.Find(p => (p.name + p.surname + p.mail + p.phoneNumber) == searchTemp && p.Id != AdminEl.Id).FirstOrDefault() == null)
            {
                AdminList old = Database.AdminLists.Get(AdminEl.Id);
                if (old == null)
                    return 2;

                old.dateOfBirth = AdminEl.dateOfBirth;
                old.isInStaff = AdminEl.isInStaff;
                old.mail = AdminEl.mail;
                old.name = AdminEl.name;
                old.notes = AdminEl.notes;
                old.phoneNumber = AdminEl.phoneNumber;
                old.surname = AdminEl.surname;
                Database.AdminLists.Update(old);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public AdminListBL GetFullInfo(int el)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AdminList, AdminListBL>()).CreateMapper();
            return mapper.Map<AdminList, AdminListBL>(Database.AdminLists.Get(el));
        }

        public void Delete(int i)
        {
            Database.AdminLists.Delete(i);
            Database.Save();
        }

        public AdminListBL GetEmpty() => throw new NotImplementedException();

        public IEnumerable<AdminListBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}




