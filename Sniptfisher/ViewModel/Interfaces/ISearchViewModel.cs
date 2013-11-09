using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;
using System.Windows.Input;

namespace Sniptfisher.ViewModel.Interfaces
{
    public interface ISearchViewModel
    {
        ObservableCollection<SniptModel> Results { get; }
        bool IsLoading { get; }

        ICommand ViewItemDetailCommand { get; }
        ICommand DoSearchCommand { get; }
        ICommand LoadMoreResultsCommand { get; }
    }
}
