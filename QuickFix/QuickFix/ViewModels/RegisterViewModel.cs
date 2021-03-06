using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFix.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
      
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public byte[] Photo { get; set; }
       

    }
}
