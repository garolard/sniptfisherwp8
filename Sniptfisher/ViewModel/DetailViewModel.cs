using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Sniptfisher.Model.Public;
using Sniptfisher.Services.Interfaces;
using Sniptfisher.Services;
using Sniptfisher.Repository.Interfaces;
using Sniptfisher.Repository;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Sniptfisher.ViewModel
{
    public class DetailViewModel : ViewModelBase, Interfaces.IDetailViewModel, Interfaces.INavigable
    {
        public object NavigationContext
        {
            set
            {
                if (value is Snipt && value != ActiveItem)
                    ActiveItem = value as Snipt;
            }
        }

        #region Propiedades enlazables
        /// <summary>
        /// The <see cref="ActiveItem" /> property's name.
        /// </summary>
        public const string ActiveItemPropertyName = "ActiveItem";

        private Snipt _activeItem = null;

        /// <summary>
        /// Sets and gets the ActiveItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Snipt ActiveItem
        {
            get
            {
                return _activeItem;
            }

            set
            {
                if (_activeItem == value)
                {
                    return;
                }

                RaisePropertyChanging(ActiveItemPropertyName);
                _activeItem = value;
                RaisePropertyChanged(ActiveItemPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsRelatedDataLoaded" /> property's name.
        /// </summary>
        public const string IsRelatedDataLoadedPropertyName = "IsRelatedDataLoaded";

        private bool _isRelatedDataLoaded = false;

        /// <summary>
        /// Sets and gets the IsRelatedDataLoaded property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsRelatedDataLoaded
        {
            get
            {
                return _isRelatedDataLoaded;
            }

            set
            {
                if (_isRelatedDataLoaded == value)
                {
                    return;
                }

                RaisePropertyChanging(IsRelatedDataLoadedPropertyName);
                _isRelatedDataLoaded = value;
                RaisePropertyChanged(IsRelatedDataLoadedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RelatedItems" /> property's name.
        /// </summary>
        public const string RelatedItemsPropertyName = "RelatedItems";

        private ObservableCollection<Snipt> _relatedItems = new ObservableCollection<Snipt>();

        /// <summary>
        /// Sets and gets the RelatedItems property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Snipt> RelatedItems
        {
            get
            {
                return _relatedItems;
            }

            set
            {
                if (_relatedItems == value)
                {
                    return;
                }

                RaisePropertyChanging(RelatedItemsPropertyName);
                _relatedItems = value;
                RaisePropertyChanged(RelatedItemsPropertyName);
            }
        }
        #endregion Propiedades enlazables

        #region Comandos
        public ICommand ShareCommand { get; private set; }
        public ICommand ChangeActiveItemCommand { get; private set; }
        #endregion Comandos

        #region Servicios
        private readonly ISniptRepository LocalSniptRepository;
        private readonly IShareService ShareService;
        private readonly IDialogService DialogService;
        #endregion Servicios

        public DetailViewModel(
            ISniptRepository sniptRepository,
            IShareService shareService,
            IDialogService dialogService)
        {
            this.LocalSniptRepository = sniptRepository;
            this.ShareService = shareService;
            this.DialogService = dialogService;

            this.ShareCommand = new RelayCommand(this.ShareItem);
            this.ChangeActiveItemCommand = new RelayCommand<Snipt>(this.ChangeActiveItem);
        }

        private void ShareItem()
        {
            var message = string.Format("Sharing {0} by {1} from @sniptfisher, a WP8 client for @snipt", ActiveItem.title, ActiveItem.user.username);
            this.ShareService.Share(ActiveItem.title, message, ActiveItem.full_absolute_url);
        }

        private void ChangeActiveItem(Snipt item)
        {
            this.ActiveItem = item;
        }

        async public Task LoadRelatedItems()
        {
            try
            {
                this.RelatedItems = await this.LocalSniptRepository.FindByUserId(ActiveItem.user.id);
            }
            catch (Exceptions.ApiRequestException are)
            {
                System.Diagnostics.Debug.WriteLine(are.Message);
                this.DialogService.Show("Tenemos problemas para conectarnos con Snipt.net. Por favor, revisa tu conexión a internet.");
            }
        }
    }
}
