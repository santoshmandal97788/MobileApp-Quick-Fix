using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFix.Models
{
    public class RegisterBindingModel
    {
        public string FullName { get; set; }
        //public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte[] Photo { get; set; }
       
    }
}
