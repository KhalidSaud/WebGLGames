using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebGLGames.Data;

namespace WebGLGames.Areas.Identity.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountExtrasController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountExtrasController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost][HttpGet]
        public async Task<IActionResult> IsEmailNotTaken([Bind(Prefix = "Input.Email")]string email)
        {

            var emailFromDb = await _userManager.FindByEmailAsync(email);

            if (emailFromDb != null)
            {
                return Json($"The email <strong>{email}</strong> is already registered");
            } else
            {
                return Json(true);
            }
        }
    }
}