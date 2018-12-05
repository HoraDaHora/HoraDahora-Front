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
	public partial class AddAbilitiePage : ContentPage
	{
        WebClient wc;

        List<int> ids;

        public AddAbilitiePage ()
		{
			InitializeComponent ();

            ids = new List<int>();

            wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");

            string listOpcoes = wc.DownloadString(App.urlGlobal + "users/abilities/");
            dynamic objeto = JsonConvert.DeserializeObject(listOpcoes);

            List<string> lista = new List<string>();
            
            foreach(var i in objeto)
            {
                ids.Add((int)i.id);
                lista.Add((string)i.name);
            }

            opcoes.ItemsSource = lista;
        }

        private void InserirNova(object sender, EventArgs e)
        {
            if (ability.Text != "" && ability.Text != null)
            {
                string content = "{\"name\":\"" + ability.Text + "\"}";
                wc.Headers.Add("Content-Type", "application/json");
                wc.UploadString(App.urlGlobal + "users/abilities/", "Post", content);
            }
            else
            {
                DisplayAlert("Alerta", "Preencha com alguma habilidade", "ok");
            }

        }

        private void Inserir(object sender, EventArgs e)
        {
            if(opcoes.SelectedIndex != -1)
            {
                InsertAbility();
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Alerta", "Nenhuma habilidade foi selecionada.", "ok");
            }
        }

        private void InsertAbility()
        {
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            dynamic objeto = JsonConvert.DeserializeObject(user);
            string url = App.urlGlobal + "users/profile/" + objeto.profile.id + "/";

            string content = "{\"abilities\":[";
            foreach (var i in objeto.profile.abilities)
            {
                content += "{\"id\":\"" + i.id + "\"},";
            }
            content += "{\"id\":\"" + ids[opcoes.SelectedIndex] + "\"}" + "]}";
            System.Diagnostics.Debug.WriteLine(url);
            System.Diagnostics.Debug.WriteLine(content);
            wc.UploadString(url, "Put", (string)content);
        }
    }
}