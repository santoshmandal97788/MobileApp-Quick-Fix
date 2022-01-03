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
    public partial class ClientList : ContentPage
    {
        UserService _userService = new UserService();
        public ClientList()
        {
            InitializeComponent();
           ClientLists();
        }
        public class Clients
        {
            public int UId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public Nullable<decimal> Latitude { get; set; }
            public Nullable<decimal> Longitude { get; set; }
            public ImageSource Photo { get; set; }
        }
        async void ClientLists()
        {
            var list = await _userService.GetAllUsers();
            List<Clients> lst = new List<Clients>();
            foreach (var item in list)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Photo));
                lst.Add(new Clients() { UId = item.UId, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address,  Photo = imageSource });

            }
            collectionViewList.ItemsSource = lst;

        }
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (e.CurrentSelection.FirstOrDefault() as Clients).UId;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"clientdetails?id={id}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/domestic/dogs/dogdetails?name={dogName}");
        }
    }
}