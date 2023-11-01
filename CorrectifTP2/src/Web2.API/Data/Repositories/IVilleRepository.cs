using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public interface IVilleRepository : IGenericRepository<Ville>
    {
        IEnumerable<Ville> GetList();
        Ville GetVille(int id);
        IEnumerable<VillePopularityDTO> GetVillesOrderByPopularity();
    }
}
