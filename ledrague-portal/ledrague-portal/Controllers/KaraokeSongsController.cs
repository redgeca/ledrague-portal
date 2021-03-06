﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ledrague_portal.Data;
using LeDraguePortal.utils;
using LeDragueCoreObjects.Karaoke;
using LeDragueCoreObjects.lucene;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LeDragueCoreObjects.misc;
using Microsoft.AspNetCore.SignalR;
using LeDraguePortal.Hubs;

namespace leDraguePortal.Controllers
{
    [Produces("application/json", "text/plain")]
    [Route("api/KaraokeSongs")]
    public class KaraokeSongsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHubContext<MessageHub> hubContext;

        public KaraokeSongsController(ApplicationDbContext pDbContext, IHubContext<MessageHub> pHubContext)
        {
            dbContext = pDbContext;
            hubContext = pHubContext;
        }

        // GET: api/KaraokeState
        [HttpGet]
        public async Task<IActionResult> Get(String filter, String orderBy, int? page, int? pageSize,
            int? artistId, int? categoryId)
        {
            IQueryable<Song> songs = dbContext.KaraokeSongs
                   .Include(cs => cs.Artist);
//                   .Include(cs => cs.CategorySongs)
//                   .ThenInclude(cat => cat.Category);

            if (filter != null && !filter.Equals("null"))
            {
                Searcher searcher = new Searcher();
                songs = songs.Where(s => searcher.searchTitles(filter).Contains(s.Id));
            }

            if (artistId != null)
            {
                songs = songs.Where(a => a.ArtistId == artistId);
            }

            switch (orderBy)
            {
                case "title_desc":
                    songs = songs.OrderByDescending(cs => cs.Title).ThenBy(cs => cs.Artist.Name);
                    break;

                case "artist_asc":
                    songs = songs.OrderBy(cs => cs.Artist.Name).ThenBy(cs => cs.Title);
                    break;

                case "artist_desc":
                    songs = songs.OrderByDescending(cs => cs.Artist.Name).ThenBy(cs => cs.Title);
                    break;

                default:
                    songs = songs.OrderBy(cs => cs.Title).ThenBy(cs => cs.Artist.Name);
                    break;
            }


            PaginatedList<Song> resultSongs = await PaginatedList<Song>.CreateAsync(songs.AsNoTracking(), page ?? 1, pageSize ?? 25);

            Request.HttpContext.Response.Headers.Add("X-Total-Count", resultSongs.TotalItems.ToString());
            return new JsonResult(resultSongs);
            
        }

        // GET: api/KaraokeSongs/{id}
        [HttpGet("{id}")]
        public ActionResult Get(Int32 id)
        {
            var song = dbContext.KaraokeSongs
                .Include(s => s.Artist)
                .Include(s => s.CategorySongs).ThenInclude(cs => cs.Category).Where(s => s.Id == id)
                .OrderBy(cs => cs.CategorySongs.Select(c => c.Category.Name)).FirstOrDefault();

            if (song == null)
            {
                return BadRequest("Invalid Song");
            }
            return new JsonResult(song);
        }

        [HttpGet("search")]
        public ActionResult search(String filter)
        {
            if (filter != null && !filter.Equals("null"))
            {
                Searcher searcher = new Searcher();
                return Json(searcher.KeywordSearch(filter));
            }

            return BadRequest("Filter cannot be null of empty");
        }

        [HttpGet("searchArtist")]
        public ActionResult searchArtist(String artist)
        {
            Searcher searcher = new Searcher();
            return Json(searcher.searchArtistName(artist));
        }

        [HttpGet("searchByArtist")]
        public ActionResult search(String artist, String filter)
        {
            Searcher searcher = new Searcher();
            return Json(searcher.KeywordSearch(artist, filter));
        }

        [HttpPost("request")]
        public ActionResult request(int id, String singer, String notes)
        {
            Configuration state = dbContext.Configurations.Where(c => c.key == Constants.KARAOKE_STATE_FLAG).FirstOrDefault();

            if (state.value.Equals(Constants.STOPPED_FLAG)) {
                return BadRequest("Le karaoké n'est pas en fonction.  Vous ne pouvez pas demander de chansons pour le moment.");
            }
            try
            {
                Song song = (Song)dbContext.KaraokeSongs.Where(s => s.Id == id).First();
                if (song != null)
                {
                    Request request = new Request();
                    request.SingerName = singer;
                    request.SongId = song.Id;
                    request.Notes = notes;
                    request.RequestTime = DateTime.Now;
                    dbContext.KaraokeRequests.Add(request);
                    dbContext.SaveChanges();
                }
                hubContext.Clients.All.SendAsync("newRequest", song.Title, singer, notes);
                hubContext.Clients.All.SendAsync("reloadRequests");

            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("La chanson demandée d'existe pas");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("La chanson demandée d'existe pas");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("La chanson demandée d'existe pas");
            }

            return Json("Demande effectuée à " + String.Format("{0:HH:mm:ss}", DateTime.Now));
        }

    }
}