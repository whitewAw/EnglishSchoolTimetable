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
    class parentsOfStudentBLRepository: IRepositoryBL<parentsOfStudentBL>
    {
        IUnitOfWork Database { get; set; }
        public parentsOfStudentBLRepository(ref IUnitOfWork uow) => Database = uow;

        public IEnumerable<parentsOfStudentBL> GetAll() => new MapperConfiguration(cfg => cfg.CreateMap<parentsOfStudent, parentsOfStudentBL>()).CreateMapper().Map<IEnumerable<parentsOfStudent>, List<parentsOfStudentBL>>(Database.ParentsOfStudents.GetAll());

        public int Add(parentsOfStudentBL el)
        {
            try
            {
                el.name = el.name.Trim();
                if (!string.IsNullOrEmpty(el.surname))
                    el.surname = el.surname.Trim();
                if (!string.IsNullOrEmpty(el.mail))
                    el.mail = el.mail.Trim();
                el.phoneNumber = el.phoneNumber.Trim();
                if (!string.IsNullOrEmpty(el.patronymic))
                    el.patronymic = el.patronymic.Trim();
                var searchTemp = el.name + el.surname + el.mail + el.phoneNumber+el.patronymic;
                if (Database.ParentsOfStudents.Find(p => (p.name + p.surname + p.mail + p.phoneNumber+ p.patronymic) == searchTemp).FirstOrDefault() == null)
                {
                    var elAdd = new MapperConfiguration(cfg => cfg.CreateMap<parentsOfStudentBL, parentsOfStudent>()).CreateMapper().Map<parentsOfStudentBL, parentsOfStudent>(el);
                    Database.ParentsOfStudents.Create(ref elAdd);
                    Database.Save();
                    return elAdd.Id;
                }
            }
            catch { }
            return 0;
        }

        public parentsOfStudentBL Get(int id) => new MapperConfiguration(cfg => cfg.CreateMap<parentsOfStudent, parentsOfStudentBL>()).CreateMapper().Map<parentsOfStudent, parentsOfStudentBL>(Database.ParentsOfStudents.Get(id));

        public parentsOfStudentBL GetFullInfo(int id) => throw new NotImplementedException();

        public int Update(parentsOfStudentBL El)
        {
            El.name = El.name.Trim();
            if (!string.IsNullOrEmpty(El.surname))
                El.surname = El.surname.Trim();
            if (!string.IsNullOrEmpty(El.mail))
                El.mail = El.mail.Trim();
            El.phoneNumber = El.phoneNumber.Trim();
            if (!string.IsNullOrEmpty(El.patronymic))
                El.patronymic = El.patronymic.Trim();
            var searchTemp = El.name + El.surname + El.mail + El.phoneNumber + El.patronymic;
            if (Database.ParentsOfStudents.Find(p => (p.name + p.surname + p.mail + p.phoneNumber + p.patronymic) == searchTemp && p.Id != El.Id).FirstOrDefault() == null)
            {
                var el = Database.ParentsOfStudents.Get(El.Id);
                if (el == null)
                    return 2;
                el.mail = El.mail;
                el.name = El.name;
                el.patronymic = El.patronymic;
                el.phoneNumber = El.phoneNumber;
                el.surname = El.surname;
                el.disable = El.disable;
                Database.ParentsOfStudents.Update(el);
                Database.Save();
                return 1;
            }
            return 0;
        }

        public void Delete(int id)
        {
            Database.ParentsOfStudents.Delete(id);
            Database.Save();
        }
        public parentsOfStudentBL GetEmpty() => throw new NotImplementedException();
        public IEnumerable<parentsOfStudentBL> TakeBetweenDates(DateTime? start, DateTime? finish) => throw new NotImplementedException();
    }
}
