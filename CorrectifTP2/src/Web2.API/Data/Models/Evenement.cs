using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web2.API.Data.Models
{
    public class Evenement
    {
        public int ID { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        [Required]
        public string Organisateur { get; set; }
        public DateTime DateFin { get; set; }
        [Required]
        public string Adresse { get; set; }
        public double prix { get; set; }

        [Required]
        public Ville Ville { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Participation> participations { get; set; }

    }
}

