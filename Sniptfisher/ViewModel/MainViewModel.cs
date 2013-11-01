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
        public object NavigationContext { private get; set; }
        #endregion Miembros privados

        #region Propiedades enlazables
        /// <summary>
        /// The <see cref="Items" /> property's name.
        /// </summary>
        public const string ItemsPropertyName = "Items";

        private ObservableCollection<SniptModel> _items = new ObservableCollection<SniptModel>();

        /// <summary>
        /// Sets and gets the Items property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<SniptModel> Items
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
        #endregion Propiedades enlazables

        #region Comandos
        public ICommand ViewItemDetailCommand { get; private set; }
        #endregion Comandos

        #region Servicios
        private readonly ISniptRepository LocalSniptRepository;
        private readonly INavigationService NavigationService;
        #endregion Servicios


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// Hay que pasarle todos los servicios por el constructor 
        /// inyectador por el contenedor de IoC.
        /// </summary>
        public MainViewModel(
            ISniptRepository sniptRepository,
            INavigationService navigationService)
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

            this.ViewItemDetailCommand = new RelayCommand<SniptModel>(this.ViewItemDetail);            
        }

        // Este m�todo deber�a estar en un hipot�tico "SniptService"
        async public Task LoadDataAsync()
        {
            Items = await this.LocalSniptRepository.FindAll();
        }

        private void ViewItemDetail(SniptModel item)
        {
            this.NavigationService.NavigateTo<Interfaces.IDetailViewModel>(item);
        }
    }
}