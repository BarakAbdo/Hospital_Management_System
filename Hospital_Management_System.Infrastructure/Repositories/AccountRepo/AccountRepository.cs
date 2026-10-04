using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using System.Linq;

namespace Hospital_Management_System.Infrastructure.Repositories.AccountRepo
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }
    }

}

