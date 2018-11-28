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
                HttpResponseMessage response = await client.GetAsync($"http://api.tvmaze.com/shows/{show.id}/cast");
                if (response.IsSuccessStatusCode)
                {
                    TVShow updatedShow = new TVShow();
                    var casts = await response.Content.ReadAsAsync<List<CastFromScraper>>();
                    updatedShow.Casts = casts.Select(cast => new Cast() { birthday = cast.person.birthday, name = cast.person.name }).ToList();
                    updatedShow.name = show.name; updatedShow.id = show.id;
                    updatedList.Add(updatedShow);
                }
            }
            return updatedList;
        }


    }
}
