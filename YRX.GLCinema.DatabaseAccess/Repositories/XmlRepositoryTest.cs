using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YRX.GLCinema.DatabaseAccess.Repositories
{
    public class XmlRepository<T> : IRepository<T> where T : Entity
    {
        public IQueryable<T> Items => _dbSet.Items;

        private readonly CinemaSet<T> _dbSet;

        public XmlRepository()
        {
            _dbSet = new CinemaSet<T>();
        }

        public T Get(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }
    }

    public class CinemaSet<T> where T : Entity
    {
        public IQueryable<T> Items => Deserialize().AsQueryable();

        private readonly CinemaDbContext _dbContext;
        private readonly XDocument _document;

        public CinemaSet()
        {
            _dbContext = CinemaDbContext.GetInstance;
            _document = _dbContext.Document;
        }

        private IEnumerable<T> Deserialize()
        {
            var name = typeof(T).Name;
            var objects = _document.Root.Element(name + 's').Elements(name);
            var xmlSerializer = new XmlSerializer(typeof(T));
            return objects.Select(o => (T)xmlSerializer.Deserialize(o.CreateReader()));
        }

        private XElement Serialize(T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return XElement.Parse(textWriter.ToString());
            }
        }

        public void Add(T item)
        {
            var name = typeof(T).Name;
            var node = _document.Root.Element(name + 's')?.Elements(name);

            if (node != null && node.Any())
            {
                var exist = node.FirstOrDefault(x => (int)x.Attribute("Id") == item.Id);

                if (exist != null)
                {
                    exist.Remove();
                }

                if (node != null && node.Any())
                {
                    node.LastOrDefault().AddAfterSelf(Serialize(item));
                }
                else
                {
                    _document.Root.Element(name + 's').Add(Serialize(item));
                }
            }
            else
            {
                _document.Root.Add(new XElement(name + 's', new XElement(Serialize(item))));
            }

            _dbContext.SaveChanges();
        }

        public void Remove(T item)
        {
            var name = typeof(T).Name;
            var node = _document.Root.Element(name + 's').Elements(name);
            node.FirstOrDefault(x => (int)x.Attribute("Id") == item.Id).Remove();
            _dbContext.SaveChanges();
        }
    }


    public class CinemaDbContext
    {
        private static readonly Lazy<CinemaDbContext> Instance = new Lazy<CinemaDbContext>(() => new CinemaDbContext());

        public static CinemaDbContext GetInstance => Instance.Value;

        public XDocument Document;

        public readonly string _path;

        private CinemaDbContext()
        {
            var acc = Assembly.GetEntryAssembly();
            _path = acc != null ? @"..\..\App_Data\database.xml" : Path.Combine(System.Web.HttpRuntime.BinDirectory, @"..\App_Data\database.xml");
            Document = XDocument.Load(ConfigurationManager.AppSettings.Get("Database") ?? _path);
        }
        
        public void SaveChanges()
        {
            Document.Save(ConfigurationManager.AppSettings.Get("Database") ?? _path);
        }        
    }
}
