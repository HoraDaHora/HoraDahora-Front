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
        List<int> ids;

        public GetHourPage (int id)
		{
			InitializeComponent ();

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");

            string user = "";

            try
            {
                user = wc.DownloadString("http://localhost:8000/users/" + id);
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
                aux = wc.DownloadString("http://localhost:8000/users/availability/" + i);
                auxObj = JsonConvert.DeserializeObject(aux);
                ids.Add((int)auxObj.id);
                lista.Add((string)auxObj.name);
            }

            opcoes.ItemsSource = lista;
        }
	}
}