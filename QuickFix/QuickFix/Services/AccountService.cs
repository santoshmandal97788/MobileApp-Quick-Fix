using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickFix.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QuickFix.Services
{
    public class AccountService
    {
        //Main URL
        const string URL = "http://192.168.0.113:45456/";
        //For Token URL
        const string tokenURL = "http://192.168.0.113:45456/token";

        public async Task<string> LoginAsync(string UserName, string Password)
        {
            //chnage url url is wrong
            //string URL = "https://localhost:45456/token";
           // string URL = "https://192.168.0.106:45456/token";
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
                client.BaseAddress = new Uri("http://192.168.0.113:45456/");

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
    }
}
