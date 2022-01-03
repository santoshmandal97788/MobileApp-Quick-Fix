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

            //var client = new HttpClient();
            //client.Timeout = TimeSpan.FromSeconds(200);
            //client.BaseAddress = new Uri("http://10.10.11.59:45455/");

            //string jsonData = JsonConvert.SerializeObject(tb);
            // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


            //HttpResponseMessage response = await client.PostAsync("api/User/Register", content);
            //var content1 = await response.Content.ReadAsStringAsync();
            //// this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            ////var result = await response.Content.ReadAsStringAsync();

            //if (response.IsSuccessStatusCode)
            //{
            //    Response = true;
            //}


            //return Response;

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(200);
            client.BaseAddress = new Uri("http://10.10.11.59:45455/");
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent baContent = new ByteArrayContent(tb.Photo);
            StringContent FullName = new StringContent(tb.FullName);
            StringContent Gender = new StringContent(tb.Gender);
            StringContent Email = new StringContent(tb.Email);
            StringContent PhoneNumber = new StringContent(tb.PhoneNumber);
            StringContent Address = new StringContent(tb.Address);
            StringContent Password = new StringContent(tb.Password);
            StringContent Latitude = new StringContent(tb.Latitude.ToString());
            StringContent Longitude = new StringContent(tb.Longitude.ToString());
            content.Add(baContent, "File", "filename.ext");
            content.Add(FullName, "FullName");
            content.Add(Gender, "Gender");
            content.Add(Email, "Email");
            content.Add(PhoneNumber, "PhoneNumber");
            content.Add(Address, "Address");
            content.Add(Password, "Password");
            content.Add(Latitude, "Latitude");
            content.Add(Longitude, "Longitude");


            //upload MultipartFormDataContent content async and store response in response var
            var response = await client.PostAsync("api/User/Register", content);

            //read response result as a string async into json var
            var responsestr = response.Content.ReadAsStringAsync().Result;
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
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

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
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

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
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

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
        //new mrthod to get all users
        public async Task<List<ClientLists>> GetAllUsers()
        {

            try
            {
                List<ClientLists> lstusers = new List<ClientLists>();
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/User/GetAllUser");
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject<List<ClientLists>>(responseJSON);

                return msg;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClientLists> GetUserById(int id)
        {

            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/User/GetClientbyId?id=" + id);
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject<ClientLists>(responseJSON);

                return msg;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
