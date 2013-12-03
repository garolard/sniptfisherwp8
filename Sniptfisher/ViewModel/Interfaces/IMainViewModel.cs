using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sniptfisher.Model.Public;

namespace Sniptfisher.ViewModel.Interfaces
{
    public interface IMainViewModel
    {
        ObservableCollection<Snipt> Items { get; }
        bool IsDataLoaded { get; }

        ICommand ViewItemDetailCommand { get; }
    }
}
