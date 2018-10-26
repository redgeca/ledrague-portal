using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeDraguePortal.Hubs
{
    public class MessageHub : Hub
    {
        public Task newRequest(String pTitle, String pName)
        {
            return Clients.All.SendAsync("newRequest", pTitle, pName);
        }
        public Task reloadRequests()
        {
            return Clients.All.SendAsync("reloadRequests");
        }

        public Task reloadPlaylist()
        {
            return Clients.All.SendAsync("reloadPlaylist");
        }
    }
}
