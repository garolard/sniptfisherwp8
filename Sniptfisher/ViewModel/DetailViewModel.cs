using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Sniptfisher.Model.Public;
using Sniptfisher.Services.Interfaces;
using Sniptfisher.Services;
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
        #endregion Propiedades enlazables

        #region Comandos
        public ICommand ShareCommand { get; private set; }
        #endregion Comandos

        #region Servicios
        private readonly IShareService ShareService;
        #endregion Servicios

        public DetailViewModel(
            IShareService shareService)
        {
            this.ShareService = shareService;
            this.ShareCommand = new RelayCommand(this.ShareItem);
        }

        private void ShareItem()
        {
            var message = string.Format("Sharing {0} by {1} from @sniptfisher, a WP8 client for @snipt", ActiveItem.title, ActiveItem.user.username);
            this.ShareService.Share(ActiveItem.title, message, ActiveItem.full_absolute_url);
        }
    }
}
