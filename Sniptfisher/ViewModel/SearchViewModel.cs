using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Sniptfisher.Model.Public;
using Sniptfisher.Repository;
using Sniptfisher.Repository.Interfaces;
using Sniptfisher.Services;
using Sniptfisher.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sniptfisher.ViewModel
{
    public class SearchViewModel : ViewModelBase, Interfaces.ISearchViewModel
    {
        #region Propiedades enlazables
        /// <summary>
        /// The <see cref="SearchTerm" /> property's name.
        /// </summary>
        public const string SearchTermPropertyName = "SearchTerm";

        private string _searchTerm = String.Empty;

        /// <summary>
        /// Sets and gets the SearchTerm property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SearchTerm
        {
            get
            {
                return _searchTerm;
            }

            set
            {
                if (_searchTerm == value)
                {
                    return;
                }

                RaisePropertyChanging(SearchTermPropertyName);
                _searchTerm = value;
                RaisePropertyChanged(SearchTermPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Results" /> property's name.
        /// </summary>
        public const string ResultsPropertyName = "Results";

        private ObservableCollection<SniptModel> _results = new ObservableCollection<SniptModel>();

        /// <summary>
        /// Sets and gets the Results property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<SniptModel> Results
        {
            get
            {
                return _results;
            }

            set
            {
                if (_results == value)
                {
                    return;
                }

                RaisePropertyChanging(ResultsPropertyName);
                _results = value;
                RaisePropertyChanged(ResultsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        private bool _isLoading = false;

        /// <summary>
        /// Sets and gets the IsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading == value)
                {
                    return;
                }

                RaisePropertyChanging(IsLoadingPropertyName);
                _isLoading = value;
                RaisePropertyChanged(IsLoadingPropertyName);
            }
        }
        #endregion Propiedades enlazables

        #region Comandos
        public ICommand ViewItemDetailCommand { get; private set; }
        public ICommand DoSearchCommand { get; private set; }
        public ICommand LoadMoreResultsCommand { get; private set; }
        #endregion Comandos

        #region Servicios
        private readonly ISniptRepository LocalSniptRepository;
        private readonly INavigationService NavigationService;
        private readonly IDialogService DialogService;
        #endregion Servicios

        public SearchViewModel(
            ISniptRepository sniptRepository,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            this.LocalSniptRepository = sniptRepository;
            this.NavigationService = navigationService;
            this.DialogService = dialogService;

            // Inicializar comandos
            this.ViewItemDetailCommand = new RelayCommand<SniptModel>(this.ViewItemDetail);
            this.DoSearchCommand = new RelayCommand<string>(this.DoSearch);
            this.LoadMoreResultsCommand = new RelayCommand(this.LoadMoreResults);
        }

        private void ViewItemDetail(SniptModel item)
        {
            this.NavigationService.NavigateTo<Interfaces.IDetailViewModel>(item);
        }

        async private void DoSearch(string searchTerm)
        {
            this.SearchTerm = searchTerm;
            IsLoading = true;
            try
            {
                this.Results = await this.LocalSniptRepository.FindWithQuery(searchTerm);
            }
            catch (Exceptions.ApiRequestException are)
            {
                System.Diagnostics.Debug.WriteLine(are.Message);
                this.DialogService.Show("Tenemos problemas para encontrar lo que buscas. Usa otro término o prueba más tarde.");
                IsLoading = false;
            }
            IsLoading = false;
        }

        async private void LoadMoreResults()
        {
            // var newItems = await this.LocalSniptRepository.FindWithOffset();
        }
    }
}
