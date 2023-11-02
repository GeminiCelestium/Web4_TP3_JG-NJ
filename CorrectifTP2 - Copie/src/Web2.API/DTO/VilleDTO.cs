using Web2.API.Utils.Types;

namespace Web2.API.DTO
{
    public class VilleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public RegionType Region { get; set; }
    }

    public class VillePopularityDTO : VilleDTO
    {
        public int EventsCount { get; set; }

    }
}
