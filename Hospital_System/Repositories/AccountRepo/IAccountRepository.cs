using Hospital_Management_System.Models;

namespace Hospital_System.Repositories.AccountRepo
{
    public interface IAccountRepository
    {
        User? GetByUsername(string username);
        void Save();
    }
}
