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
        }
        private async void Button_clicked(object sender, EventArgs args)
        {
            //await Navigation.PushAsync(new ChangePasswordPage());
            //await Navigation.PushModalAsync(new ChangePasswordPage());
           // await DisplayAlert("Invalid Username and Password", "Invalid Username and Password", "Ok");
            var response = await _service.LoginAsync(EmailEntry.Text, PasswordEntry.Text);
            if (string.IsNullOrEmpty(response))
            {
                await DisplayAlert("Invalid Username and Password","Invalid Username and Password","Ok");
            }
            else
            {
                AccountService _accService = new AccountService();
                var list = await _accService.GetUserByToken();
                await SecureStorage.SetAsync("name", list.ID.ToString());
                await SecureStorage.SetAsync("name", list.FullName);
                await SecureStorage.SetAsync("name", list.Gender);
                await SecureStorage.SetAsync("email", list.Email);
                await SecureStorage.SetAsync("address", list.Address);
                await SecureStorage.SetAsync("address", list.PhoneNumber);
                await SecureStorage.SetAsync("role", list.Role);
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(list.Photo));
               // await SecureStorage.SetAsync("role", imageSource);
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