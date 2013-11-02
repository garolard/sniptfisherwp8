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
                if (value is SniptModel && value != ActiveItem)
                    ActiveItem = value as SniptModel;
            }
        }

        #region Propiedades enlazables
        /// <summary>
        /// The <see cref="ActiveItem" /> property's name.
        /// </summary>
        public const string ActiveItemPropertyName = "ActiveItem";

        private SniptModel _activeItem = null;

        /// <summary>
        /// Sets and gets the ActiveItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SniptModel ActiveItem
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

        private ObservableCollection<SniptModel> _relatedItems = new ObservableCollection<SniptModel>();

        /// <summary>
        /// Sets and gets the RelatedItems property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<SniptModel> RelatedItems
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
        #endregion Servicios

        public DetailViewModel(
            ISniptRepository sniptRepository,
            IShareService shareService)
        {
            this.LocalSniptRepository = sniptRepository;
            this.ShareService = shareService;
            this.ShareCommand = new RelayCommand(this.ShareItem);
            this.ChangeActiveItemCommand = new RelayCommand<SniptModel>(this.ChangeActiveItem);
        }

        private void ShareItem()
        {
            var message = string.Format("Sharing {0} by {1} from @sniptfisher, a WP8 client for @snipt", ActiveItem.title, ActiveItem.user.username);
            this.ShareService.Share(ActiveItem.title, message, ActiveItem.full_absolute_url);
        }

        private void ChangeActiveItem(SniptModel item)
        {
            this.ActiveItem = item;
        }

        async public Task LoadRelatedItems()
        {
            this.RelatedItems = await this.LocalSniptRepository.FindByUserId(ActiveItem.user.id);
        }
    }
}
