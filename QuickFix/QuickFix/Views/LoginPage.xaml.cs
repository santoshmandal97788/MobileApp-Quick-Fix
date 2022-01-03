using QuickFix.Services;
using QuickFix.ViewModels;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        AccountService _service = new AccountService();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            var properties = App.Current.Properties;
            if (properties.ContainsKey("username"))
            {
                var savedUsername = (string)properties["username"];
                var savedPassword = (string)properties["password"];
                EmailEntry.Text = savedUsername;
                PasswordEntry.Text = savedPassword;
            }
        }
        private async void Button_clicked(object sender, EventArgs args)
        {
           
           
      
            var response = await _service.LoginAsync(EmailEntry.Text, PasswordEntry.Text);
            if (string.IsNullOrEmpty(response))
            {
                await DisplayAlert("Invalid Username and Password","Invalid Username and Password","Ok");
            }
            else
            {
                AccountService _accService = new AccountService();
                var list = await _accService.GetUserByToken();
               
                await SecureStorage.SetAsync("id", list.ID.ToString());
                await SecureStorage.SetAsync("name", list.FullName);
                await SecureStorage.SetAsync("gender", list.Gender);
                await SecureStorage.SetAsync("email", list.Email);
                await SecureStorage.SetAsync("address", list.Address);
                await SecureStorage.SetAsync("phone", list.PhoneNumber);
                await SecureStorage.SetAsync("role", list.Role);
                var role=  SecureStorage.GetAsync("role").Result;
               // role.ToString();
                if (role=="technicians")
                {
                    await SecureStorage.SetAsync("TType", list.TType.ToString());
                }
                string base64 = Convert.ToBase64String(list.Photo);
                await SecureStorage.SetAsync("photo", base64);

                if (rebemberMe.IsChecked)
                {
                    var properties = App.Current.Properties;
                    if (!properties.ContainsKey("username") && !properties.ContainsKey("password"))
                    {
                        properties.Add("username", EmailEntry.Text);
                        properties.Add("password", PasswordEntry.Text);
                    }
                    else
                    {
                        properties["username"] = EmailEntry.Text;
                        properties["password"] = PasswordEntry.Text;
                    }
                }
                //  MessagingCenter.Send<LoginPage>(this, list.Role);
                Application.Current.MainPage = new AppShell();
            }
            //await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
            //Application.Current.MainPage = new AppShell();
        }

        //void signInClickFun()
        //{
        //    signInLabel.GestureRecognizers.Add(new TapGestureRecognizer()
        //    {
        //        Command =new Command(() =>
        //        {
        //            //DisplayAlert("task", "You clicked on me", "OK");
        //             Navigation.PushAsync(new CongratulationPage());
        //        })
        //    });
        //
        private async void SignUpFormCommandOpen(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new RegisterAsPage());
        }
    }
}