using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuickFix.Models
{
   public class Users
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public string Role { get; set; }
        public ImageSource CPhoto { get; set; }
    }
}
