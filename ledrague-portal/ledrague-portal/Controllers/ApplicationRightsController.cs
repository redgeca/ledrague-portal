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
    [Route("api/ApplicationRights")]
    public class ApplicationRightsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ApplicationRightsController(ApplicationDbContext pContext)
        {
            dbContext = pContext;
        }

        public ActionResult getRights()
        {
            return Ok(dbContext.Applications.Include(a => a.ApplicationRights).ToList());
        }
    }
}