

using Hospital_Management_System.Application.Dtos;
using Hospital_Management_System.Application.Dtos.UsersDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Mvc;


namespace Hospital_Management_System.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }


        [HttpPost]
        public IActionResult Create(CreateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(userDto);
                return RedirectToAction("Index");
            }
            return View(userDto);
        }

        
        [HttpPost]
        public IActionResult Edit(UpdateUserDto user)
        {

            if (ModelState.IsValid)
            {
                var existingUser = _userService.GetUserById(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

       


        [HttpPost]
        public IActionResult Delete(User user)
        {
            var oldUser = _userService.GetUserByUId(user.UID);
            if (oldUser == null)
            {
                return NotFound();
            }
            _userService.DeleteUser(user.UID);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult ManageRoles(string uid)
        {
            var user = _userService.GetUserByUId(uid);
            if (user == null)
            {
                return NotFound();
            }


            var roles = _userService.GetAllRoles();
            var userRoleIds = _userService.GetUserRoleIds(user.Id);

            var model = new UserRolesVM
            {
                UserId = user.Id,
                UserName = user.Name,
                Roles = roles.Select(role => new RoleCheckVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = userRoleIds.Contains(role.Id)
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult ManageRoles(UserRolesVM model)
        {
            var user = _userService.GetUserById(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var selectedRoleIds = model.Roles?
                .Where(r => r.IsSelected)
                .Select(r => r.RoleId)
                .ToList() ?? new List<int>();

            _userService.UpdateUserRoles(model.UserId, selectedRoleIds);

            return RedirectToAction("Index");
        }

       

        public IActionResult ManageFiles(string uid)
        {
            var user = _userService.GetUserByUId(uid);
            if (user == null)
                return NotFound();

            var files = _userService.GetUserFiles(user.Id);

            ViewBag.UserName = user.Username;
            ViewBag.Files = files;

            UserFile userFile = new UserFile
            {
                UserId = user.Id
            };

            return View(userFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(UserFile userFile, IFormFile fileUser)
        {
            if (userFile != null && fileUser != null)
            {
                _userService.AddUserFile(userFile, fileUser);
            }
            var user = _userService.GetUserById(userFile.UserId);
            return RedirectToAction(nameof(ManageFiles), new { uid = user?.UID });
        }


        public IActionResult DeleteFile(int id, string uid)
        {
            _userService.DeleteUserFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
