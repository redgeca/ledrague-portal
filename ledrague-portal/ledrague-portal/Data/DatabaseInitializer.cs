using ledrague_portal.Data;
using ledrague_portal.Models;
using LeDragueCoreObjects.cia;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeDraguePortal.Data
{
    public class DatabaseInitializer
    {
        public static async Task seedInitialData(ApplicationDbContext pContext, UserManager<ApplicationUser> pUserManager,
            RoleManager<IdentityRole> pRoleManager)
        {
            List<Application> seedingApplications = new List<Application> {
                new Application { Name = "CIA", ApplicationPrefix = "drague.com.cia.",
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.cia.view.users", DisplayName = "Voir les utilisateurs" },
                        new ApplicationRight { Name = "drague.com.cia.edit.users", DisplayName = "Éditer les utilisateurs" },
                        new ApplicationRight { Name = "drague.com.cia.add.users", DisplayName = "Ajouter des utilisateurs" },
                        new ApplicationRight { Name = "drague.com.cia.delete.users", DisplayName = "Effacer des utilisateurs" },
                        new ApplicationRight { Name = "drague.com.cia.changepassword.users", DisplayName = "Changer les mots de passes" },
                        new ApplicationRight { Name = "drague.com.cia.manage.claims", DisplayName = "Administrer les droits des utilisateurs" },
                    }
                },
                new Application { Name = "Contrats", ApplicationPrefix = "drague.com.contracts.",
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.contracts.view.artists", DisplayName = "Voir les Artistes" },
                        new ApplicationRight { Name = "drague.com.contracts.view.contracts", DisplayName = "Voir les Contrats" },
                        new ApplicationRight { Name = "drague.com.contracts.view.templates", DisplayName = "Voir les Templates" },

                        new ApplicationRight { Name = "drague.com.contracts.add.artists", DisplayName = "Ajouter des Artistes" },
                        new ApplicationRight { Name = "drague.com.contracts.add.contracts", DisplayName = "Ajouter des Contrats" },
                        new ApplicationRight { Name = "drague.com.contracts.add.templates", DisplayName = "Ajouter des Templates" },

                        new ApplicationRight { Name = "drague.com.contracts.edit.artists", DisplayName = "Éditer les Artistes" },
                        new ApplicationRight { Name = "drague.com.contracts.edit.contracts", DisplayName = "Éditer les Contrats" },
                        new ApplicationRight { Name = "drague.com.contracts.edit.templates", DisplayName = "Éditer les Templates" },

                        new ApplicationRight { Name = "drague.com.contracts.delete.artists", DisplayName = "Effacer des Artistes" },
                        new ApplicationRight { Name = "drague.com.contracts.delete.contracts", DisplayName = "Effacer des Contrats" },
                        new ApplicationRight { Name = "drague.com.contracts.delete.templates", DisplayName = "Effacer des Templates" },

                    }
                },
                new Application { Name = "Karaoké", ApplicationPrefix = "drague.com.karaoke." ,
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.karaoke.view.requests", DisplayName = "Voir les demandes clients" },
                        new ApplicationRight { Name = "drague.com.karaoke.delete.requests", DisplayName = "Effacer des demandes clients" },

                        new ApplicationRight { Name = "drague.com.karaoke.view.playlist", DisplayName = "Voir la playlist" },
                        new ApplicationRight { Name = "drague.com.karaoke.delete.playlist", DisplayName = "Effacer de la playlist" },
                        new ApplicationRight { Name = "drague.com.karaoke.manage.playlist", DisplayName = "Gérer la playlist (Ajouter ou enlever de)" },
                    }
                },
            };


            foreach(Application application in seedingApplications)
            {
                Application newApplication = pContext.Applications.Where(a => a.Name == application.Name).FirstOrDefault();
                if (newApplication == null)
                {
                    pContext.Applications.Add(application);
                    await pContext.SaveChangesAsync();
                    newApplication = application;
                }

                foreach (ApplicationRight appRight in application.ApplicationRights)
                {
                    if (pContext.ApplicationRights.Where(r => r.Name == appRight.Name).FirstOrDefault() == null)
                    {
                        appRight.Application = newApplication;
                        pContext.ApplicationRights.Add(appRight);
                    }
                }
                await pContext.SaveChangesAsync();
            }

            await pUserManager.CreateAsync(new ApplicationUser { Email = "redgeca@gmail.com", UserName = "Redgeca" }, "Tourlou1!");

            if (! await pRoleManager.RoleExistsAsync("poweruser"))
            {
                await pRoleManager.CreateAsync(new IdentityRole { Name = "poweruser" });
            }
            await pContext.SaveChangesAsync();

            ApplicationUser redge = pUserManager.FindByNameAsync("Redgeca").Result;
            await pUserManager.AddToRoleAsync(redge, "poweruser");
            
            await pContext.SaveChangesAsync();
        }
    }
}
