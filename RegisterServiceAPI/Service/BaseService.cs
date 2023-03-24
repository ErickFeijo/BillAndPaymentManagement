using RegisterServiceAPI.Data.Repositories.Interfaces;
using RegisterServiceAPI.Data.Repositories;
using RegisterServiceAPI.Service.Interfaces;

namespace RegisterServiceAPI.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        IBaseRepository<T> _repository;

        public BaseService(DbContext context)
        {
            _repository = new BaseRepository<T>(context);
        }

        public T Find(int id)
        {
            return _repository.Find(id);
        }

        public IQueryable<T> List()
        {
            return _repository.List();
        }

        public virtual void Add(T item)
        {
            _repository.Add(item);
            _repository.Save();
        }

        public void Remove(T item)
        {
            _repository.Remove(item);
            _repository.Save();
        }

        public void Edit(T item)
        {
            _repository.Edit(item);
            _repository.Save();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
