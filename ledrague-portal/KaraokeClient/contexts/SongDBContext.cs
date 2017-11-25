using LeDragueCoreObjects.Karaoke;
using LeDragueCoreObjects.misc;
using Microsoft.EntityFrameworkCore;

namespace KaraokeClient.contexts

{
    class SongDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.CONNECTION_STRING, 
                b => b.MigrationsAssembly("DBSetupAndDataSeed"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
        }

        public DbSet<LeDragueCoreObjects.Karaoke.Artist> KaraokeArtists { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Category> KaraokeCategories { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Configuration> Configurations { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Playlist> KaraokePlaylists { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Request> KaraokeRequests { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Song> KaraokeSongs { get; set; }
    }
}
