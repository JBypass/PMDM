using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpticaPMDM.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        ContentPage _inicioPage;
        NavigationPage _stockPage;
        NavigationPage _pedidosPage;
        ContentPage _configuracionPage;

        private ContentPage InicioPage
        {
            get
            {
                if(_inicioPage == null){
                    _inicioPage = new InicioPage();
                }
                return _inicioPage;
            }
        }

        private NavigationPage StockPage
        {
            get
            {
                if(_stockPage == null)
                {
                    _stockPage = new NavigationPage(new StockPage());
                }
                return _stockPage;
            }
        }

        private NavigationPage PedidosPage
        {
            get
            {
                if(_pedidosPage == null)
                {
                    _pedidosPage = new NavigationPage(new PedidosPage());
                }
                return _pedidosPage;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            InicioPage.IconImageSource = ImageSource.FromFile("home_variant_outline");
            Children.Add(InicioPage);

            StockPage.IconImageSource = ImageSource.FromFile("eye_settings_outline");
            Children.Add(StockPage);

            PedidosPage.IconImageSource = ImageSource.FromFile("cart");
            Children.Add(PedidosPage);

        }
    }
}