﻿using CinemaMVC.Models;
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
                // Вернуть представление с сообщением об ошибке, например:
                ModelState.AddModelError("", "Введите имя и пароль.");
                return View(); // Представление с формой логина
            }

            // Здесь ваши проверки логина и пароля
            // ...

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, login),
        // Добавьте другие необходимые клеймы
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Films"); // Редирект после успешной аутентификации
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
