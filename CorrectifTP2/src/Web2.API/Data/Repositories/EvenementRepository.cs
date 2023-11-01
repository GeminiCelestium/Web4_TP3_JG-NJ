using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public class EvenementRepository : GenericRepository<Evenement>, IEvenementRepository
    {
        public EvenementRepository(EventPlatformDbContext dbContext) : base(dbContext)
        {
        }

        public Evenement GetEvent(int id)
        {
            return _dbContext.Evenements.Include(e => e.Categories).Include(e => e.Ville).Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Evenement> GetList(int pageIndex, int pageCount, string searchQuery)
        {

            return _dbContext.Evenements.Include(e => e.Categories).Include(e => e.Ville)
                .Where(e => string.IsNullOrEmpty(searchQuery) || (e.Titre.ToUpper().Contains(searchQuery.ToUpper()) || e.Description.ToUpper().Contains(searchQuery.ToUpper())))
                .OrderByDescending(e => e.DateDebut)
                .Skip((pageIndex - 1) * pageCount).Take(pageCount).AsNoTracking();
        }

        public Evenement GetByIdIncludingCategories(int id)
        {
            return _dbContext.Evenements.Include(e => e.Categories).Include(e => e.Ville).FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Evenement> GetByVilleId(int id, int pageIndex, int pageCount)
        {
            return _dbContext.Evenements.Include(e => e.Categories).Include(e => e.Ville)
                .Where(e => e.Ville.ID == id)
                .OrderByDescending(e => e.ID)
                .Skip((pageIndex - 1) * pageCount).Take(pageCount).AsNoTracking();
        }

        public bool ExistForVille(int villeId)
        {
            return _dbContext.Evenements.Any(v => v.Ville.ID == villeId);
        }
        public double GetTotalVentes(int eventId)
        {
            var evenement = _dbContext.Evenements.Include(e => e.participations).FirstOrDefault(e => e.ID == eventId);
            if (evenement != null)
            {
                return evenement.participations.Sum(p => p.NombrePlace) * evenement.prix;
            }
            return 0;
        }

        public object GetEventsProfitability(){
            return  _dbContext.Evenements
                .Select(evenement => new
                {
                    Title = evenement.Titre,
                    Id = evenement.ID,
                    Amount =  evenement.prix * evenement.participations.Sum(p => p.NombrePlace)
                })
                .OrderByDescending(evt => evt.Amount)
                .ToList();
        }
    }
}
