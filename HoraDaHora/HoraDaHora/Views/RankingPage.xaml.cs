using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoraDaHora.Models;
using System.Net;
using Newtonsoft.Json;

namespace HoraDaHora.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RankingPage : ContentPage
	{
		public RankingPage ()
		{
			InitializeComponent ();

            List<User> users = new List<User>();

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            try
            {
                string resp = wc.DownloadString(App.urlGlobal + "users/");
                dynamic objeto = JsonConvert.DeserializeObject(resp);

                foreach(dynamic i in objeto)
                {
                    users.Add(new User { Id = (int)i.id, Username = (string)i.username, Coins = (int)i.profile.coins });
                }
                ItemsListView.ItemsSource = bubblesort(users);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Conection error");
            }


		}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as User;
            if (item == null)
                return;

            await Navigation.PushAsync(new UserDetailPage(item.Id));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        
        public List<User> bubblesort(List<User> a)
        {
            User temp;
            // foreach(int i in a)
            //System.Diagnostics.Debug.WriteLine(a.Count());
            for (int i = 1; i <= a.Count; i++)
            {
                for (int j = 0; j < a.Count - i; j++)
                {
                    //System.Diagnostics.Debug.WriteLine("jksand jsnd ");

                    if (a[j].Coins < a[j + 1].Coins)
                    {
                        //System.Diagnostics.Debug.WriteLine(a);
                        temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                    }
                }
            }
            int aux = 1;
            foreach(var i in a)
            {
                System.Diagnostics.Debug.WriteLine(i.Coins);
                i.Username = aux.ToString() + " - " + i.Username;
                aux++;
            }
            return a;
        }

        private void LogOut(object sender, EventArgs e)
        {
            App.Current.Properties.Clear();
            App.Current.MainPage = new NavigationPage(new Views.LoginPage());
        }
    }
}