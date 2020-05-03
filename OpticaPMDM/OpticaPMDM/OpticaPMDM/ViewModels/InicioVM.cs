using OpticaPMDM.Backend.Context;
using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpticaPMDM.ViewModels
{
    class InicioVM
    {

        IUsadasService _usadasService;
        ILentillasService _lentillasService;
        public ICommand SaveCommand { get; private set; }


        public InicioVM(IUsadasService usadasService, ILentillasService lentillasService)
        {
            _usadasService = usadasService;
            _lentillasService = lentillasService;

            SaveCommand = new Command(Save);
        }

        //Método Save
        private async void Save()
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
                }
                else
                {
                    //Si no tiene lentillas habrá que darle la opción para que añada stock

                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
