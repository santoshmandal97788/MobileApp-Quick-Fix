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
    public partial class ClientDetailPage : ContentPage
    {
        public new int Id
        {
            set
            {
                LoadClient(value);
            }
        }
        public ClientDetailPage()
        {
            InitializeComponent();
        }
        async void LoadClient(int id)
        {
            try
            {
                UserService _userService = new UserService();
                var list = await _userService.GetUserById(id);
                List<ClientLists> lst = new List<ClientLists>();

                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(list.Photo));
                lst.Add(new ClientLists() { UId = list.UId, FullName = list.FullName, Latitude = list.Latitude, Longitude = list.Longitude, Gender = list.Gender, Email = list.Email, PhoneNumber = list.PhoneNumber, Address = list.Address,  CPhoto = imageSource });


                ClientLists tList = lst.Where(a => a.UId == id).FirstOrDefault();
                BindingContext = tList;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load Client Data.");
            }
        }
        private  void MakeCall_Button_Clicked(object sender, EventArgs args)
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