using Hospital_Management_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.AccountRepo
{
    public interface IAccountRepository : IRepository<User>
    {
        User? GetByUsername(string username);
        void Save();
    }
}
