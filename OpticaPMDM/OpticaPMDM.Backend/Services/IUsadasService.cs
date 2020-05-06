using OpticaPMDM.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace OpticaPMDM.Backend.Services
{
    public interface IUsadasService
    {
        Task Add(Usadas usadas);
        Task<List<Usadas>> GetAll();
        Task<Usadas> GetByID(long Id);
        Task<Usadas> GetLast(DateTime date);
        Task Remove(Usadas usadas);

        Task Update(Usadas usadas);

    }
}
