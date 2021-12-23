using Newtonsoft.Json;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        UserService _service = new UserService();

        public Map()
        {
            InitializeComponent();
            DetectLocation();
            //GetAllTechnician();
            GetAllELectricianNearBy();
        }

        public async void DetectLocation()
        {
            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
            if (gpsStatus == false)
            {
                await DisplayAlert("Access Location Services", "Please Turn On Location", "OK");

                if (Device.RuntimePlatform == global::Xamarin.Forms.Device.Android)
                {

                    //DependencyService.Get<ISettingsService>().OpenSettings();
                    global::Xamarin.Forms.DependencyService.Get<global::QuickFix.ILocSettings>().OpenSettings();
                }

            }


        }
      
        public async void GetAllTechnician()
        {           
            var response = await _service.GetAllTechnician();
            if (response != null)
            {
                foreach (var item in response)
                {
                    CustomPin pin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.Latitude, item.Longitude),
                        Label = item.Name + item.Gender+ item.DisatanceAway,
                        Address = item.Address ,
                        Name = item.Name,
                        Phone=item.Phone,
                        Gender=item.Gender,
                        DisatanceAway=item.DisatanceAway,
                        Technician = "Electrician",
                        URL = "http://xamarin.com/about/"
                    };
                    customMap.CustomPins = new List<CustomPin> { pin };
                    customMap.Pins.Add(pin);
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(item.Latitude, item.Longitude), Distance.FromMiles(1.0)));

                }
            }
          
          
        }
        public async void GetAllELectricianNearBy()
        {
            var response = await _service.GetAllElectricianNearBy();
            if (response != null)
            {
                foreach (var item in response)
                {
                    CustomPin pin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.Latitude, item.Longitude),
                        Label = item.Name + item.Gender + item.DisatanceAway,
                        Address = item.Address,
                        Id=item.Id,
                        Name = item.Name,
                        Phone = item.Phone,
                        Gender = item.Gender,
                        DisatanceAway = item.DisatanceAway,
                        Technician = "Electrician",
                        URL = "http://xamarin.com/about/"
                    };
                    customMap.CustomPins = new List<CustomPin> { pin };
                    customMap.Pins.Add(pin);
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(item.Latitude, item.Longitude), Distance.FromMiles(1.0)));

                }
            }


        }

    }
}