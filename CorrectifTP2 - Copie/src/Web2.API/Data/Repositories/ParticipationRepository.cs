using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public class ParticipationRepository : GenericRepository<Participation>, IParticipationRepository
    {
        public ParticipationRepository(EventPlatformDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Participation> GetByEvent(int eventId)
        {
            return _dbContext.Participations.Include(p => p.Evenement).Where(x => x.Evenement.ID == eventId).AsNoTracking();
        }

        public bool ExistForEvenementAndEmail(int eventId, string email)
        {
            return _dbContext.Participations.Any(x => x.Evenement.ID == eventId && x.Email.ToUpper().Equals(email.Trim().ToUpper()));
        }
        public Participation GetByIdIgnoreQueryFilter(int id)
        {
            return _dbContext.Participations.Include(p => p.Evenement).Where(x => x.ID == id).IgnoreQueryFilters().FirstOrDefault();
        }
    }
}
