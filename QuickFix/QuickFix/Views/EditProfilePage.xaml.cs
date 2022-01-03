using QuickFix.Services;
using QuickFix.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        AccountService _accService = new AccountService();
        public EditProfilePage()
        {
            InitializeComponent();
            string photo = SecureStorage.GetAsync("photo").Result;
            byte[] bytes = Convert.FromBase64String(photo);
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            resultImage.Source = imageSource;
            phoneNumber.Text = SecureStorage.GetAsync("phone").Result;

            address.Text = SecureStorage.GetAsync("address").Result;

            email.Text = SecureStorage.GetAsync("email").Result;
            string fName= SecureStorage.GetAsync("name").Result;
            string[] firstNameSplit = fName.Split(' ');
            firstName.Text = firstNameSplit[0];
            lastName.Text = firstNameSplit[1];
            string gender= SecureStorage.GetAsync("gender").Result;
            if (gender== "Male")
            {
                radioButtonMale.IsChecked = true;
            }
            else
            {
                radioButtonFemale.IsChecked = true;

            }
            string role = SecureStorage.GetAsync("role").Result;
            int tType = Convert.ToInt32(SecureStorage.GetAsync("TType").Result);
            if (role=="technicians")
            {
                if (tType==1)
                {
                    radioButtonElectrician.IsChecked = true;
                }
                else
                {
                    radioButtonPlumber.IsChecked = true;
                }
            }

            
        }
        byte[] imgData;
        void OnAnimalRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Xamarin.Forms.RadioButton button = sender as Xamarin.Forms.RadioButton;
            //animalLabel.Text = $"You have chosen: {button.Value}";
        }

        async void Browse_Image_Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please Select a Photo"
            });



            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);

            }
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                imgData = memoryStream.ToArray();
            }

        }

        async void Take_Image_Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                imgData = memoryStream.ToArray();
            }
        }
        void OnToggled(object sender, ToggledEventArgs e)
        {
            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
            if (gpsStatus == false)
            {
                // DisplayAlert("Access Location Services", "Please Turn On Location", "OK");

                if (Device.RuntimePlatform == global::Xamarin.Forms.Device.Android)
                {

                    //DependencyService.Get<ISettingsService>().OpenSettings();
                    global::Xamarin.Forms.DependencyService.Get<global::QuickFix.ILocSettings>().OpenSettings();
                }

            }

        }

        private async void Button_clicked(object sender, EventArgs e)
        {
            try
            {
                string role = SecureStorage.GetAsync("role").Result;
                UpdateViewModel rvm = new UpdateViewModel();
                string id = SecureStorage.GetAsync("id").Result;
                rvm.Id = Convert.ToInt32(id);
                rvm.FirstName = firstName.Text;
                rvm.LastName = lastName.Text;
                if (role=="user")
                {
                    if (radioButtonMale.IsChecked)
                    {
                        rvm.Gender = "Male";
                    }
                    else
                    {
                        rvm.Gender = "Female";
                    }

                }
                else
                {

                    if (radioButtonMale.IsChecked)
                    {
                        rvm.Gender = "Male";
                    }
                    else
                    {
                        rvm.Gender = "Female";
                    }
                    if (radioButtonElectrician.IsChecked)
                    {
                        rvm.TType = 1;
                    }
                    else
                    {
                        rvm.TType = 0;
                    }
                }
             
                rvm.Email = email.Text;
                rvm.PhoneNumber = phoneNumber.Text;
                rvm.Address = address.Text;
                if (rvm.Photo==null)
                {
                    string photo = SecureStorage.GetAsync("photo").Result;
                    byte[] bytes = Convert.FromBase64String(photo);
                    rvm.Photo = bytes;
                }
                else
                {
                    rvm.Photo = imgData;
                }
               
                bool response = await _accService.UpdateProfileAsync(rvm);
                if (response)
                {
                    SecureStorage.Remove("name");
                    SecureStorage.Remove("email");
                    SecureStorage.Remove("gender");
                    SecureStorage.Remove("address");
                    SecureStorage.Remove("phone");
                    SecureStorage.Remove("photo");
                    if (role == "technicians")
                    {
                      SecureStorage.Remove("TType");
                        await SecureStorage.SetAsync("TType", rvm.TType.ToString());
                    }

                    await SecureStorage.SetAsync("name", rvm.FirstName + rvm.LastName);
                  
                    await SecureStorage.SetAsync("gender", rvm.Gender);
                    
                    
                    await SecureStorage.SetAsync("email", rvm.Email);
                    await SecureStorage.SetAsync("address", rvm.Address);
                    await SecureStorage.SetAsync("phone", rvm.PhoneNumber);
                    string base64 = Convert.ToBase64String(rvm.Photo);
                    await SecureStorage.SetAsync("photo", base64);
                    await DisplayAlert("Alert", "Successful!! Profile Updated", "Ok");

                    await Navigation.PushModalAsync(new ProfilePage());
                }
                else
                {
                    await DisplayAlert("Alert", "Error Occured! Try Again Later", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}