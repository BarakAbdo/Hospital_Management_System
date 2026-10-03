using Hospital_Management_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IAccountService
    {

        User GetByUsername(string username);
        void AddUser(User user);
        User Login(string username, string password);
    }
}
