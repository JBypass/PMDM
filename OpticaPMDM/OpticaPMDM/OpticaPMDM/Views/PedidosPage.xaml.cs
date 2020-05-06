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
    public partial class PedidosPage : ContentPage
    {
        ListaPedidosVM _viewmodel;
        public PedidosPage()
        {
            InitializeComponent();
            Title = "Pedidos";

            _viewmodel = new ListaPedidosVM(GoToDetails, PedidosService.Instance);
            BindingContext = _viewmodel;
            Appearing += PedidosPage_Appearing;
        }

        private void GoToDetails(Pedidos pedidos)
        {
            if (pedidos != null)
                Navigation.PushAsync(new DetailsPedidosPage(pedidos));
        }

        private async void PedidosPage_Appearing(object sender, EventArgs e)
        {
            await _viewmodel.Initialize();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailsPedidosPage());
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var pedidos = (sender as MenuItem)?.CommandParameter as Pedidos;
            if (pedidos != null)
                PedidosService.Instance.Remove(pedidos);
        }
    }
}