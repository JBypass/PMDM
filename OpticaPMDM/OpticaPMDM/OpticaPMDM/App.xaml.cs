using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OpticaPMDM.Views;
using System.IO;

namespace OpticaPMDM
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            Backend.Context.Database.DBPATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "opticaPMDM.db3");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
