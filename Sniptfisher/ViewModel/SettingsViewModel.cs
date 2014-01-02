using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sniptfisher.Services.Interfaces;

namespace Sniptfisher.ViewModel
{
    public class SettingsViewModel : ViewModelBase, Interfaces.ISettingsViewModel, Interfaces.INavigable
    {
        #region Propiedades enlazables
        /// <summary>
        /// The <see cref="Username" /> property's name.
        /// </summary>
        public const string UsernamePropertyName = "Username";

        private string _username = String.Empty;

        /// <summary>
        /// Sets and gets the Username property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                if (_username == value)
                {
                    return;
                }

                RaisePropertyChanging(UsernamePropertyName);
                _username = value;
                RaisePropertyChanged(UsernamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ApiKey" /> property's name.
        /// </summary>
        public const string ApiKeyPropertyName = "ApiKey";

        private string _apiKey = String.Empty;

        /// <summary>
        /// Sets and gets the ApiKey property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _apiKey;
            }

            set
            {
                if (_apiKey == value)
                {
                    return;
                }

                RaisePropertyChanging(ApiKeyPropertyName);
                _apiKey = value;
                RaisePropertyChanged(ApiKeyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLogged" /> property's name.
        /// </summary>
        public const string IsLoggedPropertyName = "IsLogged";

        private bool _isLogged = false;

        /// <summary>
        /// Sets and gets the IsLogged property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLogged
        {
            get
            {
                return _isLogged;
            }

            set
            {
                if (_isLogged == value)
                {
                    return;
                }

                RaisePropertyChanging(IsLoggedPropertyName);
                _isLogged = value;
                RaisePropertyChanged(IsLoggedPropertyName);
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
        public ICommand TryLoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand TrySetLoggedUserCommand { get; private set; }
        #endregion Comandos

        #region Servicios
        private readonly INavigationService NavigationService;
        private readonly IUserService UserService;
        private readonly IDialogService DialogService;
        private readonly Services.SettingsService SettingsService;
        private readonly IPersistentResourceService PersistentResourceService;
        #endregion Servicios

        public SettingsViewModel(
            INavigationService navigationService,
            IUserService userService,
            IDialogService dialogService,
            Services.SettingsService settingsService,
            IPersistentResourceService persistentResourceService)
        {
            this.NavigationService = navigationService;
            this.UserService = userService;
            this.DialogService = dialogService;
            this.SettingsService = settingsService;
            this.PersistentResourceService = persistentResourceService;

            this.TryLoginCommand = new RelayCommand(this.TryLogin);
            this.LogoutCommand = new RelayCommand(this.Logout);
            this.TrySetLoggedUserCommand = new RelayCommand(this.TrySetLoggedUser);
        }

        async public void TryLogin()
        {
            try
            {
                this.IsLogged = await this.UserService.LogIn(this.Username, this.ApiKey);
            }
            catch (Exceptions.ApiRequestException are)
            {
                this.IsLogged = false;
                System.Diagnostics.Debug.WriteLine(are.Message);
                this.DialogService.Show("Tenemos problemas para loguearte en Snipt.net, por favor, revisa tus credenciales o inténtalo de nuevo más tarde."); // TODO: Traducir cadena
            }
        }

        public void Logout()
        {
            this.UserService.LogOut();
        }

        async public void TrySetLoggedUser()
        {
            try
            {
                this.LoggedUser = await this.PersistentResourceService.Load<Model.Public.User>("loggedUser");
            }
            catch (System.IO.IsolatedStorage.IsolatedStorageException ise)
            {
                System.Diagnostics.Debug.WriteLine(ise.Message + ": " + ise.StackTrace);
                this.DialogService.Show("Error cargando datos del usuario."); // TODO: Traducir cadena
            }
        }

        public object NavigationContext
        {
            set { throw new NotImplementedException(); }
        }
    }
}
