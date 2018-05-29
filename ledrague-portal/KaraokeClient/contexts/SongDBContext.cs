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

            modelBuilder.Entity<CategorySong>().HasKey(t => new { t.CategoryId, t.SongId });

            modelBuilder.Entity<CategorySong>().HasOne(cs => cs.Category)
                .WithMany(cs => cs.CategorySongs)
                .HasForeignKey(cs => cs.CategoryId);

            modelBuilder.Entity<CategorySong>().HasOne(cs => cs.Song)
                .WithMany(cs => cs.CategorySongs)
                .HasForeignKey(cs => cs.SongId);
        }

        public DbSet<LeDragueCoreObjects.Karaoke.Artist> KaraokeArtists { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Category> KaraokeCategories { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Configuration> Configurations { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Playlist> KaraokePlaylists { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Request> KaraokeRequests { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Song> KaraokeSongs { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.CategorySong> KaraokeCategorySongs { get; set; }
    }
}
