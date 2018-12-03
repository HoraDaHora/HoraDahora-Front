using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoraDaHora.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        private void About(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Views.AboutPage());
        }

        private void Perfil(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Views.PerfilPage());
        }

        private void Ranking(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Views.RankingPage());
        }
    }
}