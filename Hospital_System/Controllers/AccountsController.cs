using Hospital_System.Data;
using Hospital_System.Repositories.AccountRepo;
using Hospital_System.Repositories.Base;
using Hospital_System.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    public class AccountsController : Controller
    {

        //Service
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            var user = _accountService.Login(username, password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
    }
}
