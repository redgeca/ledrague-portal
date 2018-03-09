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
            var result = dbContext.Applications.Include(a => a.ApplicationRights)
            //dbContext.CiaApplications.Include(a => a.ApplicationRights)
                .OrderBy(a => a.Name).ToList();

            result.ForEach(ar => ar.ApplicationRights = ar.ApplicationRights.OrderBy(r => r.DisplayName).ToList());
      
            return Ok(result);
        }
    }
}