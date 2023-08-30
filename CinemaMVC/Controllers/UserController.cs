using CinemaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Проверяем, нет ли уже пользователя с таким логином
                if (_context.Users.Any(u => u.Logins == user.Logins))
                {
                    ModelState.AddModelError("Logins", "Пользователь с таким логином уже существует.");
                    return View(user);
                }

                if (user.Passwords != confirmPassword)
                {
                    ModelState.AddModelError("Passwords", "Пароли не совпадают.");
                    return View(user);
                }
                user.SetPassword(user.Passwords); // Хешируем пароль перед сохранением
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Films"); // Измените на нужный редирект
            }

            return View(user);
        }
    }
}
