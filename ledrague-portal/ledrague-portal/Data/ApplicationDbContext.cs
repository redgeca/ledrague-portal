using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ledrague_portal.Models;
using LeDragueCoreObjects.Karaoke;

namespace ledrague_portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<LeDragueCoreObjects.Contracts.Artist> ContractArtists { get; set; }
        public DbSet<LeDragueCoreObjects.Contracts.Contract> Contracts { get; set; }
        public DbSet<LeDragueCoreObjects.Contracts.Contract_Content> ContractContents { get; set; }
        public DbSet<LeDragueCoreObjects.Contracts.Template> ContractTemplates{ get; set; }
        public DbSet<LeDragueCoreObjects.Contracts.Token_Value> ContractTokenValues { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Artist> KaraokeArtists { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Category> KaraokeCategories { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Configuration> Configurations { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Playlist> KaraokePlaylists { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Request> KaraokeRequests{ get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.Song> KaraokeSongs { get; set; }
        public DbSet<LeDragueCoreObjects.Karaoke.CategorySong> KaraokeCategorySongs { get; set; }

        public DbSet<LeDragueCoreObjects.cia.Application> Applications { get; set; }
        public DbSet<LeDragueCoreObjects.cia.ApplicationRight> ApplicationRights { get; set; }
        public DbSet<LeDragueCoreObjects.cia.Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategorySong>().HasKey(t => new { t.CategoryId, t.SongId });

            builder.Entity<CategorySong>().HasOne(cs => cs.Category)
                .WithMany(cs => cs.CategorySongs)
                .HasForeignKey(cs => cs.CategoryId);

            builder.Entity<CategorySong>().HasOne(cs => cs.Song)
                .WithMany(cs => cs.CategorySongs)
                .HasForeignKey(cs => cs.SongId);
        }
    }
}
