using Microsoft.EntityFrameworkCore;
using RTLMovieScraper.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RTLMovieScraper.Models
{
    public class TVShow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        //public int TVMazeid { get; set; }
        public string name { get; set; }

        public ICollection<Cast> Casts { get; set; }

        /// <summary>
        /// Update show list and respective casts as well.
        /// </summary>
        /// <param name="showlistWithCasts"></param>
        /// <param name="context"></param>
        internal static void UpdateShows(List<TVShow> showlistWithCasts, MovieScrapperDBContext context)
        {
            context.TVShow.AddRange(showlistWithCasts);

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TVShow] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.TVShow OFF");
            }
            //TO do handle exceptions
            catch (Exception ex)
            {
            }
            finally
            {
                context.Database.CloseConnection();
            }

        }
    }
}
