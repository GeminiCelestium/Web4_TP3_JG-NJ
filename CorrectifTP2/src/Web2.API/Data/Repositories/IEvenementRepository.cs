using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public interface IEvenementRepository : IGenericRepository<Evenement>
    {
        Evenement GetEvent(int id);
        IEnumerable<Evenement> GetList(int pageIndex, int pageCount, string searchQuery);

        Evenement GetByIdIncludingCategories(int id);
        IEnumerable<Evenement> GetByVilleId(int id, int pageIndex, int pageCount);
        bool ExistForVille(int villeId);
        public double GetTotalVentes(int eventId);
        public object GetEventsProfitability();

    }
}
