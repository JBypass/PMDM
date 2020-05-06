using OpticaPMDM.Backend.Context;
using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using OpticaPMDM.Views;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        Color _activoColorRein;
        public Color ActivoColorRein
        {
            get => _activoColorRein;
            set
            {
                _activoColorRein = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActivoColorRein"));
            }
        }

        Color _activoColorEst;
        public Color ActivoColorEst
        {
            get => _activoColorEst;
            set
            {
                _activoColorEst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActivoColorEst"));
            }
        }

        string _textoBoton;
        public string TextoBoton
        {
            get => _textoBoton;
            set
            {
                _textoBoton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextoBoton"));
            }
        }

        public INavigation Navegacion { get; set; }

        public Command CargarStockCommand => new Command<object>(obj => { CargarStock(); });
        public Command FinalizarParCommand => new Command<object>(obj => { FinalizarPar(); });
        public Command EstrenarCommand => new Command<object>(obj => { Estrenar(); });

        private void CargarStock()
        {
            if (Navegacion != null)
            {
                Navegacion.PushModalAsync(new DetailsLentsPage(true));
            }
        }


        public InicioVM(IUsadasService usadasService, ILentillasService lentillasService)
        {
            _usadasService = usadasService;
            _lentillasService = lentillasService;
        }

        public async Task Initialize()
        {
            Usadas us = await _usadasService.GetLast(DateTime.Now);
            if (us == null)
            {
                Estrenadas = false;
                ActivoColorRein = Color.FromHex("#afe6e4");
                ActivoColorEst = Color.FromHex("#01aca6");
                Lentillas len = await _lentillasService.GetByStock();
                if (len == null)
                {
                    TextoBoton = "Sin stock";
                }
                else
                {
                    TextoBoton = "Estrenar";
                }
            }
            else
            {
                Estrenadas = true;
                ActivoColorRein = Color.FromHex("#01aca6");
                ActivoColorEst = Color.FromHex("#afe6e4");
                Usadas usadas = await _usadasService.GetLast(DateTime.Now);
                TimeSpan dias = usadas.FechaCaducidad - DateTime.Now;
                int Dias = dias.Days;
                TextoBoton = "Sus lentillas actuales caducan en " + Dias + " días";
            }
        }

        //Método Save
        private async void Estrenar()
        {
            try
            {
                Lentillas lentillas = await _lentillasService.GetByStock();
                //Si tiene lentillas en el stock
                if (lentillas != null && Estrenadas == false)
                {                    
                    Usadas nuevas = new Usadas();
                    nuevas.FechaEstreno = DateTime.Now;
                    nuevas.FechaCaducidad = nuevas.FechaEstreno.AddDays(lentillas.Duracion);
                    lentillas.Dias_Restantes = lentillas.Dias_Restantes - lentillas.Duracion;
                    lentillas.Num_Pares = lentillas.Num_Pares - 1;
                    await _usadasService.Add(nuevas);
                    await _lentillasService.Update(lentillas);
                    //Activar botón Finalizar par y esactivar botón estrenar
                    Estrenadas = true;
                    //CrossLocalNotifications.Current.Show("Atención", "Sus lentillas caducan hoy. Recuerde cambiarlas por un nuevo par", 101, DateTime.Now.AddSeconds(5));
                    CrossLocalNotifications.Current.Show("Atención", "Sus lentillas caducan hoy. Recuerde cambiarlas por un nuevo par.", 101, DateTime.Now.AddDays(lentillas.Duracion));
                    await App.Current.MainPage.DisplayAlert("Perfecto", "Sus lentillas caducarán el " + nuevas.FechaEstreno.AddDays(lentillas.Duracion).ToString("dd-MM-yyy") + ".", "OK");

                    if (lentillas.Num_Pares == 0)
                    {
                        lentillas.FechaFinal = DateTime.Now;
                    }
                    Initialize();
                }
                else
                {
                    if (Navegacion != null)
                    {
                        Navegacion.PushModalAsync(new DetailsLentsPage(true));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //Finalizar par para activar el botón
        private async void FinalizarPar()
        {
            if (Estrenadas == true)
            {
                OnAlertYesNoClicked();
            }
        }

        public async void OnAlertYesNoClicked()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Atención", "¿Seguro que desea dar por caducadas las lentillas actuales?", "Si", "No");
            if (answer == true)
            {
                Usadas usadas = await _usadasService.GetLast(DateTime.Now);
                usadas.FechaCaducidad = DateTime.Now.AddDays(-1);
                await _usadasService.Update(usadas);
                CrossLocalNotifications.Current.Cancel(101);
                Estrenadas = false;
                Initialize();
            }
        }

    }
}
