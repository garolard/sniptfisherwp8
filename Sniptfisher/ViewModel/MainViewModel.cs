using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;
using Sniptfisher.Repository.Interfaces;
using Sniptfisher.Repository;
using Sniptfisher.Services.Interfaces;
using Sniptfisher.Services;
using System.Windows.Input;
using RestSharp;
using System.Windows;

namespace Sniptfisher.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, Interfaces.IMainViewModel, Interfaces.INavigable
    {
        #region Miembros privados
        private object _navigationContext;
        public object NavigationContext
        {
            private get { return _navigationContext; }
            set
            {
                if (value is User && value != LoggedUser)
                {
                    LoggedUser = value as User;
                }
            }
        }
        #endregion Miembros privados

        #region Propiedades enlazables
        /// <summary>
        /// The <see cref="Items" /> property's name.
        /// </summary>
        public const string ItemsPropertyName = "Items";

        private ObservableCollection<Snipt> _items = new ObservableCollection<Snipt>();

        /// <summary>
        /// Sets and gets the Items property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Snipt> Items
        {
            get
            {
                return _items;
            }

            set
            {
                if (_items == value)
                {
                    return;
                }

                RaisePropertyChanging(ItemsPropertyName);
                _items = value;
                RaisePropertyChanged(ItemsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsDataLoaded" /> property's name.
        /// </summary>
        public const string IsDataLoadedPropertyName = "IsDataLoaded";

        private bool _isDataLoaded = false;

        /// <summary>
        /// Sets and gets the IsDataLoaded property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsDataLoaded
        {
            get
            {
                return Items.Count > 0;
            }

            set
            {
                if (_isDataLoaded == value)
                {
                    return;
                }

                RaisePropertyChanging(IsDataLoadedPropertyName);
                _isDataLoaded = value;
                RaisePropertyChanged(IsDataLoadedPropertyName);
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

        /// <summary>
        /// The <see cref="LoggedUser" /> property's name.
        /// </summary>
        public const string LoggedUserPropertyName = "LoggedUser";

        private Model.Public.User _loggedUser = null;

        /// <summary>
        /// Sets and gets the LoggedUser property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Model.Public.User LoggedUser
        {
            get
            {
                return _loggedUser;
            }

            set
            {
                if (_loggedUser == value)
                {
                    return;
                }

                RaisePropertyChanging(LoggedUserPropertyName);
                _loggedUser = value;
                RaisePropertyChanged(LoggedUserPropertyName);
            }
        }
        #endregion Propiedades enlazables

        #region Comandos
        public ICommand ViewItemDetailCommand { get; private set; }
        public ICommand LoadMoreItemsCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        #endregion Comandos

        #region Servicios
        private readonly ISniptRepository LocalSniptRepository;
        private readonly INavigationService NavigationService;
        private readonly IDialogService DialogService;
        #endregion Servicios


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// Hay que pasarle todos los servicios por el constructor 
        /// inyectador por el contenedor de IoC.
        /// </summary>
        public MainViewModel(
            ISniptRepository sniptRepository,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            this.LocalSniptRepository = sniptRepository;
            this.NavigationService = navigationService;
            this.DialogService = dialogService;

            this.ViewItemDetailCommand = new RelayCommand<Snipt>(this.ViewItemDetail);
            this.LoadMoreItemsCommand = new RelayCommand(this.LoadExtraItems);
            this.SearchCommand = new RelayCommand(this.SearchSnipts);
            this.OpenSettingsCommand = new RelayCommand(this.OpenSettings);
            this.AboutCommand = new RelayCommand(this.OpenAboutPage);
        }
        
        async public Task LoadDataAsync()
        {
            // Este m�todo deber�a estar en un hipot�tico "SniptService"
            IsLoading = true;
            try
            {
                Items = await this.LocalSniptRepository.FindAll();
            }
            catch (Exceptions.ApiRequestException are)
            {
                System.Diagnostics.Debug.WriteLine(are.Message);
                this.DialogService.Show("Tenemos problemas para conectarnos con Snipt.net. Por favor, revisa tu conexi�n a internet.");
                IsLoading = false;
            }
            IsLoading = false;
        }

        private void ViewItemDetail(Snipt item)
        {
            this.NavigationService.NavigateTo<Interfaces.IDetailViewModel>(item);
        }

        async public void LoadExtraItems()
        {
            IsLoading = true;
            try
            {
                var newItems = await this.LocalSniptRepository.FindWithOffset(this.Items.Count);
                foreach (Snipt item in newItems)
                {
                    this.Items.Add(item);
                }
            }
            catch (Exceptions.ApiRequestException are)
            {
                System.Diagnostics.Debug.WriteLine(are.Message);
                this.DialogService.Show("Tenemos problemas para conectarnos con Snipt.net. Por favor, revisa tu conexi�n a internet.");
                IsLoading = false;
            }
            IsLoading = false;
        }

        private void SearchSnipts()
        {
            this.NavigationService.NavigateTo<Interfaces.ISearchViewModel>();
        }

        private void OpenSettings()
        {
            this.NavigationService.NavigateTo<Interfaces.ISettingsViewModel>();
        }

        private void OpenAboutPage()
        {
            this.NavigationService.NavigateTo<Interfaces.IAboutViewModel>();
        }
    }
}
