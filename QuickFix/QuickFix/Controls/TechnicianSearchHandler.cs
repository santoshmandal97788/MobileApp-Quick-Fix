using QuickFix.Models;
using QuickFix.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickFix.Controls
{
    public class TechnicianSearchHandler : SearchHandler
    {
        TechnicianService _techService = new TechnicianService();
        public TechnicianSearchHandler()
        {
            ElectricianLists();
            PlumberLists();
        }
        public IList<TechnicianLists> Technicians { get; set; }

        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Technicians
                    .Where(t => t.FullName.ToLower().Contains(newValue.ToLower()))
                    .ToList<TechnicianLists>();
            }
        }
        public async void ElectricianLists()
        {
            Technicians = new List<TechnicianLists>();
            var list = await _techService.GetAllElectrician();

            foreach (var item in list)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Photo));
                Technicians.Add(new TechnicianLists() { TID = item.TID, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, CPhoto = imageSource });

            }
        }
            public async void PlumberLists()
            {
                
                Technicians = new List<TechnicianLists>();
                var list = await _techService.GetAllPlumber();

                foreach (var item in list)
                {
                    ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Photo));
                    Technicians.Add(new TechnicianLists() { TID = item.TID, FullName = item.FullName, Latitude = item.Latitude, Longitude = item.Longitude, Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, CPhoto = imageSource });

                }

            }
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"{GetNavigationTarget()}?id={((TechnicianLists)item).TID}");
        }

        string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}
