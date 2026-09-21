using Hospital_Management_System.Models;
using Hospital_System.Data;
using System.Linq;

namespace Hospital_System.Repositories.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}

