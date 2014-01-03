using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Repository.Interfaces
{
    /// <summary>
    /// Public interface that contains the basic CRUD
    /// operations supported by all the model entities.
    /// </summary>
    /// <typeparam name="T">The model type that an implementor class actually manages.</typeparam>
    /// <typeparam name="K">The identifier type for the model type.</typeparam>
    public interface IRepository<T, K>
    {
        Task<T> Create(T obj);
        void Update(T obj);
        void Delete(K objKey);
        Task<T> Find(K objKey);
        Task<ObservableCollection<T>> FindAll();
    }
}
