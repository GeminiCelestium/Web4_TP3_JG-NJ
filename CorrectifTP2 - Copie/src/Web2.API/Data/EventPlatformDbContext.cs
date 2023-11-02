using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web2.API.Data.Models;

namespace Web2.API.Data
{
    public class EventPlatformDbContext : DbContext
    {
        public EventPlatformDbContext(DbContextOptions<EventPlatformDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
            .Entity<Ville>()
            .Property(v => v.Region)
            .HasConversion<string>();

            modelBuilder.Entity<Participation>().HasQueryFilter(p => p.IsValid);

            modelBuilder.Entity<Evenement>().HasMany(e => e.participations).WithOne(p => p.Evenement).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Participation>().Navigation(p => p.Evenement).AutoInclude();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CheckForValidParticipation();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void CheckForValidParticipation()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Participation participation)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            participation.IsValid = false;
                            break;
                    }
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CheckForValidParticipation();
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participation> Participations { get; set; }
    }
}
