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
    public partial class PerfilPage : ContentPage
    {
        public PerfilPage()
        {
            InitializeComponent();

            WebClient wc = new WebClient();
            //wc.Headers.Add("Content-Type", "application/json");
            //string key = App.Current.Properties["key"].ToString();
            //System.Diagnostics.Debug.WriteLine("Authorization: Token " + key);
            //wc.Headers.Add("Authorization: Token " + key);
            

            //try
            //{
            //    System.Diagnostics.Debug.WriteLine(wc.DownloadString("http://localhost:8000/rest-auth/user/"));
            //}
            //catch
            //{

            //}

        }
    }
}