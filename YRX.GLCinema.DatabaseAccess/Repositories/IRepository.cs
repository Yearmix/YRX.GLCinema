using System.Linq;

namespace YRX.GLCinema.DatabaseAccess.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Items { get; }

        T Get(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);               
    }
}

