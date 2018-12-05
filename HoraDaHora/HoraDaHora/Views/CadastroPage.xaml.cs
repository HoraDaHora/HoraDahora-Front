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
	public partial class CadastroPage : ContentPage
	{
		public CadastroPage ()
		{
			InitializeComponent ();
		}

        private void Cadastrar(object sender, EventArgs e)
        {
            // TODO - cadastrar na api
            string content = "{\"username\": \"" + login.Text + "\",\"email\": \"\",\"password\": \"" + senha.Text + "\"}";
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");

            try
            {
                string resp = wc.UploadString(App.urlGlobal + "users/", content);
                System.Diagnostics.Debug.WriteLine(resp.ToString());
            }
            catch
            {

            }
            Navigation.PopAsync();
        }
    }
}