using QuickFix.Models;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickFix.Controls
{
    public partial class FlyoutHeader : ContentView
    {
        public FlyoutHeader()
        {
            InitializeComponent();
            GetUerByToken();
        }

        async void GetUerByToken()
        {
            try
            {
                AccountService _accService = new AccountService();
                var list = await _accService.GetUserByToken();
               // await SecureStorage.SetAsync("role", list.Role);
           //     MessagingCenter.Send<FlyoutHeader>(this, list.Role
                  
           //);
                List<Users> lst = new List<Users>();
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(list.Photo));
                lst.Add(new Users() { ID = list.ID, FullName = list.FullName, Email = list.Email, CPhoto = imageSource });

                Users tList = lst.Where(a => a.ID == list.ID).FirstOrDefault();
                //BindingContext = tList;
                userImg.Source = imageSource;
                userName.Text = tList.FullName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
   
}
