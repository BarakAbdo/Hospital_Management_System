using Hospital_Management_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Services.Base
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddUser(User user)
        {
            _unitOfWork.AccountRepo.Add(user); 
            _unitOfWork.Save();
        }


        public User GetByUsername(string username)
        {
            return _unitOfWork.AccountRepo.GetByUsername(username);
        }


        public User Login(string username, string password)
        {
            var user = _unitOfWork.AccountRepo.GetByUsername(username);

            if (user != null && !string.IsNullOrEmpty(user.PasswordHash) && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }


    }


}
