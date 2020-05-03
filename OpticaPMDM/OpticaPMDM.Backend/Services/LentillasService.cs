using OpticaPMDM.Backend.Context;
using OpticaPMDM.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpticaPMDM.Backend.Services
{
    public class LentillasService : ILentillasService
    {
        static LentillasService _instance;
        public static LentillasService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LentillasService();
                }
                return _instance;
            }
        }

        public async Task Add(Lentillas lentillas)
        {
            await Database.INSTANCE.Add(lentillas);
            MessagingCenter.Send<LentillasService, Lentillas>(this, "Add", lentillas);
        }

        public async Task<List<Lentillas>> GetAll()
        {
            return await Database.INSTANCE.GetAll<Lentillas>();
        }

        public async Task<Lentillas> GetByID(long Id)
        {
            return await Database.INSTANCE.GetSingle<Lentillas>(it => it.IdLentillas == Id);
        }

        public async Task<Lentillas> GetByStock()
        {
            return await Database.INSTANCE.GetSingle<Lentillas>(it => it.Dias_Restantes > 0);
        }

        public async Task Remove(Lentillas lentillas)
        {
            await Database.INSTANCE.Delete(lentillas);
        }

        public async Task Update(Lentillas lentillas)
        {
            await Database.INSTANCE.Update(lentillas);
            MessagingCenter.Send<LentillasService, Lentillas>(this, "Update", lentillas);
        }
    }
}
