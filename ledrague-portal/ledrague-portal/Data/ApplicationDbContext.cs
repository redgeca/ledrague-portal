using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ledrague_portal.Models;
using LeDragueCoreObjects.cia;

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

        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationRight> ApplicationRights { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
