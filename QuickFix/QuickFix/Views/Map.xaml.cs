using Newtonsoft.Json;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        UserService _service = new UserService();
        TechnicianService _techService = new TechnicianService();
        public Map()
        {
            InitializeComponent();
            //DetectLocation();
            //GetAllTechnician();
            GetAllELectrician();
        }

        //public async void DetectLocation()
        //{
        //    bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
        //    if (gpsStatus == false)
        //    {
        //        await DisplayAlert("Access Location Services", "Please Turn On Location", "OK");

        //        if (Device.RuntimePlatform == global::Xamarin.Forms.Device.Android)
        //        {

        //            //DependencyService.Get<ISettingsService>().OpenSettings();
        //            global::Xamarin.Forms.DependencyService.Get<global::QuickFix.ILocSettings>().OpenSettings();
        //        }

        //    }


        //}
      
        //public async void GetAllTechnician()
        //{           
        //    var response = await _service.GetAllTechnician();
        //    if (response != null)
        //    {
        //        foreach (var item in response)
        //        {
        //            CustomPin pin = new CustomPin
        //            {
        //                Type = PinType.Place,
        //                Position = new Position(item.Latitude, item.Longitude),
        //                Label = item.Name + item.Gender+ item.DisatanceAway,
        //                Address = item.Address ,
        //                Name = item.Name,
        //                Phone=item.Phone,
        //                Gender=item.Gender,
        //                DisatanceAway=item.DisatanceAway,
        //                Technician = "Electrician",
        //                URL = "http://xamarin.com/about/"
        //            };
        //            customMap.CustomPins = new List<CustomPin> { pin };
        //            customMap.Pins.Add(pin);
        //            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(item.Latitude, item.Longitude), Distance.FromMiles(1.0)));

        //        }
        //    }
          
          
        //}
        //public async void GetAllELectricianNearBy()
        //{
        //    var response = await _service.GetAllElectricianNearBy();
        //    if (response != null)
        //    {
        //        foreach (var item in response)
        //        {
        //            CustomPin pin = new CustomPin
        //            {
        //                Type = PinType.Place,
        //                Position = new Position(item.Latitude, item.Longitude),
        //                Label = item.Name + item.Gender + item.DisatanceAway,
        //                Address = item.Address,
        //                Id=item.Id,
        //                Name = item.Name,
        //                Phone = item.Phone,
        //                Gender = item.Gender,
        //                DisatanceAway = item.DisatanceAway,
        //                Technician = "Electrician",
        //                URL = "http://xamarin.com/about/"
        //            };
        //            customMap.CustomPins = new List<CustomPin> { pin };
        //            customMap.Pins.Add(pin);
        //            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(item.Latitude, item.Longitude), Distance.FromMiles(1.0)));

        //        }
        //    }


        //}
        public class TechnicianDetails
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Double Latitude { get; set; }
            public Double Longitude { get; set; }
            public string Address { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public float DisatanceAway { get; set; }
            public string Technician { get; set; }
        }
        public async void GetAllELectrician()
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

            CancellationTokenSource cts;

            var response = await _techService.GetAllElectrician();
            if (response != null)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

                //location code ends-----
                Location pin1 = new Location(location.Latitude, location.Longitude);
                List<TechnicianDetails> lst = new List<TechnicianDetails>();
                foreach (var item in response)
                {
                    Location pin2 = new Location((double)item.Latitude, (double)item.Longitude);
                    double distance = Location.CalculateDistance(pin1, pin2, DistanceUnits.Kilometers);
                   // double distanceBetween = pin1.GetDistanceTo(pin2);
                    if (distance <= 5)
                    {

                        lst.Add(new TechnicianDetails() { Id = item.TID, Name = item.FullName, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender, Phone = item.PhoneNumber, Address = item.Address, DisatanceAway = (float)distance, Technician = "Electrician" });

                    }
                }
                if (lst.Count==0)
                {
                    bool result= await DisplayAlert("Not Found", "No Electrician Found In Your Locality Go For Further??", "OK","Cancel");
                    if (result)
                    {
                        foreach (var item in response)
                        {
                            Location pin2 = new Location((double)item.Latitude, (double)item.Longitude);
                            double distance = Location.CalculateDistance(pin1, pin2, DistanceUnits.Kilometers);
                            // double distanceBetween = pin1.GetDistanceTo(pin2);
                           

                                lst.Add(new TechnicianDetails() { Id = item.TID, Name = item.FullName, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender, Phone = item.PhoneNumber, Address = item.Address, DisatanceAway = (float)distance, Technician = "Electrician" });

                            
                        }
                    }
                }

                foreach (var item in lst)
                {
                    CustomPin pin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.Latitude, item.Longitude),
                        Label = item.Name + item.Gender + item.DisatanceAway,
                        Address = item.Address,
                        Id = item.Id,
                        Name = item.Name,
                        Phone = item.Phone,
                        Gender = item.Gender,
                        DisatanceAway = item.DisatanceAway,
                        Technician = item.Technician,
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