using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeDragueCoreObjects.misc;
using ledrague_portal.Data;
using Microsoft.EntityFrameworkCore;
using LeDragueCoreObjects.Karaoke;

namespace leDraguePortal.Controllers
{
    [Produces("application/json")]
    [Route("api/Playlist")]
    public class PlaylistController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PlaylistController(ApplicationDbContext pContext)
        {
            dbContext = pContext;
        }

        //        [Authorize(Roles="animation,admin,DJ")]
        [HttpGet]
        public ActionResult GetPlaylists()
        {
            var actualPlaylist = dbContext.KaraokePlaylists
                .Include(p => p.Request).ThenInclude(r => r.Song).ThenInclude(s => s.Artist)
                .Where(p => p.IsDone == 0).OrderBy(p => p.playOrder);

            return Ok(actualPlaylist);
        }

        //        [Authorize(Roles="animation,admin,DJ")]
        [HttpGet("{id}")]
        public ActionResult GetPlaylists(int id)
        {
            Playlist playlistEntry = dbContext.KaraokePlaylists
                .Include(p => p.Request).ThenInclude(r => r.Song).ThenInclude(s => s.Artist)
                .Where(p => p.Id == id).FirstOrDefault();

            if (playlistEntry == null)
            {
                return BadRequest("Entry doesn't exist");
            }

            return Ok(playlistEntry);
        }

        //        [Authorize(Roles="animation,admin")]
        [HttpPut]
        public ActionResult ChangeOrder(int id, [FromBody] int pNewPosition)
        {
            Playlist request = dbContext.KaraokePlaylists.Where(s => s.Id == id).FirstOrDefault();

            if (pNewPosition < 0)
            {
                return BadRequest("Invalid position");
            }

            // Request desn't exist in playlist...  Return an error
            if (request == null)
            {
                return BadRequest("Request doesn't exist");
            }

            // Position cannot be greater than count play song
            int totalCount = dbContext.KaraokePlaylists.Where(p => p.IsDone == 0).Count();

            if (totalCount > 0)
            {
                totalCount = totalCount - 1;
            }
            if (pNewPosition > totalCount)
            {
                pNewPosition = totalCount;
            }

            int from = request.playOrder;
            int to = pNewPosition;
            List<Playlist> orderPlaylist = dbContext.KaraokePlaylists
                .Where(p => p.playOrder >= Math.Min(from, to)
                    && p.playOrder <= Math.Max(from, to)
                    && p.IsDone == 0)
                .OrderBy(p => p.playOrder).ToList();

            int position = Math.Min(from, to);
            foreach (Playlist song in orderPlaylist)
            {
                if (from < to)
                {
                    if (song.playOrder == from)
                    {
                        continue;
                    }
                    song.playOrder = position;
                    dbContext.KaraokePlaylists.Update(song);
                    position++;
                }
                else
                {
                    position++;
                    song.playOrder = position;
                    dbContext.KaraokePlaylists.Update(song);
                }
            }

            request.playOrder = pNewPosition;
            dbContext.KaraokePlaylists.Update(request);
            dbContext.SaveChanges();

            return Ok(request);
        }

        //        [Authorize(Roles="animation,admin")]
        [HttpPost]
        public ActionResult AddToPlaylist(int id, [FromBody] int pPosition)
        {
            Request request = dbContext.KaraokeRequests.Where(s => s.Id == id).FirstOrDefault();

            // Request desn't exist in playlist...  Return an error
            if (request == null)
            {
                return BadRequest("Request doesn't exist");
            }

            // Position cannot be greater than count play song + 1
            int totalCount = dbContext.KaraokePlaylists.Where(p => p.IsDone == 0).Count();

            if (pPosition < 0 || pPosition > totalCount)
            {
                pPosition = totalCount;
            }

            List<Playlist> orderPlaylist = dbContext.KaraokePlaylists
                .Where(p => p.playOrder >= pPosition && p.IsDone == 0)
                .OrderBy(p => p.playOrder).ToList();

            Playlist entry = new Playlist();
            entry.RequestId = id;
            entry.playOrder = pPosition;
            dbContext.KaraokePlaylists.Add(entry);

            foreach (Playlist song in orderPlaylist)
            {
                pPosition++;
                song.playOrder = pPosition;
            }
            dbContext.SaveChanges();

            entry = dbContext.KaraokePlaylists
                .Include(p => p.Request)
                .ThenInclude(r => r.Song)
                .ThenInclude(s => s.Artist)
                .Where(p => p.Id == entry.Id).FirstOrDefault();
            return Ok(entry);
        }

        //        [Authorize(Roles="admin,DJ,animation")]
        public ActionResult MarkAsDone(int id, [FromBody] string pDelete)
        {
            Playlist entry = dbContext.KaraokePlaylists
                .Include(p => p.Request)
                .Where(p => p.Id == id).FirstOrDefault();

            if (entry == null)
            {
                return BadRequest("Entry doesn't exist in plylist");
            }

            dbContext.KaraokePlaylists.Remove(entry);

            // a DELETE commad in the body indicates that We want to remove the request also, as the song was sang
            // Without a DELETE command is just a remove from Playlist (and put back in requests)
            if (pDelete != null && pDelete.Equals(Constants.DELETE_COMMAND))
            {
                dbContext.KaraokeRequests.Remove(entry.Request);
            }
            dbContext.SaveChanges();

            List<Playlist> orderPlaylist = dbContext.KaraokePlaylists
                .Where(p => p.playOrder >= entry.playOrder && p.IsDone == 0)
                .OrderBy(p => p.playOrder).ToList();

            int newOrder = entry.playOrder;
            foreach (Playlist playEntry in orderPlaylist)
            {
                playEntry.playOrder = newOrder;
                newOrder++;
                dbContext.KaraokePlaylists.Update(playEntry);
            }

            dbContext.SaveChanges();
            return Ok();
        }

        
        [HttpDelete]
        public ActionResult Delete(int id, String delete)
        {
            return MarkAsDone(id, delete);
        }
    }
}