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
    public partial class TechnicianRegisterPage : ContentPage
    {
        public TechnicianRegisterPage()
        {
            InitializeComponent();
            DetectLocation();
        }
        TechnicianService us = new TechnicianService();
        byte[] imgData;
       

        void OnAnimalRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Xamarin.Forms.RadioButton button = sender as Xamarin.Forms.RadioButton;
            //animalLabel.Text = $"You have chosen: {button.Value}";
        }
        private async void Button_clicked(object sender, EventArgs e)
        {
            try
            {
                TechnicainRegisterViewModel rvm = new TechnicainRegisterViewModel();
                rvm.FirstName = firstName.Text;
                rvm.LastName = lastName.Text;
                if (radioButtonMale.IsChecked)
                {
                    rvm.Gender = true;
                }
                else
                {
                    rvm.Gender = false;
                }
                if (radioButtonElectrician.IsChecked)
                {
                    rvm.TType = 1;
                }
                else
                {
                    rvm.TType = 0;
                }
                rvm.Email = email.Text;
                rvm.PhoneNumber = phoneNumber.Text;
                rvm.Address = address.Text;
                rvm.Password = password.Text;
                rvm.Photo = imgData;
                rvm.Password = confirmPassword.Text;
                bool response = await us.RegisterTechnicianAsync(rvm);
                if (response)
                {
                    await DisplayAlert("Alert", "Successful!!", "Ok");
                    await Navigation.PushModalAsync(new LoginPage());
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
        public async void DetectLocation()
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

    }
}