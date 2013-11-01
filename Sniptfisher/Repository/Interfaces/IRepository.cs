using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Repository.Interfaces
{
    public interface IRepository<T, K>
    {
        Task<T> Create(T obj);
        void Update(T obj);
        void Delete(K objKey);
        Task<T> Find(K objKey);
        Task<ObservableCollection<T>> FindAll();
    }
}
