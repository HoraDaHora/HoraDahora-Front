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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void Login(object sender, EventArgs e)
        {
            string content = "{\"email\": \"\",\"username\": \"" + login.Text + "\",\"password\": \"" + senha.Text + "\"}";
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");

            try
            {
                string resp = wc.UploadString("http://localhost:8000/rest-auth/login/", content);
                
                App.Current.Properties.Clear();
                App.Current.Properties.Add("user", content);
                App.Current.Properties.Add("key", resp);

                System.Diagnostics.Debug.WriteLine(resp.ToString());
                App.Current.MainPage = new Views.MainPage();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Invalid password");
            }
        }

        private void Cadastro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.CadastroPage());
        }
    }
}