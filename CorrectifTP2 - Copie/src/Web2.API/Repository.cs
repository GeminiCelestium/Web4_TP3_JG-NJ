using System.Collections.Generic;
using Web2.API.DTO;

namespace Web2.API
{
    public static class Repository
    {
        public static int IdSequenceVille { get; set; } = 1;
        public static int IdSequenceEvenement { get; set; } = 1;
        public static int IdSequenceParticipation { get; set; } = 1;
        public static int IdSequenceCategory { get; set; } = 1;
        public static ISet<VilleDTO> Villes { get; set; } = new HashSet<VilleDTO>();
        public static ISet<EvenementDTO> Evenements = new HashSet<EvenementDTO>();
        public static ISet<ParticipationDTO> Participations = new HashSet<ParticipationDTO>();
        public static ISet<CategoryDTO> Categories = new HashSet<CategoryDTO>();

    }
}
