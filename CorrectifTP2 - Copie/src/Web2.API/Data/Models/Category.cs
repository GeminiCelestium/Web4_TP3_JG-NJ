using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Web2.API.Data.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Evenement> Evenements { get; set; }
    }
}
