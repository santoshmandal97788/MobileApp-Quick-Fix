using QuickFix.Models;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [QueryProperty(nameof(Id), "id")]
   // [QueryProperty(nameof(Name), "name")]
    public partial class ElectricianDetailPage : ContentPage
    {
        public new int Id
        {
            set
            {
                LoadElectrician (value);
            }
        }
        //public string Name
        //{
        //    set
        //    {
        //        LoadElectrician(value);
        //    }
        //}
        public ElectricianDetailPage()
        {
            InitializeComponent();
            //LoadElectrician("Santosh Mandal");
        }
        //public ElectricianDetailPage(string name)
        //{
        //    InitializeComponent();
        //    LoadElectrician(name);
        //}
        async void LoadElectrician(int id)
        {
            try
            {
                TechnicianService _techService = new TechnicianService();
                var list = await _techService.GetElectricianById(id);
                List<TechnicianLists> lst = new List<TechnicianLists>();
              
                    ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(list.Photo));
                    lst.Add(new TechnicianLists() { TID = list.TID, FullName = list.FullName, Latitude = list.Latitude, Longitude = list.Longitude, Gender = list.Gender, Email = list.Email, PhoneNumber = list.PhoneNumber, Address = list.Address, IsApproved = list.IsApproved, CPhoto = imageSource });

                
                TechnicianLists tList = lst.Where(a => a.TID == id).FirstOrDefault();
                BindingContext = tList;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        //async void LoadElectrician(string name)
        //{
        //    try
        //    {
        //        TechnicianService _techService = new TechnicianService();
        //        var list = await _techService.GetAllElectrician();
        //        List<TechnicianLists> lst = new List<TechnicianLists>();
        //        foreach (var item in list)
        //        {
        //            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Photo));
        //            lst.Add(new TechnicianLists() { TID = item.TID, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, CPhoto = imageSource });

        //        }


        //        TechnicianLists tList = lst.Where(a => a.FullName == name).FirstOrDefault();
        //        BindingContext = tList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}