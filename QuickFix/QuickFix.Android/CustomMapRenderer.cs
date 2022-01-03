using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using System;
using System.Collections.Generic;
using Android.Gms.Maps.Model;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using QuickFix;
using QuickFix.Droid;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace QuickFix.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<CustomPin> customPins = new List<CustomPin>();

        public CustomMapRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        List<Pin> lst = new List<Pin>();


        protected override MarkerOptions CreateMarker(Pin pin)
        {
            lst.Add(pin);
           // customPins.Add(new CustomPin {Address=pin.Address, Name=pin.});
            
        // customPinsAdd(new CustomPin { Name=pin.})
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.TechnicianPin));
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }
            int id = customPin.Id;
            if (id>0)
            {
                //var url = Android.Net.Uri.Parse(customPin.URL);
                //var intent = new Intent(Intent.ActionView, url);
                //intent.AddFlags(ActivityFlags.NewTask);
                //Android.App.Application.Context.StartActivity(intent);
                // (App.Current as App).NavigationPage.Navigation.PushAsync(new ContentPage());
                if (customPin.Technician=="Electrician")
                {
                    Shell.Current.GoToAsync($"electriciandetails?id={customPin.Id}");

                }
                else
                {
                    Shell.Current.GoToAsync($"plumberdetails?id={customPin.Id}");

                }

            }
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;              
                 var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (customPin.Name.Equals("Xamarin"))
                {
                    view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                }

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);
                var infoGender = view.FindViewById<TextView>(Resource.Id.InfoWindowGender);
                var infoTechnician  = view.FindViewById<TextView>(Resource.Id.InfoWindowTechnician);
               // var infoImg = view.FindViewById<TextView>(Resource.Id.InfoWindowImage);
                //var infoImg = view. FindViewById<ImageView>(Resource.Id.InfoWindowImage);

                //if (infoImg != null)
                //{
                //    infoImg.SetImageResource(Resource.Drawable.monkey);
                //}
                if (infoTitle != null)
                {
                    infoTitle.Text =  "Name: " + customPin.Name;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = "Phone: " + customPin.Phone + " " + "Gender: " + customPin.Gender;
                }
                if (infoTechnician != null)
                {
                    infoTechnician.Text = "Technician: " + "Electrician";
                }
                if (infoGender != null)
                {
                    infoGender.Text = "Distance Away: " + customPin.DisatanceAway +"km";
                }
             
                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in lst)
            {
                if (pin.Position == position)
                {
                    return (CustomPin)pin;
                }
            }
            return null;
        }
    }
}

