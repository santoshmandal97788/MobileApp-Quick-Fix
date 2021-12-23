using QuickFix.Models;
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
    public partial class LIstAllTechnician : ContentPage
    {
        public IList<Technician> Technicians { get; private set; }

        public LIstAllTechnician()
        {
            InitializeComponent();
            Technicians = new List<Technician>();
            Technicians.Add(new Technician
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Howler Monkey",
                Location = "South America",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Alouatta_guariba.jpg/200px-Alouatta_guariba.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Japanese Macaque",
                Location = "Japan",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Macaca_fuscata_fuscata1.jpg/220px-Macaca_fuscata_fuscata1.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, Equatorial Guinea, and Congo",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Proboscis Monkey",
                Location = "Borneo",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Red-shanked Douc",
                Location = "Vietnam, Laos",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Portrait_of_a_Douc.jpg/159px-Portrait_of_a_Douc.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Gray-shanked Douc",
                Location = "Vietnam",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Cuc.Phuong.Primate.Rehab.center.jpg/320px-Cuc.Phuong.Primate.Rehab.center.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Golden Snub-nosed Monkey",
                Location = "China",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg/165px-Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Black Snub-nosed Monkey",
                Location = "China",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/5/59/RhinopitecusBieti.jpg/320px-RhinopitecusBieti.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Tonkin Snub-nosed Monkey",
                Location = "Vietnam",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg/320px-Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Thomas's Langur",
                Location = "Indonesia",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Thomas%27s_langur_Presbytis_thomasi.jpg/142px-Thomas%27s_langur_Presbytis_thomasi.jpg"
            });

            Technicians.Add(new Technician
            {
                Name = "Purple-faced Langur",
                Location = "Sri Lanka",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Semnopithèque_blanchâtre_mâle.JPG/192px-Semnopithèque_blanchâtre_mâle.JPG"
            });

            Technicians.Add(new Technician
            {
                Name = "Gelada",
                Location = "Ethiopia",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg"
            });

            BindingContext = this;
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Technician selectedItem = e.SelectedItem as Technician;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Technician tappedItem = e.Item as Technician;
        }
    }
}
