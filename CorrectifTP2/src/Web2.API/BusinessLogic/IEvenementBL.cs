using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface IEvenementBL
    {
        public IEnumerable<EvenementDTO> GetList(int pageIndex, int pageCount, string searchQuery);
        public EvenementDTO Get(int id);

        public EvenementDTO Add(EvenementDTO value);
        public EvenementDTO Updade(int id, EvenementDTO value);
        public void Delete(int id);

        public IEnumerable<EvenementDTO> GetByVille(int villeId, int pageIndex, int pageCount);

        double GetTotalVentes(int eventId);
        object GetEventsProfitability();
    }
}
