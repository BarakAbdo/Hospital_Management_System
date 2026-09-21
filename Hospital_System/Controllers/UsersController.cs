 using Hospital_Management_System.Dtos;
using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Dtos.RolesDtos;
using Hospital_System.Dtos.UsersDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.UserRepo;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Hospital_Management_System.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        //private readonly AppDbContext _db;

        //public UsersController(AppDbContext db)
        //{
        //    _db = db;
        //}


        public IActionResult Index()
        {

            //IEnumerable<User> users = _repo.GetAll();
            var user = _repo.GetAll().Select(u => new UserDto
            {
                Id = u.Id,
                UID = u.UID,
                Name = u.Name,
                Email = u.Email,
                Username = u.Username
            }).ToList();


            return View(user);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(CreateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var use = new User
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Username = userDto.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
                };

                _repo.Add(use);
                _repo.Save();

                //_db.Users.Add(use);
                //_db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(userDto);
        }

        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(UpdateUserDto user)
        {

            var oldUser = _repo.GetById(user.Id);
            if (user.UID == null)
                user.UID = Guid.NewGuid().ToString();

            if (oldUser == null)
            {
                return NotFound();
            }

            oldUser.Name = user.Name;
            oldUser.Email = user.Email;
            oldUser.Username = user.Username;
            


            // Change password only if user entered a new password
            if (!string.IsNullOrEmpty(user.Password))
            {
                oldUser.PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            _repo.Update(oldUser);
            _repo.Save();
            //_db.SaveChanges();

            return RedirectToAction("Index");
        }

        private string UploadImage(IFormFile image) 
        {
        string fileName = Guid.NewGuid().ToString()
                + Path.GetExtension(image.FileName);

            string folderPath = Path.Combine(
           Directory.GetCurrentDirectory(),
           "wwwroot",
           "images",
           "users"
                );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(
                folderPath, 
                fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            { 
            image.CopyTo(stream);
            }
            return "/images/users/" + fileName;
        }

        // =========================
        // Delete
        // =========================
        [HttpPost]
        public IActionResult Delete(User user)
        {
            var oldUser = _repo.GetByUId(user.UID);
            if (oldUser == null)
            {
                return NotFound();

                //_db.Users.Remove(user);
                //_db.SaveChanges();
            }
            _repo.Delete(oldUser);
            _repo.Save();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult ManageRoles(string uid)
        {
            var user = _repo.GetByUId(uid);

            if (user == null)
            {
                return NotFound();
            }

            // جميع الصلاحيات
            var roles = _repo.Roles.ToList();

            // الصلاحيات الموجودة بالفعل للمستخدم
            var userRoleIds = _repo.RoleUsers
                .Where(x => x.UserId == user.Id)
                .Select(x => x.RoleId)
                .ToList();

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
            var user = _repo.GetById(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            
            var selectedRoleIds = model.Roles?
                .Where(r => r.IsSelected)
                .Select(r => r.RoleId)
                .ToList() ?? new List<int>();

            _repo.UpdateUserRoles(model.UserId, selectedRoleIds);
            _repo.Save();

            return RedirectToAction("Index");

           
        }

        private string UploadFiles(IFormFile file, string name)
        {
            if (file == null) return string.Empty;

            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "Files",
      "Users"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Users/" + fileName;
        }




        public IActionResult ManageFiles(string uid)
        {
            //var user = _repo.Users.FirstOrDefault(e => e.Id == userId);
            var user = _repo.GetByUId(uid);
            if (user == null)
                return NotFound();

            var files = _repo.UserFiles.Where(e => e.UserId == user.Id).ToList();
            ViewBag.UserName = user.Username;

            ViewBag.Files = files;

            UserFile userFile = new UserFile();

            userFile.UserId = user.Id;

            return View(userFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(UserFile userFile, IFormFile fileUser)
        {
            if (userFile != null)
            {
                {
                    userFile.FileURL = UploadFiles(fileUser, userFile.Name);
                    _repo.AddUserFile(userFile);
                    _repo.Save();
                }
            }

            //_repo.UserFiles.Add(userFile);
            //_repo.SaveChanges();
            var user = _repo.GetById(userFile.UserId);

            return RedirectToAction(nameof(ManageFiles), new { uid = user?.UID });

        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.UserFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteUserFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
