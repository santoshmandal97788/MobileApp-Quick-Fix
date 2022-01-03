using QuickFix.Services;
using QuickFix.ViewModels;
using QuickFix.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
            string role = SecureStorage.GetAsync("role").Result;
            if (role == "technicians")
            {
                bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
                if (gpsStatus == false)
                {
                    DisplayAlert("Access Location Services", "Please Turn On Location", "OK");

                    if (Device.RuntimePlatform == global::Xamarin.Forms.Device.Android)
                    {

                        //DependencyService.Get<ISettingsService>().OpenSettings();
                        global::Xamarin.Forms.DependencyService.Get<global::QuickFix.ILocSettings>().OpenSettings();
                    }

                }
                Device.StartTimer(new TimeSpan(0, 1, 0), () =>
                {
                    // do something every 60 seconds
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // interact with UI elements
                        DetectLocationChangeAndUpdate();

                    });
                    return true; // runs again, or false to stop
                });
            }
           
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
            Routes.Add("clientdetails", typeof(ClientDetailPage));

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



        public async void DetectLocationChangeAndUpdate()
        {
            AccountService _accService = new AccountService();
            CancellationTokenSource cts;
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var list = await _accService.GetUserByToken();
            double lat = list.Latitude;
            double lon = list.Longitude;
            if (location.Latitude!=lat || location.Longitude != lon)
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");
                MultipartFormDataContent content = new MultipartFormDataContent();
                string id = SecureStorage.GetAsync("id").Result;
                StringContent Id = new StringContent(id);
                StringContent Latitude = new StringContent(location.Latitude.ToString());
                StringContent Longitude = new StringContent(location.Longitude.ToString());
                content.Add(Id, "Id");
                content.Add(Latitude, "Latitude");
                content.Add(Longitude, "Longitude");
                var response = await client.PostAsync("api/Technician/UpdateLoc", content);

            }

        }

        private  void logout_Clicked(object sender, EventArgs args)
        {
            SecureStorage.RemoveAll();
            Application.Current.MainPage = new LoginPage();
        }

    }
}