using System;
using System.Collections.Generic;
using YRX.GLCinema.DatabaseAccess;
using YRX.GLCinema.DatabaseAccess.Repositories;

namespace YRX.GLCinema.Fulfilment
{
    class Program
    {
        static void Main(string[] args)
        {
            //PopulateTestMovie();
            //PopulateTestTicket();
            //PopulateTestOrder();
            //PopulateTestHall();

            RetrieveMovie();
            RetrieveTicket();
            RetrieveOrder();
            RetrieveHall();

            Console.ReadKey();
        }

        public static void RetrieveMovie()
        {
            var repo = new XmlRepository<Movie>();
            foreach (var repoItem in repo.Items)
            {
                Console.WriteLine(repoItem);
            }
        }
        public static void RetrieveTicket()
        {
            var repo = new XmlRepository<Ticket>();
            foreach (var repoItem in repo.Items)
            {
                Console.WriteLine(repoItem);
            }
        }
        public static void RetrieveOrder()
        {
            var repo = new XmlRepository<Order>();
            foreach (var repoItem in repo.Items)
            {
                Console.WriteLine(repoItem);
            }
        }
        public static void RetrieveHall()
        {
            var repo = new XmlRepository<Hall>();
            foreach (var repoItem in repo.Items)
            {
                Console.WriteLine(repoItem);
            }
        }

        public static void PopulateTestHall()
        {
            var hallRepository = new XmlRepository<Hall>();

            var hall = new Hall {Id = 1};
            hallRepository.Add(hall);

            var hall1 = new Hall {Id = 2};

            hallRepository.Add(hall1);

            hallRepository.Delete(hall.Id);
        }
        public static void PopulateTestOrder()
        {
            var orderRepository = new XmlRepository<Order>();

            var order = new Order
            {
                Id = 1,
                Approved = true,
                Tickets = new List<Ticket>()
                {
                    new Ticket() {Id = 1, MovieId = 2, Row = 1, SeetNumber = 1},
                    new Ticket() {Id = 2, MovieId = 2, Row = 4, SeetNumber = 4},
                    new Ticket() {Id = 3, MovieId = 2, Row = 5, SeetNumber = 5}
                },
                CustomerEmail = "name@domain.com",
                CustomerName = "John Dou",
                OrderTotal = 50
            };

            orderRepository.Add(order);

            var order1 = new Order
            {
                Id = 2,
                Approved = true,
                Tickets = new List<Ticket>()
                {
                    new Ticket() {Id = 1, MovieId = 2, Row = 6, SeetNumber = 1},
                    new Ticket() {Id = 2, MovieId = 2, Row = 7, SeetNumber = 3},
                    new Ticket() {Id = 3, MovieId = 2, Row = 8, SeetNumber = 5}
                },
                CustomerEmail = "name@domain.com",
                CustomerName = "John Dou",
                OrderTotal = 50
            };

            orderRepository.Add(order1);

            orderRepository.Delete(order.Id);
        }
        public static void PopulateTestMovie()
        {
            var movieRepository = new XmlRepository<Movie>();

            var movie = new Movie();
            movie.Id = 1;
            movie.Name = "Suck my balls";
            movie.Showtime = DateTime.Now;
        

            movieRepository.Add(movie);

            var movie1 = new Movie();
            movie1.Id = 2;
            movie1.Name = "Suck my balls";
            movie1.Showtime = DateTime.Now;

            movieRepository.Add(movie1);

            movieRepository.Delete(movie.Id);
        }
        public static void PopulateTestTicket()
        {
            var ticketRepository = new XmlRepository<Ticket>();

            var ticket = new Ticket();
            ticket.Id = 1;
            ticket.MovieId = 2;
            ticket.SeetNumber = 3;
        
            ticketRepository.Add(ticket);

            var ticket1 = new Ticket();
            ticket1.Id = 2;
            ticket1.MovieId = 2;
            ticket1.SeetNumber = 3;

            ticketRepository.Add(ticket1);

            ticketRepository.Delete(ticket.Id);
        }
    }
}
