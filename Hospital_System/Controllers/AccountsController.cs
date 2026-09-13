using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AppDbContext _db;

        public AccountsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            //Hash the provided password and compare it with the stored hash

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {

                // User authenticated successfully
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed

                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
    }
}
