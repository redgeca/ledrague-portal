using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using Microsoft.EntityFrameworkCore;

namespace leDraguePortal.Controllers
{
    [Produces("application/json")]
    [Route("api/Applications")]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ApplicationsController(ApplicationDbContext pContext)
        {
            dbContext = pContext;
        }

        // GET: api/Applications
        [HttpGet]
        public ActionResult get()
        {
            return Ok(dbContext.Applications.ToList());
        }

        [HttpGet("{name}")]
        public ActionResult get(string name)
        {
            var result = dbContext.Applications.Include(a => a.ApplicationRights).ThenInclude(r => r.Category)
                .Where(a => a.Name.Equals(name)).ToList();
            result.ForEach(ar => ar.ApplicationRights = ar.ApplicationRights.OrderBy(r => r.DisplayName).ToList());
            return Ok(result.FirstOrDefault());
        }
    }
}