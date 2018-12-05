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
	public partial class UserDetailPage : ContentPage
	{
        int id;

		public UserDetailPage (int id)
		{
			InitializeComponent ();

            this.id = id;
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            dynamic objeto = JsonConvert.DeserializeObject(user);

            if (id == (int)objeto.id)
            {
                BtnHour.IsEnabled = false;
                BtnHour.IsVisible = false;
            }

            user = "";
            
            try
            {
                user = wc.DownloadString("http://localhost:8000/users/" + id);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Connection error");
            }

            objeto = JsonConvert.DeserializeObject(user);

            username.Text = (string)objeto.username;
            phone.Text = (string)objeto.profile.phone;
            coins.Text = (string)objeto.profile.coins;

            string aux;
            dynamic auxObj;

            try
            {
                foreach(var i in objeto.profile.abilities)
                {
                    InsertAbilitie((string)i.name);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Abilities exception");
            }

            try
            {
                foreach (var i in objeto.availability)
                {
                    aux = wc.DownloadString("http://localhost:8000/users/availability/" + i);
                    auxObj = JsonConvert.DeserializeObject(aux);
                    InsertHour((string)auxObj.date + " : " + auxObj.inicial + " - " + auxObj.final);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Hours exception");
            }
        }

        private void InsertAbilitie(string name)
        {
            Button btn = new Button() { Text = name, BackgroundColor = GetRandomColor() };
            abilities.Children.Add(btn);
        }

        private void InsertHour(string name)
        {
            Button btn = new Button() { Text = name, BackgroundColor = GetRandomColor() };
            hour.Children.Add(btn);
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

        private void GetHour(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetHourPage(id));
        }
    }
}