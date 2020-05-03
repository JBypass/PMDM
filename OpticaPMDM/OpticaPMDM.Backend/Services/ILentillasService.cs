using OpticaPMDM.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpticaPMDM.Backend.Services
{
    public interface ILentillasService
    {
        Task Add(Lentillas lentillas);
        Task<List<Lentillas>> GetAll();
        Task<Lentillas> GetByID(long Id);
        Task<Lentillas> GetByStock();
        Task Remove(Lentillas lentillas);

        Task Update(Lentillas lentillas);
    }
}
