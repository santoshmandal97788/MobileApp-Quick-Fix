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
    public class TechnicianService
    {
        public async Task<bool> RegisterTechnicianAsync(TechnicainRegisterViewModel rvm)
        {
            CancellationTokenSource cts;
            bool Response = false;


            TechnicianRegisterBindingModel tb = new TechnicianRegisterBindingModel();
            tb.FullName = rvm.FirstName + " " + rvm.LastName;
            tb.Gender = rvm.Gender;
            tb.TType = rvm.TType;
            tb.Email = rvm.Email;
            tb.PhoneNumber = rvm.PhoneNumber;
            tb.Address = rvm.Address;
            tb.Password = rvm.ConfirmPassword;
           
            
            //code for location access and getting value
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            //location code ends-----

            tb.Latitude = location.Latitude;
            tb.Longitude = location.Longitude;
            tb.Photo = rvm.Photo;
            tb.WorkCount = 0;

            //new code
            //var client = new HttpClient();
            //client.Timeout = TimeSpan.FromSeconds(200);
            //client.BaseAddress = new Uri("http://10.10.11.59:45455/");

            //string jsonData = JsonConvert.SerializeObject(tb);

            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync("api/Technician/Register", content);
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
            StringContent TType = new StringContent(tb.TType.ToString());
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
            content.Add(TType, "TType");
            content.Add(Latitude, "Latitude");
            content.Add(Longitude, "Longitude");


            //upload MultipartFormDataContent content async and store response in response var
            var response = await client.PostAsync("api/Technician/Register", content);

            //read response result as a string async into json var
            var responsestr = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                Response = true;
            }

            return Response;

        }

        public async Task<List<TechnicianLists>> GetAllElectrician()
        {

            try
            {
                List<TechnicianLists> lstTech = new List<TechnicianLists>();
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/Technician/GetAllElectrician");
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject< List<TechnicianLists>>(responseJSON);

                return msg;
                // var techDetails = JsonConvert.DeserializeObject<List<Technician>>(response);
                //foreach (var item in techDetails)
                //{
                //    lstTech.Add(new Technician() { Name = item.Name, Latitude = (decimal)item.Latitude, Longitude = (decimal)item.Longitude, Gender = item.Gender, PhoneNumber = item.PhoneNumber, Address = item.Address, Image=item.Image});
                //}
                //return lstTech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TechnicianLists> GetElectricianById(int id)
        {

            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/Technician/GetPlumberbyId?id=" + id);
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject<TechnicianLists>(responseJSON);

                return msg;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<TechnicianLists>> GetAllPlumber()
        {

            try
            {
                List<TechnicianLists> lstTech = new List<TechnicianLists>();
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/Technician/GetAllPlumber");
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject<List<TechnicianLists>>(responseJSON);

                return msg;
                // var techDetails = JsonConvert.DeserializeObject<List<Technician>>(response);
                //foreach (var item in techDetails)
                //{
                //    lstTech.Add(new Technician() { Name = item.Name, Latitude = (decimal)item.Latitude, Longitude = (decimal)item.Longitude, Gender = item.Gender, PhoneNumber = item.PhoneNumber, Address = item.Address, Image=item.Image});
                //}
                //return lstTech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TechnicianLists> GetPlumberById(int id)
        {

            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/Technician/GetPlumberbyId?id=" + id);
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject<TechnicianLists>(responseJSON);

                return msg;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
