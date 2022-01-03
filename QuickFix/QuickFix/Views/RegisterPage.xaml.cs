using QuickFix.Services;
using QuickFix.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            DetectLocation();
        }
        UserService us = new UserService();
        byte[] imgData;
        private void EntryOutlined_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var stringg = "";
        }

        void OnAnimalRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Xamarin.Forms.RadioButton button = sender as Xamarin.Forms.RadioButton;
            //animalLabel.Text = $"You have chosen: {button.Value}";
        }
        private async void Button_clicked(object sender, EventArgs e)
        {
            try
            {
                RegisterViewModel rvm = new RegisterViewModel();
                rvm.FirstName = firstName.Text;
                rvm.LastName = lastName.Text;
                if (radioButtonMale.IsChecked)
                {
                    rvm.Gender = (string)radioButtonMale.Value;
                }
                else
                {
                    rvm.Gender = (string)radioButtonFemale.Value;
                }
                rvm.Email = email.Text;
                rvm.PhoneNumber = phoneNumber.Text;
                rvm.Address = address.Text;
                rvm.Password = password.Text;
                rvm.Photo = imgData;                
                rvm.ConfirmPassword = confirmPassword.Text;
                bool response = await us.RegisterUserAsync(rvm);
                if (response)
                {
                    await DisplayAlert("Alert", "Successful!!", "Ok");
                    await Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                     await DisplayAlert("Alert","Error Occured! Try Again Later", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async void DetectLocation()
        {
            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
            if (gpsStatus==false)
            {
                 await DisplayAlert("Access Location Services", "Please Turn On Location", "OK");

                    if (Device.RuntimePlatform == global::Xamarin.Forms.Device.Android)
                    {

                        //DependencyService.Get<ISettingsService>().OpenSettings();
                        global::Xamarin.Forms.DependencyService.Get<global::QuickFix.ILocSettings>().OpenSettings();
                    }

            }


        }
        async void Browse_Image_Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title="Please Select a Photo"
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

        

    }
}