using Newtonsoft.Json;
using QuickFix.Models;
using QuickFix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QuickFix.Services
{
    public class UserService
    {
        public async Task<bool> RegisterUserAsync(RegisterViewModel rvm)
        {
            CancellationTokenSource cts;
            bool Response = false;


            RegisterBindingModel tb = new RegisterBindingModel();
            tb.FullName = rvm.FirstName + " " + rvm.LastName;
            tb.Gender = rvm.Gender;
            tb.Email = rvm.Email;
            tb.PhoneNumber = rvm.PhoneNumber;
            tb.Address = rvm.Address;
            tb.Password = rvm.ConfirmPassword;

            tb.Photo = rvm.Photo;
            //code for location access and getting value
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            //location code ends-----

            tb.Latitude = location.Latitude;
            tb.Longitude = location.Longitude;
            // tb.Photo = rvm.Photo;
            tb.Country = placemark.CountryName;
            tb.Province = placemark.AdminArea;
            tb.PostalCode = placemark.PostalCode;
            tb.District = placemark.Locality;
            tb.Locality = placemark.Locality;

            //var json = JsonConvert.SerializeObject(tb);
            ////HttpContent httpContent = new StreamContent();
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            ////httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //var response = client.PostAsync("https://localhost:44357/api/User/Register", content);
            //var mystring = response.GetAwaiter().GetResult();

            ////string url = "https://localhost:44357/api/User/Register";
            ////string jsonData = JsonConvert.SerializeObject(tb);
            ////StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            ////HttpResponseMessage response = await client.PostAsync(url, content);
            //var mystring = response.GetAwaiter().GetResult();
            //string result = await response.Content.ReadAsStringAsync();
            //Response responseData = JsonConvert.DeserializeObject<Response>(result); 



            //new code
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(200);
            client.BaseAddress = new Uri("http://192.168.0.109:45455/");

            string jsonData = JsonConvert.SerializeObject(tb);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/User/Register", content);
            var content1 = await response.Content.ReadAsStringAsync();
            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            //var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Response = true;
            }


            return Response;
        }
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


        public async Task<List<TechnicianDetails>> GetAllTechnician()
        {
          
            try
            {
                List<TechnicianDetails> lstTech = new List<TechnicianDetails>();
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://192.168.0.109:45455/");

                var response = await client.GetStringAsync("api/User/ShowAllTechnician");
                var techDetails = JsonConvert.DeserializeObject<List<TechnicianDetails>>(response);
                foreach (var item in techDetails)
                {
                    lstTech.Add(new TechnicianDetails() {Id=item.Id, Name = item.Name, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender, Phone = item.Phone, Address = item.Address, DisatanceAway = item.DisatanceAway });
                }
                return lstTech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TechnicianDetails>> GetAllElectricianNearBy()
        {

            try
            {
                List<TechnicianDetails> lstElec = new List<TechnicianDetails>();
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://192.168.0.109:45455/");

                var response = await client.GetStringAsync("api/User/ShowElectricianNearBy");
                var electricianDetails = JsonConvert.DeserializeObject<List<TechnicianDetails>>(response);
                foreach (var item in electricianDetails)
                {
                    lstElec.Add(new TechnicianDetails() {Id=item.Id, Name = item.Name, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender, Phone = item.Phone, Address = item.Address, DisatanceAway = item.DisatanceAway });
                }
                return lstElec;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TechnicianDetails>> GetAllPlumberNearBy()
        {

            try
            {
                List<TechnicianDetails> lstPlum = new List<TechnicianDetails>();
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://192.168.0.109:45455/");

                var response = await client.GetStringAsync("api/User/ShowPlumberNearBy");
                var electricianDetails = JsonConvert.DeserializeObject<List<TechnicianDetails>>(response);
                foreach (var item in electricianDetails)
                {
                    lstPlum.Add(new TechnicianDetails() { Id=item.Id, Name = item.Name, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender, Phone = item.Phone, Address = item.Address, DisatanceAway = item.DisatanceAway });
                }
                return lstPlum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
