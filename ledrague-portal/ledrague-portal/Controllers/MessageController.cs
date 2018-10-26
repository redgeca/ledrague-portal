using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeDraguePortal.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace leDraguePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller { 

        private IHubContext<MessageHub> hubContext;
    
        public MessageController(IHubContext<MessageHub> pHubContext) {
            hubContext = pHubContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            hubContext.Clients.All.SendAsync("reloadRequests");
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            hubContext.Clients.All.SendAsync("reloadPlaylist");
            return Ok();
        }
    }
}