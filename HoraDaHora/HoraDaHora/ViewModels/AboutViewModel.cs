using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace HoraDaHora.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/HoraDaHora")));
        }

        public ICommand OpenWebCommand { get; }
    }
}