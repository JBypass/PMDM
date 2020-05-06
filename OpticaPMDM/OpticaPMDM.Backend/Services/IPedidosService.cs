using OpticaPMDM.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpticaPMDM.Backend.Services
{
    public interface IPedidosService
    {
        Task Add(Pedidos pedidos);
        Task<List<Pedidos>> GetAll();
        Task<List<Pedidos>> GetAll(string descripcion);
        Task<Pedidos> GetByID(long Id);
        Task<List<Pedidos>> GetByDates(DateTime date1, DateTime date2);
        Task Remove(Pedidos pedidos);
        Task Update(Pedidos pedidos);

    }
}
