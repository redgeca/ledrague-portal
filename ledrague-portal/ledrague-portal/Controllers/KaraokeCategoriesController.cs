using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDragueCoreObjects.Karaoke;
using Microsoft.EntityFrameworkCore;
using LeDraguePortal.utils;
using LeDragueCoreObjects.lucene;

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
        [HttpGet]
        public async Task<IActionResult> Get(String filter, String orderBy, int? page, int? pageSize)
        {
            IQueryable<Category> categories = dbContext.KaraokeCategories;
//                .Include(c => c.CategorySongs);

            if (filter != null && !filter.Equals("null"))
            {
                Searcher searcher = new Searcher();
                categories = categories.Where(s => searcher.searchCategories(filter).Contains(s.Id));
            }

            switch (orderBy)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(cs => cs.Name);
                    break;

                default:
                    categories = categories.OrderBy(cs => cs.Name);
                    break;
            }

            PaginatedList<Category> resultCategories = await PaginatedList<Category>.CreateAsync(categories.AsNoTracking(), page ?? 1, pageSize ?? 25);

            Request.HttpContext.Response.Headers.Add("X-Total-Count", resultCategories.TotalItems.ToString());
            return new JsonResult(resultCategories);
        }

        // GET: api/KaraokeCategories/{id}
        [HttpGet("{id}")]
        public ActionResult Get(Int32 id)
        {
            var category = dbContext.KaraokeCategories
                 .Include(c => c.CategorySongs).ThenInclude(cs => cs.Song).Where(c => c.Id == id)
                 .OrderBy(cs => cs.CategorySongs.Select(s => s.Song.Title)).FirstOrDefault();

            if (category == null)
            {
                return BadRequest("Invalid Category");
            }
            return new JsonResult(category);
        }

    }
}