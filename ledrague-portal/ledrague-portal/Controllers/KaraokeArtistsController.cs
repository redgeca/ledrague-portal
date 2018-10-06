using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDragueCoreObjects.Karaoke;
using LeDraguePortal.utils;
using Microsoft.EntityFrameworkCore;
using LeDragueCoreObjects.lucene;

namespace leDraguePortal.Controllers
{
    [Produces("application/json", "text/plain")]
    [Route("api/KaraokeArtists")]
    public class KaraokeArtistsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public KaraokeArtistsController(ApplicationDbContext pDbContext)
        {
            dbContext = pDbContext;
        }

        // GET: api/KaraokeState
        [HttpGet]
        public async Task<IActionResult> Get(String filter, String orderBy, int? page, int? pageSize)
        {
            IQueryable<Artist> artists = dbContext.KaraokeArtists;
//                .Include(a => a.Songs);

            if (filter != null && !filter.Equals("null"))
            {
                Searcher searcher = new Searcher();
                artists = artists.Where(a => searcher.searchArtist(filter).Contains(a.Id));
            }
            switch (orderBy)
            {
                case "name_desc":
                    artists = artists.OrderByDescending(a => a.Name);
                    break;

                default:
                    artists = artists.OrderBy(a => a.Name);
                    break;
            }

            PaginatedList<Artist> resultArtists = await PaginatedList<Artist>.CreateAsync(artists.AsNoTracking(), page ?? 1, pageSize ?? 25);

            Request.HttpContext.Response.Headers.Add("X-Total-Count", resultArtists.TotalItems.ToString());
            return new JsonResult(resultArtists);
        }

        // GET: api/KaraokeState
        [HttpGet("{id}")]
        public ActionResult Get(Int32 id)
        {
            var artist = dbContext.KaraokeArtists
                 .Include(a => a.Songs).Where(a => a.Id == id)
                 .OrderBy(ars => ars.Songs.Select(s => s.Title)).FirstOrDefault();
            if (artist == null)
            {
                return BadRequest("Invalid Artist");
            }
            return new JsonResult(artist);
        }
    }
}