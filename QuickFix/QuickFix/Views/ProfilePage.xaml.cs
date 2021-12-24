using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            string photo = SecureStorage.GetAsync("photo").Result;
            byte[] bytes = Convert.FromBase64String(photo);
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            image.Source = imageSource;
            name.Text = SecureStorage.GetAsync("name").Result;
            address.Text = SecureStorage.GetAsync("address").Result; 
            phone.Text = SecureStorage.GetAsync("phone").Result;
            email.Text = SecureStorage.GetAsync("email").Result;
            
        }
        private async void EditProfile_Button_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}