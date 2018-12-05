using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoraDaHora.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : ContentPage
    {
        public PerfilPage()
        {
            InitializeComponent();

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            dynamic objeto = JsonConvert.DeserializeObject(user);

            try
            {
                user = wc.DownloadString("http://localhost:8000/users/" + objeto.id);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Connection error");
            }

            objeto = JsonConvert.DeserializeObject(user);

            username.Text = (string)objeto.username;
            phone.Text = (string)objeto.profile.phone;
            coins.Text = (string)objeto.profile.coins;

            try
            {
                foreach(var i in objeto.profile.abilities)
                {
                    InsertAbilitie(i.name);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Abilities exception");
            }
        }

        private void LogOut(object sender, EventArgs e)
        {
            App.Current.Properties.Clear();
            App.Current.MainPage = new NavigationPage(new Views.LoginPage());
        }

        private void InsertAbilitie(string name)
        {
            abilities.Children.Add(new Button { Text = name, BackgroundColor = GetRandomColor() });
        }

        private void InsertHour(string name)
        {
            hour.Children.Add(new Button { Text = name, BackgroundColor = GetRandomColor() });
        }

        public Color GetRandomColor()
        {
            Random rand = new Random();
            int max = byte.MaxValue + 1; // 256
            int r = rand.Next(max);
            int g = rand.Next(max);
            int b = rand.Next(max);
            return Color.FromRgb(r, g, b);
        }

        private void Edit(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage());
        }
    }
}