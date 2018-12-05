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
	public partial class GetHourPage : ContentPage
	{
        private List<string> ids;
        private int id;

        public GetHourPage (int id)
		{
			InitializeComponent ();
            this.id = id;
            ids = new List<string>();

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");

            string user = "";

            try
            {
                user = wc.DownloadString("http://localhost:8000/users/" + id.ToString());
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Connection error");
            }

            dynamic objeto = JsonConvert.DeserializeObject(user);

            List<string> lista = new List<string>();
            string aux;
            dynamic auxObj;
            foreach (var i in objeto.availability)
            {
                wc.Headers.Add("Content-Type", "application/json");
                aux = wc.DownloadString("http://localhost:8000/users/availability/" + (string)i);
                auxObj = JsonConvert.DeserializeObject(aux);
                ids.Add((string)auxObj.id);
                lista.Add((string)auxObj.date + " : " + auxObj.inicial + " - " + auxObj.final);
            }

            opcoes.ItemsSource = lista;
        }

        private void GetHour(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            dynamic objeto = JsonConvert.DeserializeObject(user);

            string content = "{\"owner\":"+ this.id + ",\"interested\":"+ objeto.id +",\"date\":\""+(string)ids[(int)opcoes.SelectedIndex]+"\"}";
            string url = "http://localhost:8000/users/notification/";
            

            System.Diagnostics.Debug.WriteLine(content);
            System.Diagnostics.Debug.WriteLine(url);

            try
            {
                wc.UploadString(url, "Post", content);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Get hour error");
            }

            Navigation.PushAsync(new MainPage());
        }
    }
}