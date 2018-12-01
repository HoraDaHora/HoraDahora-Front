using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoraDaHora.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Login()
        {

        }

        private void AbrirRepo(object sender, EventArgs e)
        {
           Device.OpenUri(new Uri("https://github.com/HoraDaHora"));
        }
    }
}