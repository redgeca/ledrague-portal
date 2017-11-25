using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ledrague_portal.Models;
using LeDragueCoreObjects;
using LeDragueCoreObjects.Contracts;

namespace ledrague_portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        DbSet<LeDragueCoreObjects.Contracts.Artist> ContractArtists { get; set; }
        DbSet<LeDragueCoreObjects.Contracts.Contract> Contracts { get; set; }
        DbSet<LeDragueCoreObjects.Contracts.Contract_Content> ContractContents { get; set; }
        DbSet<LeDragueCoreObjects.Contracts.Template> ContractTemplates{ get; set; }
        DbSet<LeDragueCoreObjects.Contracts.Token_Value> ContractTokenValues { get; set; }
        DbSet<LeDragueCoreObjects.Karaoke.Artist> KaraokeArtists { get; set; }
        DbSet<LeDragueCoreObjects.Karaoke.Category> KaraokeCategories { get; set; }
        DbSet<LeDragueCoreObjects.Karaoke.Configuration> Configurations { get; set; }
        DbSet<LeDragueCoreObjects.Karaoke.Playlist> KaraokePlaylists { get; set; }
        DbSet<LeDragueCoreObjects.Karaoke.Request> KaraokeRequests{ get; set; }
        DbSet<LeDragueCoreObjects.Karaoke.Song> KaraokeSongs { get; set; }

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
