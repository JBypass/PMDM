using OpticaPMDM.Backend.Context;
using OpticaPMDM.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpticaPMDM.Backend.Services
{
    public class PedidosService : IPedidosService
    {
        static PedidosService _instance;
        public static PedidosService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PedidosService();
                }
                return _instance;
            }
        }
        public async Task Add(Pedidos pedidos)
        {
            await Database.INSTANCE.Add(pedidos);
            MessagingCenter.Send<PedidosService, Pedidos>(this, "Add", pedidos); ;
        }

        public async Task<List<Pedidos>> GetAll()
        {
            return await Database.INSTANCE.GetAll<Pedidos>();
        }

        public async Task<List<Pedidos>> GetByDates(DateTime date1, DateTime date2)
        {
            return await Database.INSTANCE.GetAll<Pedidos>(it => it.FechaPedido > date1 && it.FechaPedido < date2);
        }

        public async Task<Pedidos> GetByID(long Id)
        {
            return await Database.INSTANCE.GetSingle<Pedidos>(it => it.IdPedido == Id);
        }

        public async Task Remove(Pedidos pedidos)
        {
            await Database.INSTANCE.Delete(pedidos);
        }

        public async Task<List<Pedidos>> GetAll(string descripcion)
        {
            return await Database.INSTANCE.GetAll<Pedidos>(p => p.Descripcion.ToUpper().Contains(descripcion.ToUpper()));
        }

        public async Task Update(Pedidos pedidos)
        {
            await Database.INSTANCE.Update(pedidos);
            MessagingCenter.Send<PedidosService, Pedidos>(this, "Update", pedidos);
        }
    }
}
