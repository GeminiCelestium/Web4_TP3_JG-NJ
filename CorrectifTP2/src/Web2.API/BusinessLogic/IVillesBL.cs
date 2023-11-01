using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface IVilleBL
    {
        public IEnumerable<VilleDTO> GetList(bool orderByPopularity);
        public VilleDTO Get(int id);

        public VilleDTO Add(VilleDTO value);
        public VilleDTO Updade(int id, VilleDTO value);
        public void Delete(int id);
    }
}
