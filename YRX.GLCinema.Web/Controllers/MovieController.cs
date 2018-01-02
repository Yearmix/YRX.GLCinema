using System.Collections.Generic;
using System.Web.Http;
using YRX.GLCinema.DatabaseAccess;
using YRX.GLCinema.DatabaseAccess.Repositories;

namespace YRX.GLCinema.Web.Controllers
{
    public class MovieController : ApiController
    {
        private IRepository<Movie> _movies;

        public MovieController() : this(new XmlRepository<Movie>())
        {
        }

        public MovieController(IRepository<Movie> repository)
        {
            _movies = repository;
        }

        public IEnumerable<Movie> Get()
        {
            return _movies.Items;
        }
    }
}
