using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Repositories.Base;
using System.Linq;

namespace Hospital_System.Repositories.AccountRepo
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

