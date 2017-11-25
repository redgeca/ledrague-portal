using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeDragueCoreObjects.Karaoke
{
    public class Request { 
        public int Id { get; set; }

        public int SongId { get; set; }
        public Song Song { get; set; }

        public String SingerName { get; set; }

        public DateTime RequestTime { get; set; }

        public String Notes { get; set; }
    }
}
