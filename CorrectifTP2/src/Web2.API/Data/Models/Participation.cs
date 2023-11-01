using System.ComponentModel.DataAnnotations;

namespace Web2.API.Data.Models
{
    public class Participation
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public int NombrePlace { get; set; } = 1;
        [Required]
        public Evenement Evenement { get; set; }
        public bool IsValid { get; set; }
    }
}
