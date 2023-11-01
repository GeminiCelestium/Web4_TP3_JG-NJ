using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;

namespace Web2.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EventPlatformDbContext context)
        {
            // Look for any data
            if (context.Evenements.Any() || context.Villes.Any() || context.Categories.Any() || context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var villes = new Ville[]
                {
                    new Ville { Name = "Sherbrooke", Region = Utils.Types.RegionType.ESTRIE},
                    new Ville { Name = "Bromont", Region = Utils.Types.RegionType.ESTRIE},
                    new Ville { Name = "Shawinigan", Region = Utils.Types.RegionType.MAURICIE},
                    new Ville { Name = "Trois-Rivieres", Region = Utils.Types.RegionType.MAURICIE},
                    new Ville { Name = "Amos", Region = Utils.Types.RegionType.ABITIBI},
                    new Ville { Name = "Ville-Marie", Region = Utils.Types.RegionType.ABITIBI}
                };
            context.Villes.AddRange(villes);

            var categories = new Category[]
                {
                    new Category { Name = "Festival" },
                    new Category { Name = "Carnaval" },
                    new Category { Name = "Concert" },
                    new Category { Name = "Musique" },
                    new Category { Name = "Jeux" },
                    new Category { Name = "Sport" },
                    new Category { Name = "Politique" },
                };
            context.Categories.AddRange(categories);


            var evenements = new Evenement[]
                {
                    new Evenement {
                        Titre = "Jazz Fest", Description = "Festival de Jazz", Adresse = "1420 rue de callieres", Ville = villes[0], DateDebut = DateTime.Now.AddDays(1), DateFin = DateTime.Now.AddDays(4), Categories = new Category[] { categories[0], categories[3] }, Organisateur = "Def Jam", prix = 65 
                    },
                     new Evenement {
                        Titre = "Manifestation antivac", Description = "Convois de la liberté", Adresse = "85 rue PIE XII", Ville = villes[0], DateDebut = DateTime.Now.AddDays(-1), DateFin = DateTime.Now.AddDays(4), Categories = new Category[] { categories[6] }, Organisateur = "BoJack"
                    },
                      new Evenement {
                        Titre = "Grand prix de 3r", Description = "Course automobile", Adresse = "321 rue des forges", Ville = villes[3], DateDebut = DateTime.Now.AddDays(-1), DateFin = DateTime.Now.AddDays(4), Categories = new Category[] { categories[5] }, Organisateur = "Bull Racing"
                    }
                };
            context.Evenements.AddRange(evenements);


            var partcipations = new Participation[]
                {
                    new Participation { Email = "Carson@gmail.com", Nom = "Carson", Prenom = "Alexander", NombrePlace = 3, Evenement = evenements[0]},
                    new Participation { Email = "Meredith@gmail.com", Nom = "Meredith", Prenom = "Meredith", Evenement = evenements[0]},
                    new Participation { Email = "Arturo@gmail.com", Nom = "Arturo", Prenom = "Anand", Evenement = evenements[1]},
                    new Participation { Email = "Arturo@gmail.com", Nom = "Gytis", Prenom = "Barzdukas", Evenement = evenements[1]},
                    new Participation { Email = "black@gmail.com", Nom = "Joe", Prenom = "Black",  NombrePlace = 3, Evenement = evenements[2]},
                    new Participation { Email = "Yan@gmail.com", Nom = "Yan", Prenom = "Li",  NombrePlace = 6, Evenement = evenements[2]},
                };
            context.Participations.AddRange(partcipations);

            context.SaveChanges();

        }
     }
}
