using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

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
