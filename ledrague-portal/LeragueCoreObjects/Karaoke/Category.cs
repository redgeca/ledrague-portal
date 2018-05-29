using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeDragueCoreObjects.Karaoke
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public ICollection<CategorySong> CategorySongs { get; } = new List<CategorySong>();
    }
}
