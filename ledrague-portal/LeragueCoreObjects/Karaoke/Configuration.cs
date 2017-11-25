using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeDragueCoreObjects.Karaoke
{
    public class Configuration
    {
        public int id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public DateTime lastUpdateTime { get; set; }
    }
}
