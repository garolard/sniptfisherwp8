using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;

namespace Sniptfisher.Repository.Interfaces
{
    public interface ISniptRepository : IRepository<SniptModel, int>
    {
        // TODO: Agregar métodos específicos para los Snipts
        Task<ObservableCollection<SniptModel>> FindByUserId(int usrId);
        Task<ObservableCollection<SniptModel>> FindWithOffset(int offset);
        Task<ObservableCollection<SniptModel>> FindWithQuery(string query);
    }
}
