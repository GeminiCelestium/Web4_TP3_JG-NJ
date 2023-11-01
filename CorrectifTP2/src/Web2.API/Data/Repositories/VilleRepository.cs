using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public class VilleRepository : GenericRepository<Ville>, IVilleRepository
    {
        public VilleRepository(EventPlatformDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Ville> GetList()
        {
            return GetAll().AsEnumerable();
        }

        public Ville GetVille(int id)
        {
            return _dbContext.Villes.FirstOrDefault(x => x.ID == id);
        }
        public IEnumerable<VillePopularityDTO> GetVillesOrderByPopularity()
        {
            return _dbContext.Villes
                .OrderByDescending(v => v.Evenements.Count)
                .Select(v => new VillePopularityDTO { ID = v.ID, Name = v.Name, Region = v.Region, EventsCount = v.Evenements.Count });
        }
    }
}
