using QuickFix.Models;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickFix.Data
{
    public static class ElectricianData
    {
        public static IList<TechnicianLists> Technicians { get; private set; }
        static TechnicianService _techService;
       static List<TechnicianLists> lst;



        static ElectricianData()
        {
            _techService = new TechnicianService();
           
            Technicians = new List<TechnicianLists>();
            // var data = ElectricianLists();
            foreach (var item in lst)
            {
                Technicians.Add(new TechnicianLists() { TID = item.TID, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, CPhoto = item.CPhoto });

            }
        }

        static async void ElectricianLists()
        {
            Technicians = new List<TechnicianLists>();
            var list = await _techService.GetAllElectrician();
     
            foreach (var item in list)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Photo));
                Technicians.Add(new TechnicianLists() { TID = item.TID, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, CPhoto = imageSource });

            }
           // return Technicians;

        }
    }
}