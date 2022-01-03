using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickFix.Models;
using QuickFix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QuickFix.Services
{
    public class AccountService
    {
        //Main URL
       // const string URL = "https://10.0.2.2:45456/";
        const string URL = "http://10.10.11.59:45455/";

        //For Token URL
        const string tokenURL = "http://10.10.11.59:45455/token";

        public async Task<string> LoginAsync(string UserName, string Password)
        {
            //chnage url url is wrong
            //string URL = "https://localhost:45456/token";
           // string URL = "http://10.10.11.59:45455/";
            var accessToken = string.Empty;
            await Task.Run(async () =>
            {
                try
                {
                    var keyValues = new Dictionary<string, string>();
                    keyValues.Add("UserName", UserName);
                    keyValues.Add("Password", Password);
                    keyValues.Add("grant_type", "password");
                    //var keyValues = new List<KeyValuePair<string, string>>
                    //{
                    //    new KeyValuePair<string, string>("UserName", UserName),
                    //    new KeyValuePair<string, string>("Password", Password),
                    //    new KeyValuePair<string, string>("grant_type", "password")
                    //};
                    var request = new HttpRequestMessage(HttpMethod.Post, tokenURL);
                    request.Content = new FormUrlEncodedContent(keyValues);
                    var client = new HttpClient();
                    var response = client.SendAsync(request).Result;
                    using(HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync();
                        JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json.Result);
                        //var accessTokenExpire = jwtDynamic.Value<DateTime>("expires_in");
                        accessToken= jwtDynamic.Value<string>("access_token");

                    }
                     SecureStorage.RemoveAll();
                    await SecureStorage.SetAsync("token", accessToken);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return accessToken;
        }


        //Get User Details By token 
        public async Task<Users> GetUserByToken()
        {

            try
            { 
                var token = await SecureStorage.GetAsync("token");
                var authHeader = new AuthenticationHeaderValue("bearer",token);
             
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = authHeader;

                client.Timeout = TimeSpan.FromSeconds(200);
                client.BaseAddress = new Uri("http://10.10.11.59:45455/");

                var response = await client.GetAsync("api/User/GetUserDetails");
                response.EnsureSuccessStatusCode();
                var responseJSON = await response.Content.ReadAsStringAsync();

                var msg = JsonConvert.DeserializeObject<Users>(responseJSON);

                return msg;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateProfileAsync(UpdateViewModel rvm)
        {
            CancellationTokenSource cts;
            bool Response = false;


            UpdateBindingModel tb = new UpdateBindingModel();
            tb.FullName = rvm.FirstName + " " + rvm.LastName;
            tb.Id = rvm.Id;
            tb.Gender = rvm.Gender;
            tb.TType = rvm.TType;
            tb.Email = rvm.Email;
            tb.PhoneNumber = rvm.PhoneNumber;
            tb.Address = rvm.Address;
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

            ////new code
            //var client = new HttpClient();
            //client.Timeout = TimeSpan.FromSeconds(200);
            //client.BaseAddress = new Uri("http://10.10.11.59:45455/");

            //string jsonData = JsonConvert.SerializeObject(tb);

            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //HttpResponseMessage response;
            //if (SecureStorage.GetAsync("role").Result=="user")
            //{
            //     response = await client.PutAsync("api/User/Update", content);

            //}
            //else
            //{
            //  response = await client.PutAsync("api/Technician/Update", content);

            //}
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
            StringContent Id = new StringContent(tb.Id.ToString());
            StringContent FullName = new StringContent(tb.FullName);
            StringContent Gender = new StringContent(tb.Gender);
            StringContent Email = new StringContent(tb.Email);
            StringContent PhoneNumber = new StringContent(tb.PhoneNumber);
            StringContent Address = new StringContent(tb.Address);
            StringContent Latitude = new StringContent(tb.Latitude.ToString());
            StringContent Longitude = new StringContent(tb.Longitude.ToString());
            if (SecureStorage.GetAsync("role").Result == "technician")
            {
                StringContent TType = new StringContent(tb.TType.ToString());
                content.Add(TType, "TType");
            }
            content.Add(baContent, "File", "filename.ext");
            content.Add(Id, "Id");
            content.Add(FullName, "FullName");
            content.Add(Gender, "Gender");
            content.Add(Email, "Email");
            content.Add(PhoneNumber, "PhoneNumber");
            content.Add(Address, "Address");
            content.Add(Latitude, "Latitude");
            content.Add(Longitude, "Longitude");

            HttpResponseMessage response;
            if (SecureStorage.GetAsync("role").Result == "user")
            {
                response = await client.PutAsync("api/User/Update", content);

            }
            else
            {
                response = await client.PutAsync("api/Technician/Update", content);

            }
            //read response result as a string async into json var
            var responsestr = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                Response = true;
            }

            return Response;
        }

    }
}
