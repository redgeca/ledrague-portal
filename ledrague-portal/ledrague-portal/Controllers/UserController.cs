using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ledrague_portal.Models;
using Microsoft.Extensions.Logging;

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

    }
}
