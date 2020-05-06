using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpticaPMDM.ViewModels
{
    public class LentillasVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Lentillas _model;
        public ICommand SaveCommand { get; private set; }
        Action _itemSaved;
        ILentillasService _service;

        public LentillasVM(Lentillas model, Action itemSaved, ILentillasService service)
        {
            _service = service;
            _model = model;
            _itemSaved = itemSaved;
            if(_model != null)
            {
                Num_Pares = _model.Num_Pares.ToString();
                Duracion = _model.Duracion.ToString();
                Dias_Restantes = _model.Dias_Restantes;
                FechaRegistro = _model.FechaRegistro;
            }
            Marca = _model?.Marca;
            Modelo = _model?.Modelo;
            SaveCommand = new Command(Save);

        }

        string _marca;
        public string Marca
        {
            get => _marca;
            set
            {
                _marca = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Marca"));
            }
        }

        string _modelo;
        public string Modelo
        {
            get => _modelo;
            set
            {
                _modelo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Modelo"));
            }
        }

        string _num_Pares;
        public string Num_Pares
        {
            get => _num_Pares;
            set
            {
                _num_Pares = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Num_Pares"));
                Validate();
            }
        }

        int _dias_Restantes;
        public int Dias_Restantes
        {
            get => _dias_Restantes;
            set
            {
                _dias_Restantes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dias_Restantes"));
            }
        }

        string _duracion;
        public string Duracion
        {
            get => _duracion;
            set
            {
                _duracion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Duracion"));
                Validate();
            }
        }

        DateTime _fechaRegistro;
        public DateTime FechaRegistro
        {
            get => _fechaRegistro;
            set
            {
                _fechaRegistro = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FechaRegistro"));
            }
        }

        bool _isNumParValid;
        public bool IsNumParValid
        {
            get => _isNumParValid;
            private set
            {
                _isNumParValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsNumParValid"));
            }
        }

        bool _isDuracionValid;
        public bool IsDuracionValid
        {
            get => _isDuracionValid;
            private set
            {
                _isDuracionValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsDuracionValid"));
            }
        }

        private void Validate()
        {
            int outparse;
            IsDuracionValid = int.TryParse(Duracion, out outparse);
            IsNumParValid = int.TryParse(Num_Pares, out outparse);
        }

        private async void Save()
        {
            if (IsDuracionValid && IsNumParValid)
            {
                if (_model == null)
                    await _service.Add(new Lentillas()
                    {
                        Marca = Marca,
                        Modelo = Modelo,
                        Duracion = Int32.Parse(Duracion),
                        Num_Pares = Int32.Parse(Num_Pares),
                        FechaRegistro = DateTime.Now,
                        Dias_Restantes = (Int32.Parse(Duracion) * Int32.Parse(Num_Pares))

                    });
                else
                {
                    _model.Marca = Marca;
                    _model.Modelo = Modelo;
                    _model.Duracion = Int32.Parse(Duracion);
                    _model.Num_Pares = Int32.Parse(Num_Pares);
                    _model.FechaRegistro = FechaRegistro;
                    _model.Dias_Restantes = (Int32.Parse(Duracion) * Int32.Parse(Num_Pares));
                    await _service.Update(_model);
                }

                _itemSaved?.Invoke();
            }

        }

    }
}
