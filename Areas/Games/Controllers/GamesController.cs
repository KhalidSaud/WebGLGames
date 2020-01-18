using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebGLGames.Data;
using WebGLGames.Models;

namespace WebGLGames.Areas.Games.Controllers
{

    [Area("Games")]
    //[Route("[controller]/[action]")]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GamesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //[Route("/[controller]")]
        public async Task<IActionResult> Index()
        {

            var games = await _db.Games.ToListAsync();

            return View(games);
        }


        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            _db.Games.Add(game);
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

        //[Route("{id:int?}")]

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
    }
}