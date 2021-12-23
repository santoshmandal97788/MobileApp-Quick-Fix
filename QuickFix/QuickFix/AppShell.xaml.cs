using QuickFix.ViewModels;
using QuickFix.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace QuickFix
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        //        public AppShell()
        //        {
        //            InitializeComponent();
        //            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        //            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        //        }

        //        private async void OnMenuItemClicked(object sender, EventArgs e)
        //        {
        //            await Shell.Current.GoToAsync("//LoginPage");
        //        }
        //    }
        //}


        //new TypeCode Added

        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
           // GetUserRole();
            //BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add("monkeydetails", typeof(Views.Map));
            //Routes.Add("beardetails", typeof(BearDetailPage));
            //Routes.Add("catdetails", typeof(CatDetailPage));
            //Routes.Add("dogdetails", typeof(DogDetailPage));
            //Routes.Add("elephantdetails", typeof(ElephantDetailPage));

            Routes.Add("electriciandetails", typeof(ElectricianDetailPage));
            Routes.Add("plumberdetails", typeof(PlumberDetailPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
        //bool isUser;
        //public bool GetUserRole()
        //{
        //    var getRole = SecureStorage.GetAsync("role");
        //   var role= getRole.ToString();
        //    if (role=="user")
        //    {
        //        isUser = true;
        //    }
        //    else
        //    {
        //        isUser = false;
        //    }
        //    return isUser;

        //}
        public class HiddenItem : ShellItem
        {

        }

        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    base.OnNavigating(args);

        //    // Cancel any back navigation
        //    //if (e.Source == ShellNavigationSource.Pop)
        //    //{
        //    //    e.Cancel();
        //    //}
        //}

        //protected override void OnNavigated(ShellNavigatedEventArgs args)
        //{
        //    base.OnNavigated(args);

        //    // Perform required logic
        //}
    }
}