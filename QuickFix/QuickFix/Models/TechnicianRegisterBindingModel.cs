using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFix.Models
{
    public class TechnicianRegisterBindingModel
    {
        public string FullName { get; set; }
        //public string LastName { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public int TType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte[] Photo { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string Locality { get; set; }

    }
}
