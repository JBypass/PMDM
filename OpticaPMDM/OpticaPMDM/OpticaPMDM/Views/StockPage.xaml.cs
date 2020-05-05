using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using OpticaPMDM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpticaPMDM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockPage : ContentPage
    {
        StockVM _viewmodel;
        public StockPage()
        {
            InitializeComponent();
            Title = "Stock";

            _viewmodel = new StockVM(GoToDetails, LentillasService.Instance);
            BindingContext = _viewmodel;
            Appearing += StockPage_Appearing;
        }

        private void GoToDetails(Lentillas lentillas)
        {
            if (lentillas != null)
                Navigation.PushAsync(new DetailsLentsPage(lentillas));
        }

        private async void StockPage_Appearing(object sender, EventArgs e)
        {
            await _viewmodel.Initialize();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailsLentsPage());
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var lentillas = (sender as MenuItem)?.CommandParameter as Lentillas;
            if (lentillas != null)
                LentillasService.Instance.Remove(lentillas);
        }
    }
}