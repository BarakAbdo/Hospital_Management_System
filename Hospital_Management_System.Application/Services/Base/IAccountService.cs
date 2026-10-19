using Hospital_Management_System.Domain.Models;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IAccountService
    {

        User GetByUsername(string username);
        void AddUser(User user);
        User Login(string username, string password);
    }
}
