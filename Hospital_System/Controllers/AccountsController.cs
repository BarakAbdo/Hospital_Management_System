using Hospital_System.Data;
using Hospital_System.Repositories.AccountRepo;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _repo;
        public AccountsController(IAccountRepository repo)
        {
            _repo = repo;
        }

        //private readonly AppDbContext _db;

        //public AccountsController(AppDbContext db)
        //{
        //    _db = db;
        //}

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _repo.GetByUsername(username);
            //var user = _db.Users.FirstOrDefault(u => u.Username == username);
            //Hash the provided password and compare it with the stored hash

            if (user != null && !string.IsNullOrEmpty(user.PasswordHash) && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
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
