using CinemaMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            Console.WriteLine("LOGIN ALO");
            var films = await _context.Films.ToListAsync();
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                ViewBag.Username = username;
            }
            return View(films);
        }
        public IActionResult ReturnToHomePage()
        {
            return RedirectToAction("Index", "Films");
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            Console.WriteLine(login, password); 
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                
                ModelState.AddModelError("", "Введите имя и пароль.");
                return View(); 
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, login),
        
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Films"); 
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Films"); 
        }
        public async Task<IActionResult> Details(Film film) 
        {
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
    }
}
