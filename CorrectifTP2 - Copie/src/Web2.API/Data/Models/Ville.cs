using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web2.API.Utils.Types;

namespace Web2.API.Data.Models
{
    public class Ville
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public RegionType Region { get; set; }
        public ICollection<Evenement> Evenements { get; set; }

    }
}
