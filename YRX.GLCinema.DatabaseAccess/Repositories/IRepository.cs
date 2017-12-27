using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

