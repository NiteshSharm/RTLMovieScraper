using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTLMovieScraper.DataAccess;
using RTLMovieScraper.Helpers;
using RTLMovieScraper.Models;

namespace RTLMovieScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly MovieScrapperDBContext _context;
        public ShowsController(MovieScrapperDBContext context)
        {
            _context = context;
        }
        // GET api/shows
        /// <summary>
        /// Returns the paginated list of TVShows. If no result then please execute "PUT api/shows" First
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PagedList<TVShow>> Get([FromQuery] int? PageNumber, [FromQuery] int? PageSize)
        {
            IQueryable<TVShow> query = _context.TVShow.Include(r => r.Casts).AsQueryable();
            var paginatedList = new PagedList<TVShow>(
                query, PageNumber, PageSize);
            foreach (var show in paginatedList.List)
            {
                var sortedcast = show.Casts.OrderByDescending(x => x.birthday);
                show.Casts = sortedcast.ToList();
            }
            return paginatedList;
        }

        // GET api/shows/5
        /// <summary>
        /// Search a specific Show by it's ID. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> Get(int id)
        {
            var resultFound= _context.TVShow.Include(r => r.Casts).FirstOrDefault(m => m.id == id);
            if (resultFound != null)
            {
                resultFound.Casts = resultFound.Casts.OrderByDescending(x => x.birthday).ToList();
                return resultFound;
            }
            else {
                return await ApiHelpers.GetShowCastsbyIdAsync(id);
            }
        }

        // PUT api/shows
        /// <summary>
        /// Initialize the local database. get the shows information from the API and store in the local database
        /// </summary>
        [HttpPut]
        public async Task InitializeLocalDatabaseAsync()
        {
            List<TVShow> showlistWithCasts = await getShowsToInsert();
            TVShow.UpdateShows(showlistWithCasts, _context);

        }


        #region private methods
        /// <summary>
        /// Get alll the shows and then their respective casts. Filtered with existing shows already in the database.
        /// </summary>
        /// <returns>List<TVShow></returns>
        private async Task<List<TVShow>> getShowsToInsert()
        {
            List<TVShow> showlist = await ApiHelpers.getAllShowsAsync();

            var existingShows = _context.TVShow.ToDictionary(m => m.id, m => m);
            var showDictionary = showlist.ToDictionary(m => m.id, m => m);
            var showListToInsert = showDictionary.Where(d => !existingShows.ContainsKey(d.Key)).Select(m => m.Value).ToList();

            return await ApiHelpers.UpdateCastsAsync(showListToInsert);
        }
        #endregion
    }
}
