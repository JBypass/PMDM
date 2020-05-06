using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpticaPMDM.ViewModels
{
    public class StockVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Action<Backend.Entities.Lentillas> _goToDetails;
        ILentillasService _service;
        public ICommand RemoveLentillasCommand;

        public INavigation Navigation { get; set; }

        private ObservableCollection<LentillasStock> _elements = new ObservableCollection<LentillasStock>();
        public ObservableCollection<LentillasStock> Elements { get => _elements; }

        LentillasStock _selectedItem;
        public LentillasStock SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value != null)
                {
                    var task = Task.Run(async () =>
                    {
                        return await LentillasService.Instance.GetByID(value.ID);
                    });
                    task.Wait();
                    _goToDetails?.Invoke(task.Result);
                }

            }
        }


        public StockVM(Action<Backend.Entities.Lentillas> goToDetails, ILentillasService service)
        {
            _service = service;
            _goToDetails = goToDetails;
            MessagingCenter.Subscribe<LentillasService, Backend.Entities.Lentillas>(this,
                "Add", AddLentillas);
            MessagingCenter.Subscribe<LentillasService, Backend.Entities.Lentillas>(this,
                "Update", UpdateLentillas);
        }

        private void UpdateLentillas(LentillasService sender, Backend.Entities.Lentillas item)
        {
            var element = this._elements.FirstOrDefault(el => el.ID == item.IdLentillas);
            if (element != null)
            {
                element.Marca = item.Marca;
                element.Modelo = item.Modelo;
                element.ParRestantes = item.Num_Pares;
                element.DiasRestantes = item.Dias_Restantes;
            }
        }

        private void AddLentillas(LentillasService sender, Backend.Entities.Lentillas item)
        {
            AddLentillas(item);
        }

        private void AddLentillas(Backend.Entities.Lentillas item)
        {
            _elements.Add(new LentillasStock()
            {
                ID = item.IdLentillas,
                Marca = item.Marca,
                Modelo = item.Modelo,
                ParRestantes = item.Num_Pares,
                DiasRestantes = item.Dias_Restantes,
                RemoveLentillasCommand = new Command<LentillasStock>(RemoveLentillas)
            });
        }

        private async void RemoveLentillas(LentillasStock lentillas)
        {
            var itemToRemove = await _service.GetByID(lentillas.ID);
            await _service.Remove(itemToRemove);
            _elements.Remove(lentillas);
        }

        bool _initialized;
        public async Task Initialize()
        {
            if (!_initialized)
            {
                var serviceItems = await _service.GetAll();
                foreach (var item in serviceItems)
                    AddLentillas(item);
                _initialized = true;
            }
        }

        public class LentillasStock : INotifyPropertyChanged
        {
            public Command<LentillasStock> RemoveLentillasCommand { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            public long ID { get; set; }

            int _parRestantes;
            public int ParRestantes
            {
                get => _parRestantes;
                set
                {
                    _parRestantes = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParRestantes)));
                }
            }

            int _diasRestantes;
            public int DiasRestantes
            {
                get => _diasRestantes;
                set
                {
                    _diasRestantes = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DiasRestantes)));
                }
            }

            string _marca;
            public string Marca
            {
                get => _marca;
                set
                {
                    _marca = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Marca)));
                }
            }

            string _modelo;
            public string Modelo
            {
                get => _modelo;
                set
                {
                    _modelo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Modelo)));
                }
            }
        }
    }
}
