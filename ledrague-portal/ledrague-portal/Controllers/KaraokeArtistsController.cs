using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDragueCoreObjects.Karaoke;
using LeDraguePortal.utils;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("byPage")]
        public async Task<IActionResult> Get(String filter, String orderBy, int? page, int? pageSize)
        {
            IQueryable<Artist> artists = dbContext.KaraokeArtists
                .Include(a => a.Songs);

            switch (orderBy)
            {
                case "desc":
                    artists = artists.OrderByDescending(a => a.Name);
                    break;

                default:
                    artists = artists.OrderBy(a => a.Name);
                    break;
            }

            return Ok(await PaginatedList<Artist>.CreateAsync(artists.AsNoTracking(), page ?? 1, pageSize ?? 25));
        }
    }
}