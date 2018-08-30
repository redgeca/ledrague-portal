using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDragueCoreObjects.Karaoke;
using LeDragueCoreObjects.misc;
using Microsoft.EntityFrameworkCore;

namespace leDraguePortal.Controllers
{
    [Produces("application/json")]
    [Route("api/Requests")]
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public RequestController(ApplicationDbContext pContext)
        {
            dbContext = pContext;
        }

        private DateTime getLastStartupDate()
        {
            Configuration state = dbContext.Configurations.Where(c => c.key == Constants.KARAOKE_STATE_FLAG).FirstOrDefault();
            if (state == null) {
                return DateTime.Now;
            }

            return state.lastUpdateTime;
        }

        //        [Authorize(Roles="animation,admin")]
        [HttpGet]
        public ActionResult GetRequests()
        {
            DateTime lastStartedDate = getLastStartupDate();

            var queuedRequests = dbContext.KaraokePlaylists.Select(p => p.RequestId).ToList();

            var validRequests = dbContext.KaraokeRequests
                .Include(r => r.Song).ThenInclude(s => s.Artist)
                .OrderBy(r => r.RequestTime)
                .Where(r => !queuedRequests.Contains(r.Id) && r.RequestTime >= lastStartedDate).ToList();

            return Ok(validRequests);
        }

        // GET: api/KaraokeState
        //        [Authorize(Roles="animation,admin")]
        [HttpGet("{id}")]
        public ActionResult GetRequest(int id)
        {
            Request request = dbContext.KaraokeRequests
                .Include(r => r.Song)
                .ThenInclude(s => s.Artist)
                .Where(r => r.Id == id).FirstOrDefault();

            if (request == null)
            {
                return BadRequest("Request doesn't exist");
            }
            return Ok(request);
        }

        //        [Authorize(Roles="animation,admin")]
        [HttpPost]
        public ActionResult AddToRequestlist([FromBody] Request pRequest)
        {
            Song song = dbContext.KaraokeSongs.Where(s => s.Id == pRequest.SongId).FirstOrDefault();
            if (song == null)
            {
                return BadRequest("Invalid Song");
            }

            if (pRequest.SingerName == null || pRequest.SingerName.Trim().Equals(""))
            {
                return BadRequest("Invalid Singer name");
            }

            Request request = new Request();
            request.RequestTime = DateTime.Now;
            request.SingerName = pRequest.SingerName;
            request.Notes = pRequest.Notes;
            request.Song = song;

            dbContext.KaraokeRequests.Add(request);
            dbContext.SaveChanges();

            int requestId = request.Id;
            Request newRequest = dbContext.KaraokeRequests
                .Include(r => r.Song).ThenInclude(s => s.Artist)
                .Where(r => r.Id == requestId).FirstOrDefault();

            return Ok(newRequest);
        }

        [HttpDelete]
        public ActionResult DeleteRequest(int id)
        {
            Request entry = dbContext.KaraokeRequests
                .Where(p => p.Id == id).FirstOrDefault();

            if (entry == null)
            {
                return BadRequest("Entry doesn't exist in Requests");
            }

            dbContext.KaraokeRequests.Remove(entry);

            dbContext.SaveChanges();

            return Ok();
        }

    }
}