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
    public class ListaPedidosVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Action<Backend.Entities.Pedidos> _goToDetails;
        IPedidosService _service;
        public ICommand RemovePedidoCommand;

        public INavigation Navigation { get; set; }

        private ObservableCollection<PedidosL> _elements = new ObservableCollection<PedidosL>();
        public ObservableCollection<PedidosL> Elements { get => _elements; }

        PedidosL _selectedItem;
        public PedidosL SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value != null)
                {
                    var task = Task.Run(async () =>
                    {
                        return await PedidosService.Instance.GetByID(value.ID);
                    });
                    task.Wait();
                    _goToDetails?.Invoke(task.Result);
                }

            }
        }


        public ListaPedidosVM(Action<Backend.Entities.Pedidos> goToDetails, IPedidosService service)
        {
            _service = service;
            _goToDetails = goToDetails;
            FechaDesdeBuscar = DateTime.Today.AddMonths(-3);
            FechaHastaBuscar = DateTime.Today.AddMonths(3);
            MessagingCenter.Subscribe<PedidosService, Backend.Entities.Pedidos>(this,
                "Add", AddPedido);
            MessagingCenter.Subscribe<PedidosService, Backend.Entities.Pedidos>(this,
                "Update", UpdatePedido);
        }

        private void UpdatePedido(PedidosService sender, Backend.Entities.Pedidos item)
        {
            var element = this._elements.FirstOrDefault(el => el.ID == item.IdPedido);
            if (element != null)
            {
                element.FechaPedido = item.FechaPedido;
                element.Descripcion = item.Descripcion;
                element.NombreCliente = item.Nombre_Cliente;
            }
        }

        private void AddPedido(PedidosService sender, Backend.Entities.Pedidos item)
        {
            AddPedido(item);
        }

        private void AddPedido(Backend.Entities.Pedidos item)
        {
            _elements.Add(new PedidosL()
            {
                ID = item.IdPedido,
                Descripcion = item.Descripcion,
                NombreCliente = item.Nombre_Cliente,
                FechaPedido = item.FechaPedido,
                RemovePedidoCommand = new Command<PedidosL>(RemovePedido)
            });
        }

        private async void RemovePedido(PedidosL pedido)
        {
            var itemToRemove = await _service.GetByID(pedido.ID);
            await _service.Remove(itemToRemove);
            _elements.Remove(pedido);
        }

        private DateTime _fechaDesdeBuscar;
        public DateTime FechaDesdeBuscar
        {
            get
            {
                return _fechaDesdeBuscar;
            }
            set
            {
                _fechaDesdeBuscar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FechaDesdeBuscar)));
                BuscarPorFechaCommand();
            }
        }

        private DateTime _fechaHastaBuscar;
        public DateTime FechaHastaBuscar
        {
            get
            {
                return _fechaHastaBuscar;
            }
            set
            {
                _fechaHastaBuscar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FechaHastaBuscar)));
                BuscarPorFechaCommand();
            }
        }

        private async Task BuscarPorFechaCommand()
        {
            var serviceItems = new List<Backend.Entities.Pedidos>();
            _elements.Clear();
            serviceItems = await PedidosService.Instance.GetByDates(FechaDesdeBuscar, FechaHastaBuscar);
            foreach (var item in serviceItems)
                AddPedido(item);
        }

        private string _searchedText;
        public string SearchedText
        {
            get
            {
                return _searchedText;
            }
            set
            {
                _searchedText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedText)));
                SearchCommandExecuteAsync();
            }
        }

        private async Task SearchCommandExecuteAsync()
        {
            var serviceItems = new List<Backend.Entities.Pedidos>();
            _elements.Clear();
            if (!string.IsNullOrWhiteSpace(_searchedText))
                serviceItems = await PedidosService.Instance.GetAll(_searchedText);
            else
                serviceItems = await PedidosService.Instance.GetAll();

            foreach (var item in serviceItems)
                AddPedido(item);
        }

        bool _initialized;
        public async Task Initialize()
        {
            if (!_initialized)
            {
                var serviceItems = await _service.GetAll();
                foreach (var item in serviceItems)
                    AddPedido(item);
                _initialized = true;
            }
        }

        public class PedidosL : INotifyPropertyChanged
        {
            public Command<PedidosL> RemovePedidoCommand { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            public long ID { get; set; }

            string _nombreCliente;
            public string NombreCliente
            {
                get => _nombreCliente;
                set
                {
                    _nombreCliente = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NombreCliente)));
                }
            }

            string _descripcion;
            public string Descripcion
            {
                get => _descripcion;
                set
                {
                    _descripcion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Descripcion)));
                }
            }

            DateTime _fechaPedido;
            public DateTime FechaPedido
            {
                get => _fechaPedido;
                set
                {
                    _fechaPedido = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FechaPedido)));
                }
            }
        }
    }
}