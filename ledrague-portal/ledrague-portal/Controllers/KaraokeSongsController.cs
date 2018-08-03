using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDraguePortal.utils;
using LeDragueCoreObjects.Karaoke;
using LeDragueCoreObjects.lucene;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace leDraguePortal.Controllers
{
    [Produces("application/json", "text/plain")]
    [Route("api/KaraokeSongs")]
    public class KaraokeSongsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public KaraokeSongsController(ApplicationDbContext pDbContext)
        {
            dbContext = pDbContext;
        }

        // GET: api/KaraokeState
        [HttpGet("byPage")]
        public async Task<IActionResult> Get(String filter, String orderBy, int? page, int? pageSize,
            int? artistId, int? categoryId)
        {
            IQueryable<Song> songs = dbContext.KaraokeSongs
                    .Include(cs => cs.Artist)
                    .Include(cs => cs.CategorySongs)
                    .ThenInclude(cat => cat.Category);

            if (filter != null && !filter.Equals("null"))
            {
                Searcher searcher = new Searcher();
                songs = songs.Where(s => searcher.searchTitles(filter).Contains(s.Id));
            }
//            var ids = new int[] { 11484, 20662, 21720 };
//            songs = songs.Where(s => ids.Contains(s.Id));

            if (artistId != null)
            {
                songs = songs.Where(a => a.ArtistId == artistId);
            }
//            else if (categoryId != null)
//            {
//                songs = songs.Where(c => c.CategorySongs.Contains(categoryId ?? 0));
//            }

            switch (orderBy)
            {
                case "title_desc":
                    songs = songs.OrderByDescending(cs => cs.Title).ThenBy(cs => cs.Artist.Name);
                    break;

                case "artist_asc":
                    songs = songs.OrderBy(cs => cs.Artist.Name).ThenBy(cs => cs.Title);
                    break;

                case "artist_desc":
                    songs = songs.OrderByDescending(cs => cs.Artist.Name).ThenBy(cs => cs.Title);
                    break;

                default:
                    songs = songs.OrderBy(cs => cs.Title).ThenBy(cs => cs.Artist.Name);
                    break;
            }


            PaginatedList<Song> resultSongs = await PaginatedList<Song>.CreateAsync(songs.AsNoTracking(), page ?? 1, pageSize ?? 25);
            var jsonObject = new
            {
                count = resultSongs.TotalItems,
                values = resultSongs
            };

            return new JsonResult(jsonObject);
            
        }
    }
}