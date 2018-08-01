using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDragueCoreObjects.Karaoke;
using Microsoft.EntityFrameworkCore;
using LeDraguePortal.utils;

namespace leDraguePortal.Controllers
{
    [Produces("application/json", "text/plain")]
    [Route("api/KaraokeCategories")]
    public class KaraokeCategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public KaraokeCategoriesController(ApplicationDbContext pDbContext)
        {
            dbContext = pDbContext;
        }

        // GET: api/KaraokeState
        [HttpGet("byPage")]
        public async Task<IActionResult> Get(String filter, String orderBy, int? page, int? pageSize)
        {
            IQueryable<Category> categories = dbContext.KaraokeCategories
                .Include(c => c.CategorySongs);

            switch (orderBy)
            {
                case "desc":
                    categories = categories.OrderByDescending(cs => cs.Name);
                    break;

                default:
                    categories = categories.OrderBy(cs => cs.Name);
                    break;
            }

            return Ok(await PaginatedList<Category>.CreateAsync(categories.AsNoTracking(), page ?? 1, pageSize ?? 25));
        }
    }
}