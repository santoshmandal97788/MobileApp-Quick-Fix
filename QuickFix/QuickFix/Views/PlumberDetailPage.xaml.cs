using QuickFix.Models;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [QueryProperty(nameof(Id), "id")]
    public partial class PlumberDetailPage : ContentPage
    {
        public new int Id
        {
            set
            {
                LoadPlumber(value);
            }
        }
        public PlumberDetailPage()
        {
            InitializeComponent();
        }
        async void LoadPlumber(int id)
        {
            try
            {
                TechnicianService _techService = new TechnicianService();
                var list = await _techService.GetPlumberById(id);
                List<TechnicianLists> lst = new List<TechnicianLists>();

                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(list.Photo));
                lst.Add(new TechnicianLists() { TID = list.TID, FullName = list.FullName, Latitude = list.Latitude, Longitude = list.Longitude, Gender = list.Gender, Email = list.Email, PhoneNumber = list.PhoneNumber, Address = list.Address, IsApproved = list.IsApproved, CPhoto = imageSource });


                TechnicianLists tList = lst.Where(a => a.TID == id).FirstOrDefault();
                BindingContext = tList;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load Plumber Data.");
            }
        }
        private void MakeCall_Button_Clicked(object sender, EventArgs args)
        {
            try
            {
                PhoneDialer.Open(phone.Text);
            }
            catch (Exception ex)
            {

                DisplayAlert("Unable to make call", "Please enter a number", "Ok");
            }
        }
    }
}