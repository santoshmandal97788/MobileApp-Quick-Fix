using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickFix.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAsPage : ContentPage
    {
        string radioButtonValue;
        public RegisterAsPage()
        {
            InitializeComponent();
           
        }
        
        void OnAnimalRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
           // var radiobtn = sender as RadioButton;
            radioButtonValue = (string)button.Value;
          //  radioButtonValue = button.Value;
            //animalLabel.Text = $"You have chosen: {button.Value}";

        }
        private async void OpenUserSelectedRegisterPage(object sender, EventArgs args)
        {
            if (radioButtonValue==null || radioButtonValue==string.Empty)
            {
                await DisplayAlert("Select One", "Please Select One Selection", "OK");
            }
            else if (radioButtonValue == "Technician")
            {
                await Navigation.PushModalAsync(new TechnicianRegisterPage());
            }
            else
            {
                await Navigation.PushModalAsync(new RegisterPage());
            }
        }
           
    }
}