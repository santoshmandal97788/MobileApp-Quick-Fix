using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFix.Models
{
    public class Technician
    {
        public int TechnicianId { get; set; }
        public string  FullName { get; set; }
        public string Gender { get; set; }
        public string  PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool  TechnicianType { get; set; }
        public byte[] Image { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

    }
}
