using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YRX.GLCinema.DatabaseAccess
{
    public class Entity
    {
        [XmlAttribute]
        public int Id { get; set; }

        public override string ToString()
        {
            var serializerSettings =
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            var obj = JsonConvert.SerializeObject(this, serializerSettings);
            return obj;
        }
    }

    public class Seat : Entity
    {
        public int Row { get; set; }

        public int SeetNumber { get; set; }        

        public bool Available { get; set; }

        public double Price { get; set; }
    }

    public class Ticket : Entity
    {
        public int MovieId { get; set; }

        public int Seat { get; set; }

		public double Price {get;set;}		
    }

    public class Order : Entity
    {
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public double OrderTotal { get; set; }

        public bool Approved { get; set; }

        [XmlArray("Tickets")]
        [XmlArrayItem("Ticket")]
        public List<Ticket> Tickets { get; set; }
    }

    public class Movie : Entity
    {
        public string Name { get; set; }

        public DateTime Showtime { get; set; }

        public int HallId { get; set; }

        [XmlArray("Ranges")]
        [XmlArrayItem("Range")]
        public List<IntRange> PriceRange { get; set; }
        //{
        //    get
        //    {
        //        var list = new List<IntRange>();
        //        list.Add(new IntRange() { From = 1, To = 60, Cost = 55 });
        //        list.Add(new IntRange() { From = 61, To = 135, Cost = 65 });
        //        list.Add(new IntRange() { From = 136, To = 151, Cost = 75 });
        //        return list;
        //    }
        //}

        [XmlArray("Seats")]
        [XmlArrayItem("Seat")]
        public List<Seat> Seats { get; set; }
        //{
        //    get
        //    {
        //        var list = new List<Seat>();
        //        var schema = new Hall().Schema;

        //        var id = 0;

        //        for (var i = 0; i < schema.Count; i++)
        //        {
        //            for (var j = 0; j < schema[i].Count; j++)
        //            {
        //                id++;
        //                var price = PriceRange.FirstOrDefault(x => id >= x.From && id <= x.To).Cost;

        //                list.Add(new Seat() { Id = id, Row = i + 1, SeetNumber = j + 1, Price = price, Available = true });
        //            }
        //        }

        //        return list;
        //    }
        //}
    }

    public class IntRange
    {
        public int From { get; set; }

        public int To { get; set; }

        public double Cost { get; set; }
    }

    public class Hall : Entity
    {
        [XmlArray("Schema")]
        [XmlArrayItem("row")]
        public List<List<int>> Schema = new List<List<int>>()
        {
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
        };

        //[XmlArray("Schema")]
        //public static int[][] Schema = new int[11][]
        //{
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
        //};        
    }
}
