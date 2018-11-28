using Microsoft.AspNetCore.Mvc;
using RTLMovieScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RTLMovieScraper.Helpers
{
    public class ApiHelpers
    {
        static HttpClient client = new HttpClient();
        internal static async Task<List<TVShow>> getAllShowsAsync()
        {
            List<TVShow> showList = await getAllSHowsAsync();
            return showList;
        }

        private static async Task<List<TVShow>> getAllSHowsAsync()
        {
            List<TVShow> showList = null;
            HttpResponseMessage response = await client.GetAsync("http://api.tvmaze.com/shows");
            if (response.IsSuccessStatusCode)
            {
                showList = await response.Content.ReadAsAsync<List<TVShow>>();
            }
            return showList;
        }
        /// <summary>
        /// Update the cast list in the showlist
        /// </summary>
        /// <param name="showlist"></param>
        /// <returns></returns>
        internal static async Task<List<TVShow>> UpdateCastsAsync(List<TVShow> showlist)
        {
            List<TVShow> updatedList = new List<TVShow>();
            foreach (TVShow show in showlist)
            {               
                updatedList.Add(await getCastsAndupdateshow(show));
            }
            return updatedList;
        }

        private static async Task<TVShow> getCastsAndupdateshow(TVShow show)
        {
            HttpResponseMessage response = await client.GetAsync($"http://api.tvmaze.com/shows/{show.id}/cast");
            if (response.IsSuccessStatusCode)
            {
                var casts = await response.Content.ReadAsAsync<List<CastFromScraper>>();
                show.Casts = casts.Select(cast => new Cast() { birthday = (cast.person.birthday != null ? DateTime.Parse(cast.person.birthday) : DateTime.MinValue), name = cast.person.name }).ToList();
            }
            return show;
        }

        internal static async Task<ActionResult<TVShow>> GetShowCastsbyIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"http://api.tvmaze.com/shows/{id}");
            if (response.IsSuccessStatusCode)
            {
                var show = await response.Content.ReadAsAsync<TVShow>();
                return await getCastsAndupdateshow(show);
            }
            else return null ;
        }
    }
}
