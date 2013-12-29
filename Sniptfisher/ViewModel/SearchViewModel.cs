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

        private ObservableCollection<Snipt> _results = new ObservableCollection<Snipt>();

        /// <summary>
        /// Sets and gets the Results property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Snipt> Results
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
            this.ViewItemDetailCommand = new RelayCommand<Snipt>(this.ViewItemDetail);
            this.DoSearchCommand = new RelayCommand<string>(this.DoSearch);
            this.LoadMoreResultsCommand = new RelayCommand(this.LoadMoreResults);
        }

        private void ViewItemDetail(Snipt item)
        {
            this.NavigationService.NavigateTo<Interfaces.IDetailViewModel>(item);
        }

        async private void DoSearch(string SearchTerm)
        {
            this.SearchTerm = SearchTerm;
            IsLoading = true;
            try
            {
                this.Results = await this.LocalSniptRepository.FindWithQuery(SearchTerm);
            }
            catch (Exceptions.ApiRequestException are)
            {
                System.Diagnostics.Debug.WriteLine(are.Message);
                // TODO: Traducir cadena
                this.DialogService.Show("Tenemos problemas para encontrar lo que buscas. Usa otro término o prueba más tarde.");
                IsLoading = false;
            }
            IsLoading = false;
        }

        async private void LoadMoreResults()
        {
            // TODO: Mostrar indicador de progreso sin ocultar lista de resultados
            try
            {
                var newItems = await this.LocalSniptRepository.FindWithOffsetAndQuery(this.Results.Count, SearchTerm);
                if (newItems.Count > 0)
                {
                    foreach (Snipt item in newItems) 
                    {
                        this.Results.Add(item);
                    }
                }
            }
            catch (Exceptions.ApiRequestException are)
            {
                System.Diagnostics.Debug.WriteLine(are.Message);
                // TODO: Traducir cadena
                this.DialogService.Show("Tenemos problemas para encontrar más resultados, o bien no hay más resultados coincidentes con tu búsqueda");
            }
        }
    }
}
