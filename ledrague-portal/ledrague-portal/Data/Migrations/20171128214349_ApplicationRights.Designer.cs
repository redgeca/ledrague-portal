﻿// <auto-generated />
using ledrague_portal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ledrague_portal.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171128214349_ApplicationRights")]
    partial class ApplicationRights
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ledrague_portal.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("LeDragueCoreObjects.cia.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("LeDragueCoreObjects.cia.ApplicationRight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationId");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationRights");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistName")
                        .IsRequired();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SSN")
                        .IsRequired();

                    b.Property<string>("StreetAddress1");

                    b.Property<string>("StreetAddress2");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("ContractArtists");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArtistId");

                    b.Property<decimal>("ContractAmount");

                    b.Property<int?>("ContractContentId");

                    b.Property<DateTime>("ContractDate");

                    b.Property<string>("ContractNumber");

                    b.Property<int?>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("ContractContentId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Contract_Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<string>("HashCode");

                    b.HasKey("Id");

                    b.ToTable("ContractContents");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("TemplateContent");

                    b.Property<string>("TemplateName");

                    b.HasKey("Id");

                    b.ToTable("ContractTemplates");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Token_Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContractId");

                    b.Property<string>("Token");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("ContractTokenValues");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("KaraokeArtists");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("KaraokeCategories");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Configuration", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("key");

                    b.Property<DateTime>("lastUpdateTime");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IsDone");

                    b.Property<int>("RequestId");

                    b.Property<int>("playOrder");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("KaraokePlaylists");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<DateTime>("RequestTime");

                    b.Property<string>("SingerName");

                    b.Property<int>("SongId");

                    b.HasKey("Id");

                    b.HasIndex("SongId");

                    b.ToTable("KaraokeRequests");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("CategoryId");

                    b.ToTable("KaraokeSongs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LeDragueCoreObjects.cia.ApplicationRight", b =>
                {
                    b.HasOne("LeDragueCoreObjects.cia.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Contract", b =>
                {
                    b.HasOne("LeDragueCoreObjects.Contracts.Artist", "Artist")
                        .WithMany("Contracts")
                        .HasForeignKey("ArtistId");

                    b.HasOne("LeDragueCoreObjects.Contracts.Contract_Content", "ContractContent")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractContentId");

                    b.HasOne("LeDragueCoreObjects.Contracts.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Contracts.Token_Value", b =>
                {
                    b.HasOne("LeDragueCoreObjects.Contracts.Contract", "Contract")
                        .WithMany("Token_Value")
                        .HasForeignKey("ContractId");
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Playlist", b =>
                {
                    b.HasOne("LeDragueCoreObjects.Karaoke.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Request", b =>
                {
                    b.HasOne("LeDragueCoreObjects.Karaoke.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeDragueCoreObjects.Karaoke.Song", b =>
                {
                    b.HasOne("LeDragueCoreObjects.Karaoke.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeDragueCoreObjects.Karaoke.Category", "Category")
                        .WithMany("Songs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ledrague_portal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ledrague_portal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ledrague_portal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ledrague_portal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
