using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.AccountRepo
{
    public interface IAccountRepository : IRepository<User>
    {
        User? GetByUsername(string username);
        void Save();
    }
}
