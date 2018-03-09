using ledrague_portal.Models;
using LeDragueCoreObjects.cia;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeDraguePortal.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger logger;

        public UserController(UserManager<ApplicationUser> pUserManager,
            SignInManager< ApplicationUser > pSignInManager,
            ILogger< UserController > pLogger)
        {
            userManager = pUserManager;
            signInManager = pSignInManager;
            logger = pLogger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            return userManager.Users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ApplicationUser> Get(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<IdentityResult> post([FromBody] FormUser pUser)
        {
            ApplicationUser newUser = new ApplicationUser();
            newUser.UserName = pUser.username;
            newUser.Email = pUser.email;
            if (pUser.password != null && !String.Empty.Equals(pUser.password.Trim()))
            {
                return await userManager.CreateAsync(newUser, pUser.password.Trim());

            }
            return await userManager.CreateAsync(newUser);
        } 
    }
}
