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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlumberList : ContentPage
    {
        TechnicianService _techService = new TechnicianService();
        public PlumberList()
        {
            InitializeComponent();
            PlumberLists();
        }
        public class Technicians
        {
            public int TID { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public Nullable<bool> Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public Nullable<int> TType { get; set; }
            public Nullable<decimal> Latitude { get; set; }
            public Nullable<decimal> Longitude { get; set; }
            public int WorkCount { get; set; }
            public Nullable<bool> IsApproved { get; set; }
            // public string Password { get; set; }
            public ImageSource Photo { get; set; }
        }
        async void PlumberLists()
        {
            var list = await _techService.GetAllPlumber();
            List<Technicians> lst = new List<Technicians>();
            foreach (var item in list)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Photo));
                lst.Add(new Technicians() { TID = item.TID, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, Photo = imageSource });

            }
            collectionViewList.ItemsSource = lst;

        }
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (e.CurrentSelection.FirstOrDefault() as Technicians).TID;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"plumberdetails?id={id}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/domestic/dogs/dogdetails?name={dogName}");
        }
    }
}