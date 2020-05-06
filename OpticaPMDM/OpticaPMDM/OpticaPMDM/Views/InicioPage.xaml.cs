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
    public partial class InicioPage : ContentPage
    {
        InicioVM _viewmodel;
        public InicioPage()
        {
            InitializeComponent();
            _viewmodel = new InicioVM(UsadasService.Instance, LentillasService.Instance) { Navegacion = Navigation }; ;
            BindingContext = _viewmodel;
            Appearing += InicioPage_Appearing;
        }

        private async void InicioPage_Appearing(object sender, EventArgs e)
        {
            await _viewmodel.Initialize();
        }
    }
}