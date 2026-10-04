namespace Hospital_Management_System.Infrastructure.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T? GetById(int id);


        void Add(T entity);

        void Update(T entity);


        void Delete(T entity);

        void Save();
    }
}
