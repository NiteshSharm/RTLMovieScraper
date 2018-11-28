using Microsoft.EntityFrameworkCore;
using RTLMovieScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTLMovieScraper.DataAccess
{
    public class MovieScrapperDBContext : DbContext
    {
        public MovieScrapperDBContext(DbContextOptions<MovieScrapperDBContext> options)
            : base(options)
        { }

        public DbSet<TVShow> TVShow { get; set; }
        public DbSet<Cast> Cast { get; set; }


        /// <summary>
        /// Override function allowing for adding extra constaints when the models are being created.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cast>().HasKey(m => m.id);
            builder.Entity<TVShow>().HasKey(m => m.id);

            // shadow properties
            builder.Entity<Cast>().Property<DateTime>("Updated_at");
            builder.Entity<TVShow>().Property<DateTime>("Updated_at");

            base.OnModelCreating(builder);
        }
        /// <summary>
        /// Override function to make changes right before persisting changes to the database.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<Cast>();
            updateUpdatedProperty<TVShow>();

            return base.SaveChanges();
        }

        /// <summary>
        /// Function that automatically updates the 'updated_now' property for each updated model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("Updated_at").CurrentValue = DateTime.Now;
            }
        }
    }
}
