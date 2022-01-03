using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuickFix.Models
{
    public class ClientLists
    {
        public int UId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Nullable<int> TType { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public byte[] Photo { get; set; }

        public ImageSource CPhoto { get; set; }
    }
}
