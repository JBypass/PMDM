using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpticaPMDM.ViewModels
{
    public class PedidosVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Pedidos _model;
        public ICommand SaveCommand { get; private set; }
        Action _itemSaved;
        IPedidosService _service;

        public PedidosVM(Pedidos model, Action itemSaved, IPedidosService service)
        {
            _service = service;
            _model = model;
            _itemSaved = itemSaved;
            if (_model != null)
            {
                FechaPedido = model.FechaPedido;
            }
            Descripcion = _model?.Descripcion;
            Nombre_Cliente = _model?.Nombre_Cliente;
            MinDate = DateTime.Today;
            MaxDate = DateTime.Today.AddMonths(1);
            SaveCommand = new Command(Save);

        }

        public DateTime _minDate;
        public DateTime MinDate
        {
            get => _minDate;
            set
            {
                _minDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinDate"));
            }
        }

        public DateTime _maxDate;
        public DateTime MaxDate
        {
            get => _maxDate;
            set
            {
                _maxDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxDate"));

            }
        }

        string _descripcion;
        public string Descripcion
        {
            get => _descripcion;
            set
            {
                _descripcion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Descripcion"));
                Validate();
            }
        }

        string _nombre_Cliente;
        public string Nombre_Cliente
        {
            get => _nombre_Cliente;
            set
            {
                _nombre_Cliente = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nombre_Cliente"));
                Validate();
            }
        }

        DateTime _fechaPedido;
        public DateTime FechaPedido
        {
            get => _fechaPedido;
            set
            {
                _fechaPedido = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FechaPedido"));
            }
        }

        bool _isDescripcionValid;
        public bool IsDescripcionValid
        {
            get => _isDescripcionValid;
            private set
            {
                _isDescripcionValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsDescripcionValid"));
            }
        }

        bool _isNombreValid;
        public bool IsNombreValid
        {
            get => _isNombreValid;
            private set
            {
                _isNombreValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsNombreValid"));
            }
        }

        private void Validate()
        {
            IsNombreValid = !string.IsNullOrWhiteSpace(_nombre_Cliente);
            IsDescripcionValid = !string.IsNullOrWhiteSpace(_descripcion);
        }

        public async Task SendEmail(string subject, string body, List<string> address)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = address,
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await App.Current.MainPage.DisplayAlert("Atención", "Su pedido no se ha enviado, contacte con la óptica", "OK");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void Save()
        {
            if (IsNombreValid && IsDescripcionValid)
            {
                if(FechaPedido < DateTime.Now && _model == null)
                {
                    FechaPedido = DateTime.Now;
                }
                if (_model == null)
                    await _service.Add(new Pedidos()
                    {
                        Descripcion = Descripcion,
                        Nombre_Cliente = Nombre_Cliente,
                        FechaPedido = FechaPedido,
                        FechaEnviado = DateTime.Now
                    });
                else
                {
                    _model.Descripcion = Descripcion;
                    _model.Nombre_Cliente = Nombre_Cliente;
                    _model.FechaPedido = FechaPedido;
                    await _service.Update(_model);
                }
                List<string> address = new List<string>();
                address.Add("opticaPMDP@gmail.com");
                string subject = "Pedido para " + Nombre_Cliente;
                string body = Descripcion;
                await SendEmail(subject, body, address);
                 _itemSaved?.Invoke();
            }

        }

    }
}
