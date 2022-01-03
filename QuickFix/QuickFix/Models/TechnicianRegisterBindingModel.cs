using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFix.Models
{
    public class TechnicianRegisterBindingModel
    {
          public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int TType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int WorkCount { get; set; }
       // public Nullable<bool> IsApproved { get; set; }
        // public string Password { get; set; }
        public byte[] Photo { get; set; }
        public string Password { get; set; }
     


    }
}
