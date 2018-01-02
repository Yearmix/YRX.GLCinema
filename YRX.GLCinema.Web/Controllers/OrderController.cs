using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YRX.GLCinema.DatabaseAccess;
using YRX.GLCinema.DatabaseAccess.Repositories;

namespace YRX.GLCinema.Web.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IRepository<Movie> _movies;
        private readonly IRepository<Order> _orders;

        public OrderController() : this(new XmlRepository<Movie>(), new XmlRepository<Order>())
        { }

        public OrderController(IRepository<Movie> movies, IRepository<Order> orders)
        {
            _movies = movies;
            _orders = orders;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders.Items;
        }

        public Order Put(int id, Approval approval)
        {
            var order = _orders.Get(id);
            if (order.Approved == approval.Approved)
                return order;

            order.Approved = approval.Approved;
            if (order != null)
            {
                foreach (var ticket in order.Tickets)
                {
                    var movie = _movies.Get(ticket.MovieId);
                    var seat = movie.Seats.FirstOrDefault(x => x.Row == ticket.Row && x.SeetNumber == ticket.SeetNumber);
                    if (seat != null)
                    {
                        if (!seat.Available && approval.Approved)
                            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Order has unavailable seats!"));
                        seat.Available = !approval.Approved;
                    }
                    _movies.Update(movie);                    
                }
            }
            _orders.Update(order);
            return order;
        }

        public IEnumerable<Movie> Post(Order order)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            List<Movie> movies = new List<Movie>();
            foreach (var ticket in order.Tickets)
            {
                var movie = _movies.Get(ticket.MovieId);
                var seat = movie.Seats.FirstOrDefault(x => x.Row == ticket.Row && x.SeetNumber == ticket.SeetNumber);                
                if (seat != null)
                {
                    if (!seat.Available)
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Order has unavailable seats!"));
                    seat.Available = false;
                }
                _movies.Update(movie);
                movies.Add(movie);
            }
            order.Approved = true;
            _orders.Add(order);
            return movies;
        }        
    }
}
