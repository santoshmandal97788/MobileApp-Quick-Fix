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
    public partial class CongratulationPage : ContentPage
    {
        public CongratulationPage()
        {
            InitializeComponent();
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var asdf = this.FindByName<Image>("img");
        //     asdf.Source = ImageSource.FromResource("QuickFix.Images.congrats.png");
        //}

        //private void home_Clicked(object sender, EventArgs e)
        //{
        //    App.Current.MainPage = new MainPage();
        //}
    }
}