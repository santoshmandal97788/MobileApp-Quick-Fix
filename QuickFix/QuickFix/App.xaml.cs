using QuickFix.Services;
using QuickFix.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: ExportFont("GilroyBold.otf", Alias = "BoldFont")]
[assembly: ExportFont("GilroyLight.otf", Alias = "LightFont")]
[assembly: ExportFont("MediumMuseo.otf", Alias = "MediumFont")]
[assembly: ExportFont("LightMuseoSans.otf", Alias = "Light")]
[assembly: ExportFont("material.ttf", Alias = "Material")]
namespace QuickFix
{
    public partial class App : Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;
        public App()
        {
            InitializeComponent();

            // DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            // MainPage = new GeoLoaction();
            //if (!string.IsNullOrEmpty(SecureStorage.GetAsync("name").Result))
            //{
            //    Application.Current.MainPage = new AppShell();
            //}
            //else
            //{
                MainPage = new NavigationPage(new LoginPage());

            //}
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
