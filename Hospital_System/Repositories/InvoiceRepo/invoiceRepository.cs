using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.InvoiceRepo
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Invoice> _dbSet;

        public InvoiceRepository(AppDbContext db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<Invoice>();
        }

        public IEnumerable<Invoice> Invoices => _db.Invoices.Include(i => i.Patients).ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<InvoiceFile> InvoiceFiles => _db.InvoiceFiles.ToList();

        public IEnumerable<Invoice> GetAllInvo()
        {
            return _dbSet.Include(i => i.Patients).ToList();
        }

        //public IEnumerable<Invoice> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        public Invoice? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

       

        public void AddFile(InvoiceFile invoiceFile)
        {
            _db.InvoiceFiles.Add(invoiceFile);
        }

        public void DeleteInvoiceFile(InvoiceFile invoiceFile)
        {
            _db.InvoiceFiles.Remove(invoiceFile);
        }

    }
}