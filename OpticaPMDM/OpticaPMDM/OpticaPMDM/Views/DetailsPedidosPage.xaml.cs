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
    public partial class DetailsPedidosPage : ContentPage
    {
        bool Flag { get; set; }
        public DetailsPedidosPage(Pedidos pedidos)
        {
            InitializeComponent();
            BindingContext = new PedidosVM(pedidos, ItemSaved, PedidosService.Instance);
            Flag = false;
        }
        public DetailsPedidosPage()
        {
            InitializeComponent();
            BindingContext = new PedidosVM(null, ItemSaved, PedidosService.Instance);
            Flag = false;
        }

        public DetailsPedidosPage(bool Flag)
        {
            InitializeComponent();
            BindingContext = new PedidosVM(null, ItemSaved, PedidosService.Instance);
            this.Flag = Flag;
        }

        public async void ItemSaved()
        {
            if (Flag == true)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}