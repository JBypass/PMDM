using OpticaPMDM.Backend.Context;
using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using OpticaPMDM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpticaPMDM.ViewModels
{
    public class InicioVM : INotifyPropertyChanged
    {

        IUsadasService _usadasService;
        ILentillasService _lentillasService;

        public event PropertyChangedEventHandler PropertyChanged;

        bool _estrenadas;
        public bool Estrenadas
        {
            get => _estrenadas;
            set
            {
                _estrenadas = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Estrenadas"));
            }
        }

        public ICommand EstrenarCommand { get; private set; }
        public ICommand CargarStockCommand { get; private set; }
        public ICommand FinalizarParCommand { get; private set; }
        public INavigation Navigation { get; set; }

        public InicioVM(IUsadasService usadasService, ILentillasService lentillasService)
        {
            _usadasService = usadasService;
            _lentillasService = lentillasService;

            EstrenarCommand = new Command(Estrenar);
            CargarStockCommand = new Command(CargarStock);
            FinalizarParCommand = new Command(FinalizarPar);
        }

        public async Task Initialize()
        {
            if (await _usadasService.GetLast(DateTime.Now) == null)
            {
                Estrenadas = false;
            }
            else
            {
                Estrenadas = true;
            }
        }

        //Método Save
        private async void Estrenar()
        {
            try
            {
                //Si tiene lentillas en el stock
                if (_lentillasService.GetByStock() != null)
                {
                    Lentillas lentillas = await _lentillasService.GetByStock();
                    Usadas nuevas = new Usadas();
                    nuevas.FechaEstreno = DateTime.Now;
                    nuevas.FechaCaducidad = nuevas.FechaEstreno.AddDays(lentillas.Duracion);
                    lentillas.Dias_Restantes = lentillas.Dias_Restantes - lentillas.Duracion;
                    lentillas.Num_Pares = lentillas.Num_Pares - 1;
                    await _usadasService.Add(nuevas);
                    await _lentillasService.Update(lentillas);
                    //Activar botón Finalizar par y esactivar botón estrenar
                    Estrenadas = true;
                    if(lentillas.Num_Pares == 0)
                    {
                        lentillas.FechaFinal = DateTime.Now;
                    }
                }
                else
                {
                    await Navigation.PushModalAsync(new StockPage());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Cargar Stock te lleva a otra vista
        private async void CargarStock()
        {
            await Navigation.PushModalAsync(new StockPage());
        }

        //Finalizar par para activar el botón
        private async void FinalizarPar()
        {
            Usadas usadas = await _usadasService.GetLast(DateTime.Now);
            usadas.FechaCaducidad = DateTime.Now.AddDays(-1);
            Estrenadas = false;

        }

    }
}
