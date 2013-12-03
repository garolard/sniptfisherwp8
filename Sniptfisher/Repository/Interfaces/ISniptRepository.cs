using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;

namespace Sniptfisher.Repository.Interfaces
{
    public interface ISniptRepository : IRepository<Snipt, int>
    {
        // TODO: Agregar métodos específicos para los Snipts
        Task<ObservableCollection<Snipt>> FindByUserId(int usrId);
        Task<ObservableCollection<Snipt>> FindWithOffset(int offset);
        Task<ObservableCollection<Snipt>> FindWithQuery(string query);
        Task<ObservableCollection<Snipt>> FindWithOffsetAndQuery(int offset, string query);
    }
}
