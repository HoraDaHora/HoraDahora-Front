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
	public partial class NotificationPage : ContentPage
	{
		public NotificationPage ()
		{
			InitializeComponent ();

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string notification = "";
            dynamic objeto;

            try
            {
                notification = wc.DownloadString("http://localhost:8000/users/notification/");
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Connection error");
            }
            objeto = JsonConvert.DeserializeObject(notification);
            
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            dynamic objetoUser = JsonConvert.DeserializeObject(user);

            string date;
            string owner;
            string interested;
            string status;
            dynamic auxObj;
            foreach (var i in objeto)
            {
                if(i.interested == objetoUser.id)
                {
                    date = wc.DownloadString("http://localhost:8000/users/availability/" + i.date);
                    auxObj = JsonConvert.DeserializeObject(date);
                    date = (string)(auxObj.date + " : " + auxObj.inicial + " - " + auxObj.final);
                    owner = wc.DownloadString("http://localhost:8000/users/" + i.owner);
                    auxObj = JsonConvert.DeserializeObject(owner);
                    owner = (string)auxObj.username;
                    //interested = wc.DownloadString("http://localhost:8000/users/" + i.interested);
                    //auxObj = JsonConvert.DeserializeObject(interested);
                    //interested = (string)auxObj.username;
                    status = (string)i.status;

                    InsertItem(owner + " - " + date);
                }
            }
        }

        private void InsertItem(string name)
        {
            notifications.Children.Add(new Button { Text = name, BackgroundColor = GetRandomColor() });
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
    }
}