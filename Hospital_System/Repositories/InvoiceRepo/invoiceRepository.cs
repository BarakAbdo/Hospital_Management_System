using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.InvoiceRepo
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Invoice> _dbSet;

        public InvoiceRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Invoice>();
        }

        public IEnumerable<Invoice> Invoices => _db.Invoices.Include(i => i.Patients).ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<InvoiceFile> InvoiceFiles => _db.InvoiceFiles.ToList();

        public IEnumerable<Invoice> GetAll()
        {
            return _dbSet.Include(i => i.Patients).ToList();
        }

        //public IEnumerable<Invoice> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        public Invoice? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Invoice? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Add(Invoice invoice)
        {
            _dbSet.Add(invoice);
        }

        public void Update(Invoice invoice)
        {
            _dbSet.Update(invoice);
        }

        public void Delete(Invoice invoice)
        {
            _dbSet.Remove(invoice);
        }

        public void AddFile(InvoiceFile invoiceFile)
        {
            _db.InvoiceFiles.Add(invoiceFile);
        }

        public void DeleteInvoiceFile(InvoiceFile invoiceFile)
        {
            _db.InvoiceFiles.Remove(invoiceFile);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}