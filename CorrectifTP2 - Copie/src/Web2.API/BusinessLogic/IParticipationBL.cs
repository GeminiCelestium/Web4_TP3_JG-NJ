using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface IParticipationBL
    {
        public IEnumerable<ParticipationDTO> GetList();
        public ParticipationDTO Get(int id);

        public ParticipationDTO Add(ParticipationDTO value);
        public void Delete(int id);

        public bool GetStatus(int id);

        public IEnumerable<ParticipationDTO> GetByEvent(int eventId);
    }
}
