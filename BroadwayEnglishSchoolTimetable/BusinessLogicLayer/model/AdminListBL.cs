using System;
namespace BusinessLogicLayer.model
{
    public class AdminListBL
    {
        public int Id { get; set; }
        public string IdAdmin { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string notes { get; set; }
        public DateTime dateCreate { get; set; }
        public bool isInStaff { get; set; }
    }
}
