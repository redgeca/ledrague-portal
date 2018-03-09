using ledrague_portal.Data;
using ledrague_portal.Models;
using LeDragueCoreObjects.cia;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeDraguePortal.Data
{
    public class DatabaseInitializer
    {
        public static async Task seedInitialData(ApplicationDbContext pContext, UserManager<ApplicationUser> pUserManager,
            RoleManager<IdentityRole> pRoleManager)
        {
            Category create = new Category { Name = "create", DisplayName = "Créer" };
            Category read = new Category { Name = "read", DisplayName = "Voir" };
            Category update = new Category { Name = "update", DisplayName = "Modifier" };
            Category delete = new Category { Name = "delete", DisplayName = "Effacer" };
            Category other = new Category { Name = "other", DisplayName = "Autres" };

            List<Category> categories = new List<Category>
            {
                create, read, update, delete, other
            };

            foreach (Category category in categories)
            {
                Category newCategory = pContext.Categories.Where(c => c.Name.Equals(category.Name)).FirstOrDefault();
                if (newCategory == null)
                {
                    pContext.Categories.Add(category);
                    await pContext.SaveChangesAsync();
                }
            }

            List<Application> seedingApplications = new List<Application> {
                new Application { Name = "CIA", ApplicationPrefix = "drague.com.cia.",
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.cia.view.users", DisplayName = "Voir les utilisateurs", Category = read },
                        new ApplicationRight { Name = "drague.com.cia.edit.users", DisplayName = "Éditer les utilisateurs", Category = update },
                        new ApplicationRight { Name = "drague.com.cia.add.users", DisplayName = "Ajouter des utilisateurs", Category = create },
                        new ApplicationRight { Name = "drague.com.cia.delete.users", DisplayName = "Effacer des utilisateurs", Category = delete },
                        new ApplicationRight { Name = "drague.com.cia.changepassword.users", DisplayName = "Changer les mots de passes", Category = other },
                        new ApplicationRight { Name = "drague.com.cia.manage.claims", DisplayName = "Administrer les droits des utilisateurs", Category = other },
                    }
                },
                new Application { Name = "Contrats", ApplicationPrefix = "drague.com.contracts.",
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.contracts.view.artists", DisplayName = "Voir les Artistes", Category = read },
                        new ApplicationRight { Name = "drague.com.contracts.view.contracts", DisplayName = "Voir les Contrats", Category = read },
                        new ApplicationRight { Name = "drague.com.contracts.view.templates", DisplayName = "Voir les Templates",  Category = read },

                        new ApplicationRight { Name = "drague.com.contracts.add.artists", DisplayName = "Ajouter des Artistes", Category = create},
                        new ApplicationRight { Name = "drague.com.contracts.add.contracts", DisplayName = "Ajouter des Contrats", Category = create  },
                        new ApplicationRight { Name = "drague.com.contracts.add.templates", DisplayName = "Ajouter des Templates", Category = create  },

                        new ApplicationRight { Name = "drague.com.contracts.edit.artists", DisplayName = "Éditer les Artistes", Category = update  },
                        new ApplicationRight { Name = "drague.com.contracts.edit.contracts", DisplayName = "Éditer les Contrats", Category = update  },
                        new ApplicationRight { Name = "drague.com.contracts.edit.templates", DisplayName = "Éditer les Templates", Category = update  },

                        new ApplicationRight { Name = "drague.com.contracts.delete.artists", DisplayName = "Effacer des Artistes", Category = delete  },
                        new ApplicationRight { Name = "drague.com.contracts.delete.contracts", DisplayName = "Effacer des Contrats", Category = delete  },
                        new ApplicationRight { Name = "drague.com.contracts.delete.templates", DisplayName = "Effacer des Templates", Category = delete  },

                    }
                },
                new Application { Name = "Karaoké", ApplicationPrefix = "drague.com.karaoke." ,
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.karaoke.view.requests", DisplayName = "Voir les demandes clients", Category = read  },
                        new ApplicationRight { Name = "drague.com.karaoke.delete.requests", DisplayName = "Effacer des demandes clients", Category = delete  },

                        new ApplicationRight { Name = "drague.com.karaoke.view.playlist", DisplayName = "Voir la playlist", Category = read },
                        new ApplicationRight { Name = "drague.com.karaoke.delete.playlist", DisplayName = "Effacer de la playlist", Category = delete },
                        new ApplicationRight { Name = "drague.com.karaoke.manage.playlist", DisplayName = "Gérer la playlist (Ajouter ou enlever de)", Category = other },
                    }
                },
                new Application { Name = "Réservation des loges", ApplicationPrefix = "drague.com.reservation." ,
                    ApplicationRights = new List<ApplicationRight>
                    {
                        new ApplicationRight { Name = "drague.com.reservation.add.reservation", DisplayName = "Réserver les loges", Category = create  },
                        new ApplicationRight { Name = "drague.com.reservation.update.reservation", DisplayName = "Approuver une réservation", Category = update },
                        new ApplicationRight { Name = "drague.com.reservation.delete.ownreservation", DisplayName = "Effacer ses réservations", Category = delete },
                        new ApplicationRight { Name = "drague.com.reservation.delete.allreservation", DisplayName = "Effacer des réservations", Category = other },
                    }
                },
            };

            foreach (Application application in seedingApplications)
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

            await pUserManager.CreateAsync(new ApplicationUser { Email = "redgeca@gmail.com", UserName = "Redgeca" });

            if (!await pRoleManager.RoleExistsAsync("poweruser"))
            {
                await pRoleManager.CreateAsync(new IdentityRole { Name = "poweruser" });
            }
            await pContext.SaveChangesAsync();

            ApplicationUser redge = pUserManager.FindByNameAsync("Redgeca").Result;
            await pUserManager.AddToRoleAsync(redge, "poweruser");

            var claims = await pUserManager.GetClaimsAsync(redge);
            bool found = false;
            foreach(Claim claim in claims)
            {
                if (claim.Type.ToString().Equals("isPowerUser"))
                {
                    found = true;
                    break;
                }
            }
            if (!found) {
                await pUserManager.AddClaimAsync(redge, new Claim("isPowerUser", "true"));
            }

            await pContext.SaveChangesAsync();
        }
    }
}
