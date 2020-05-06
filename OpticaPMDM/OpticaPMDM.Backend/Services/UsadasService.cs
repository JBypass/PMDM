using OpticaPMDM.Backend.Context;
using OpticaPMDM.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpticaPMDM.Backend.Services
{
    public class UsadasService : IUsadasService
    {
        static UsadasService _instance;
        public static UsadasService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new UsadasService();
                }
                return _instance;
            }
        }

        public async Task Add(Usadas usadas)
        {
            await Database.INSTANCE.Add(usadas);
            MessagingCenter.Send<UsadasService, Usadas>(this, "Add", usadas);

        }

        public async Task<List<Usadas>> GetAll()
        {
            return await Database.INSTANCE.GetAll<Usadas>();
        }

        public async Task<Usadas> GetByID(long Id)
        {
            return await Database.INSTANCE.GetSingle<Usadas>(it => it.IdUsadas == Id);
        }

        public async Task<Usadas> GetLast(DateTime date)
        {
            return await Database.INSTANCE.GetSingle<Usadas>(it => it.FechaCaducidad > date);
        }

        public async Task Remove(Usadas usadas)
        {
            await Database.INSTANCE.Delete(usadas);
        }

        public async Task Update(Usadas usadas)
        {
            await Database.INSTANCE.Update(usadas);
            MessagingCenter.Send<UsadasService, Usadas>(this, "Update", usadas);
        }
    }
}
