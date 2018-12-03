﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoraDaHora.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HoraDaHora
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            if (App.Current.Properties.ContainsKey("user"))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
