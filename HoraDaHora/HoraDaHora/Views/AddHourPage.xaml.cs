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
	public partial class AddHourPage : ContentPage
	{
		public AddHourPage ()
		{
			InitializeComponent ();

            DateTime minimumDate = DateTime.Now;

            date.MinimumDate = minimumDate;
            
		}

        private void Insert(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            string user = App.Current.Properties["user"].ToString();
            dynamic objeto = JsonConvert.DeserializeObject(user);

            string content = "{\"user\": " + objeto.id + ",\"date\":\"" + String.Format("{0:MM/dd/yyyy}", date.Date) + "\",\"inicial\":\"" + inicial.Time + "\" ,\"final\":\"" + final.Time + "\"}";

            try
            {
                System.Diagnostics.Debug.WriteLine(content);
                wc.UploadString(App.urlGlobal + "users/availability/", "Post", content);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Add hour error");
            }

            Navigation.PushAsync(new MainPage());
        }
    }
}