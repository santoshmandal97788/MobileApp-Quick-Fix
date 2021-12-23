using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: Xamarin.Forms.Dependency(typeof(QuickFix.ILocSettings))]
namespace QuickFix.Views
{
    using Xamarin.Forms.PlatformConfiguration;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoLoaction : ContentPage
    {
        public GeoLoaction()
        {
            InitializeComponent();
        }

        CancellationTokenSource cts;

        public async Task GetCurrentLocation()
        {


            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    var geocodeAddress =
                        $"Lat: {location.Latitude}\n" +
                        $"Lon: {location.Longitude}\n" +
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    locat.Text = geocodeAddress;
                }

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                throw fnsEx;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                //throw fneEx;
               await DisplayAlert("Error", fneEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
                throw ex;
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

        //public async void GetLocation()
        //{
        //    Location Location = await GeoLoaction.GetLastKnownLocationAsync();
        //    if (Location == null)
        //    {
        //        Location = await GeoLoaction.GetLocationAsync(new GeolocationRequest
        //        {
        //            DesiredAccuracy = GeolocationAccuracy.Medium,
        //            Timeout = TimeSpan.FromSeconds(30)
        //        });

        //    }
        //}
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await GetCurrentLocation();
        }
        private async void TurnLocationOn_OnClicked(object sender, global::System.EventArgs e)
        {

            var myAction = await DisplayAlert("Location", "Please Turn On Location", "OK", "CANCEL");
            if (myAction)
            {
                if (Device.RuntimePlatform == global::Xamarin.Forms.Device.Android)
                {

                    //DependencyService.Get<ISettingsService>().OpenSettings();
                    global::Xamarin.Forms.DependencyService.Get<global::QuickFix.ILocSettings>().OpenSettings();



                }
                else
                {
                    await DisplayAlert("Device", "You are using some other shit", "YEP");
                }
            }
            else
            {
                await DisplayAlert("Alert", "User Denied Permission", "OK");
            }


            // 
        }
    }


}

   