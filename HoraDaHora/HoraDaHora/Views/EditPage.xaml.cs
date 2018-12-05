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
	public partial class EditPage : ContentPage
	{
        private dynamic objeto;

        public EditPage ()
		{
			InitializeComponent ();

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            objeto = JsonConvert.DeserializeObject(user);

            try
            {
                user = wc.DownloadString("http://localhost:8000/users/" + objeto.id);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Connection error");
            }

            objeto = JsonConvert.DeserializeObject(user);

            //username.Text = (string)objeto.username;
            phone.Text = (string)objeto.profile.phone;
        }

        private void UpdatePerfil(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string userEdit = "{\"phone\":\"" + phone.Text + "\",\"abilities\":[]}";
            //string userEdit = "{\"id\":" + objeto.profile.id + ",\"abilities\":" + objeto.profile.abilities + ",\"user\":" + objeto.profile.user + ",\"phone\":" + objeto.profile.phone + ",\"photo\":" + objeto.profile.photo + ",\"coins\":" + objeto.profile.coins + ",\"points\":" + objeto.profile.points + "}";
            string url = "http://localhost:8000/users/profile/" + objeto.profile.id + "/";
            System.Diagnostics.Debug.WriteLine(url);
            System.Diagnostics.Debug.WriteLine(userEdit);
            try
            {
                wc.UploadString(url,"Put", userEdit);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Update perfil error");
            }
            Navigation.PushAsync(new MainPage());
        }
    }
}