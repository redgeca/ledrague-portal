using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeDragueCoreObjects.Karaoke
{
   public class CategorySong
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
