using CinemaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaMVC.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var films = await _context.Films.ToListAsync();
            return View(films);
        }
        public IActionResult ReturnToHomePage()
        {
            return RedirectToAction("Index", "Films");
        }
        public async Task<IActionResult> Details(Film film) // Применяем Film вместо int id
        {
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
    }
}
