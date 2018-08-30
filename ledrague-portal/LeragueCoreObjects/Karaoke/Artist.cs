using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeDragueCoreObjects.Karaoke
{
    public class Artist
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public List<Song> Songs { get; set; }
    }
}
