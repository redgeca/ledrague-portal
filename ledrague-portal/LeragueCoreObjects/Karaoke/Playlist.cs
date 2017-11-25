using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeDragueCoreObjects.Karaoke
{
    public class Playlist
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int playOrder { get; set; }
        public int IsDone { get; set; }
    }
}
