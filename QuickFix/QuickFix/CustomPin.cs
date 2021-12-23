using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace QuickFix
{
    public class CustomPin : Pin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public float DisatanceAway { get; set; }
        public string URL { get; set; }
        public string Technician { get; set; }
        public byte[] Image { get; set; }
    }
}
