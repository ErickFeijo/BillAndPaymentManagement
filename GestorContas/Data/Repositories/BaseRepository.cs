using GestorContas.Data.Repositories.Interfaces;

namespace GestorContas.Data.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private DbContext _context;

        #region Ctor
        public BaseRepository(DbContext context)
        {
            _context = context;
        }
        #endregion

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> List()
        {
            return _context.Set<T>();
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public void Edit(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
