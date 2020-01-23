using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebGLGames.Data;
using WebGLGames.Models;
using WebGLGames.Models.ViewModels;

namespace WebGLGames.Areas.Games.Controllers
{

    [Area("Games")]
    [Route("[controller]/[action]")]
    public class GamesController : Controller
    {

        private readonly ApplicationDbContext _db;
        public GamesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string StatusMessage { set; get; }
        [TempData]
        public string GameName { set; get; }
        [TempData]
        public string GameRN { set; get; }
        [TempData]
        public string GameRNJ { set; get; }

        [Authorize]
        [Route("/[controller]")]
        public async Task<IActionResult> Index()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier);
            List<Game> games = new List<Game>();

            if (1 != 0)
            {
                games = await _db.Games.Include(u => u.User).Where(i => i.UserId == userID.Value).ToListAsync();
            }
            else
            {
                games = await _db.Games.Include(g => g.User).ToListAsync();
            }

            return View(games);
        }


        [Authorize]
        public IActionResult Manage()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            GameStatusMessageViewModel model = new GameStatusMessageViewModel();

            model.StatusMessage = StatusMessage;

            model.Game = new Game()
            {
                UserId = userId.Value,
                Name = GameName,
                ReleaseName = GameRN,
                ReleaseNameDotJson = GameRNJ,

            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameStatusMessageViewModel model)
        {

            
            if (!ModelState.IsValid)
            {
                //return NotFound();
                return RedirectToAction(nameof(Manage));
            }

            // Testing validating Name

            //var NameFromDb = _db.Games.FirstOrDefault(n => n.Name == model.Game.Name);

            //if (NameFromDb != null)
            //{
            //    StatusMessage = "Error: Name not vaild";
            //    model.StatusMessage = StatusMessage;
            //    GameName = model.Game.Name;
            //    GameRN = model.Game.ReleaseName;
            //    GameRNJ = model.Game.ReleaseNameDotJson;
            //    return RedirectToAction(nameof(Manage));
            //}

            //

            var userID = User.FindFirst(ClaimTypes.NameIdentifier);

            model.Game.UserId = userID.Value;

            var UserFromDB = await _db.IdentityUsers.Include(g => g.Games).FirstOrDefaultAsync(u => u.Id == userID.Value);
            UserFromDB.Games.Add(model.Game);
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var gameToEdit = await _db.Games.FindAsync(id);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            return View(gameToEdit);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(Game game)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }


            var gameToEdit = await _db.Games.FindAsync(game.Id);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            gameToEdit.Name = game.Name;
            gameToEdit.ReleaseName = game.ReleaseName;
            gameToEdit.ReleaseNameDotJson = game.ReleaseNameDotJson;

            _db.Games.Update(gameToEdit);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var gameToDelete = await _db.Games.FindAsync(id);

            if(gameToDelete == null)
            {
                return NotFound();
            }

            _db.Games.Remove(gameToDelete);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Produces("application/json")]
        [HttpGet]
        [Route("{name}")]
        public IActionResult CheckName(string name)
        {
            var NameFromDb = _db.Games.FirstOrDefault(n => n.Name == name);

            if (NameFromDb == null)
            {
                var status = new { freeName = true };
                return Json(status);
            }
            else
            {
                var status = new { freeName = false };
                return Json(status);
            }
        }

        [HttpPost][HttpGet]
        public async Task<IActionResult> CheckName2([Bind(Prefix = "Game.Name")]string name)
        {

            var nameFromDb = await _db.Games.FirstOrDefaultAsync(u => u.Name == name);

            if (nameFromDb != null)
            {
                return Json(name + " is already taken");
            } else
            {
                return Json(true);
            }
            
        }

    }
}