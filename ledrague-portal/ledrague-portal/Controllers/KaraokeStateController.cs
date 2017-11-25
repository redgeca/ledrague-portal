using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using Microsoft.AspNetCore.Identity;
using ledrague_portal.Models;
using LeDragueCoreObjects.Karaoke;
using LeDragueCoreObjects.misc;

namespace leDraguePortal.Controllers
{
    [Produces("application/json", "text/plain")]
    [Route("api/KaraokeState")]
    public class KaraokeStateController : Controller
    {

        private readonly ApplicationDbContext dbContext;
        private readonly SignInManager<ApplicationUser> signInManager;

        public KaraokeStateController(ApplicationDbContext pContext, SignInManager<ApplicationUser> pSignInManager)
        {
            dbContext = pContext;
            signInManager = pSignInManager;
        }

        // GET: api/KaraokeState
        [HttpGet]
        public ActionResult Get()
        {
            Configuration state = dbContext.Configurations.Where(c => c.key == Constants.KARAOKE_STATE_FLAG).FirstOrDefault();
            if (state != null)
            {
                return Ok(state);
            }
            else
            {
                Configuration initialState = new Configuration();
                initialState.key = Constants.KARAOKE_STATE_FLAG;
                initialState.value = Constants.STOPPED_FLAG;
                initialState.lastUpdateTime = DateTime.Now;
                dbContext.Configurations.Add(initialState);
                dbContext.SaveChanges();
                state = initialState;
            }
            return Ok(state);
        }

        // GET: api/KaraokeState
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            Configuration state = dbContext.Configurations.Where(c => c.key == Constants.KARAOKE_STATE_FLAG).FirstOrDefault();
            if (state != null)
            {
                return Ok(state);
            }
            else
            {
                Configuration initialState = new Configuration();
                initialState.key = Constants.KARAOKE_STATE_FLAG;
                initialState.value = Constants.STOPPED_FLAG;
                initialState.lastUpdateTime = DateTime.Now;
                dbContext.Configurations.Add(initialState);
                dbContext.SaveChanges();
                state = initialState;
            }
            return Ok(state);
        }

        [HttpPut]
        [Consumes("application/json", "text/plain")]
        public IActionResult put([FromBody] string pNewState)
        {
            if (!Constants.RUNNING_FLAG.Equals(pNewState) && !Constants.STOPPED_FLAG.Equals(pNewState))
            {
                return BadRequest(pNewState);
            }

            Configuration state = dbContext.Configurations.Where(c => c.key == Constants.KARAOKE_STATE_FLAG).FirstOrDefault();
            if (state != null)
            {
                state.value = pNewState;
                state.lastUpdateTime = DateTime.Now;
                dbContext.Configurations.Update(state);
            }
            else
            {
                state = new Configuration();
                state.key = Constants.KARAOKE_STATE_FLAG;
                state.value = pNewState;
                state.lastUpdateTime = DateTime.Now;
                dbContext.Configurations.Add(state);
            }

            dbContext.SaveChanges();
            return Ok(state);
        }

    }
}