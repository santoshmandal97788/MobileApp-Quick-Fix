using QuickFix.Services;
using QuickFix.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickFix.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private bool isUser;
        private bool istechnician;
        public bool IsUser { get => isUser; set => SetProperty(ref isUser, value); }
        public bool IsTechnician { get => istechnician; set => SetProperty(ref istechnician, value); }
        public AppViewModel()
        {
          
           string role =  SecureStorage.GetAsync("role").Result;
            if (role=="user")
            {
                IsUser = true;
                IsTechnician = false;
            }
            else
            {
                IsUser = false;
                IsTechnician = true;
            }

        }
        //private bool isUser = false;
        //public string role;
        //string rr;

        //public bool IsUser { get => isUser; set => SetProperty(ref isUser, value); }
        //public AppViewModel()
        //{
        //    MessagingCenter.Subscribe<LoginPageViewModel>(this, message: "admin", (sender) =>
        //    {
        //        IsAdmin = true;
        //    });

        //    MessagingCenter.Subscribe<LoginPageViewModel>(this, message: "user", (sender) =>
        //    {
        //        IsAdmin = false;
        //    });
        //    //Get();
        //    //rr = role;

        //    //if (rr == "user")
        //    //{
        //    //    IsUser = true;
        //    //}
        //    //else
        //    //{
        //    //    IsUser = false;
        //    //}

        //}
        //public async void Get()
        //{
        //    AccountService _accService = new AccountService();
        //    var list = await _accService.GetUserByToken();
        //    role = list.Role;
        //    // return role;
        //    //  role = await SecureStorage.GetAsync("role");


        //}
    }
}
