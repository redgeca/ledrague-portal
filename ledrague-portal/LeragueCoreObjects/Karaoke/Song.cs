using LeDragueCoreObjects.Converters;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeDragueCoreObjects.Karaoke
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Song
    {
        public int Id { get; set; }

        [JsonProperty("title")]
        public String Title { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [NotMapped]
        [JsonProperty("categories.name")]
        public string CategoryName { get; set; }

        [NotMapped]
        [JsonProperty("tags.name")]
        public String ArtistName { get; set; }

        [NotMapped]
        [JsonProperty("content")]
        public String Content { get; set; }
    }
}
