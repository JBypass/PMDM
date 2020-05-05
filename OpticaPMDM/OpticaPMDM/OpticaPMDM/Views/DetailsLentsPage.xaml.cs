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
    public partial class DetailsLentsPage : ContentPage
    {
        public DetailsLentsPage(Lentillas lentillas)
        {
            InitializeComponent();
            BindingContext = new LentillasVM(lentillas, ItemSaved, LentillasService.Instance);
        }
        public DetailsLentsPage()
        {
            InitializeComponent();
            BindingContext = new LentillasVM(null, ItemSaved, LentillasService.Instance);
        }

        public async void ItemSaved()
        {
            await Navigation.PopAsync();
        }
    }
}