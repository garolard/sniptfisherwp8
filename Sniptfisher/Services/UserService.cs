using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Repository.Interfaces;

namespace Sniptfisher.Services
{
    public class UserService : Interfaces.IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly Services.SettingsService SettingsService;
        private readonly Services.Interfaces.IPersistentResourceService PersistentResourceService;

        public UserService(
            IUserRepository userRepository,
            SettingsService settingsService,
            Services.Interfaces.IPersistentResourceService persistentResourceService)
        {
            this.UserRepository = userRepository;
            this.SettingsService = settingsService;
            this.PersistentResourceService = persistentResourceService;
        }

        async public Task<bool> LogIn(string username, string apiKey)
        {
            // Primero se consulta https://snipt.net/api/public/user/?username=:username
            // para recuperar el ID del usuario.
            //
            // Seguidamente se consulta https://snipt.net/api/private/user/{userId}/
            // para recuperar los datos privados del usuaro.
            // Si los recuperamos correctamente hemos hecho Log In correctamente.
            Model.Public.User basicUser = await this.UserRepository.FindByUsername(username);

            if (basicUser == null)
                return false;

            Model.Public.User completeUser = await this.UserRepository.FindPrivate(basicUser.id, username, apiKey);

            if (completeUser == null)
                return false;

            // TODO: Securizar apiKey del usuario
            SettingsService.UsernameSetting = username;
            SettingsService.ApikeySetting = apiKey;
            basicUser = null;
            // App.Current.Resources["LoggedUser"] = completeUser; -- No implementado para Windows Phone

            bool saved = false;

            try
            {
                saved = await this.PersistentResourceService.Save<Model.Public.User>("loggedUser", completeUser);
            }
            catch (System.IO.IsolatedStorage.IsolatedStorageException ise)
            {
                System.Diagnostics.Debug.WriteLine(ise.Message + ": " + ise.StackTrace);
                saved = false;
                SettingsService.Clear();
            }
            return saved;
        }

        public void LogOut()
        {
            SettingsService.Clear();
            App.Current.Resources["LoggedUser"] = null;
        }

        public List<Model.Public.Snipt> GetAllSnipts(int userId)
        {
            throw new NotImplementedException();
        }

        public bool FavoriteSnipt(int userId, int targetSniptId)
        {
            throw new NotImplementedException();
        }

        public bool UnfavoriteSnipt(int userId, int targetSniptId)
        {
            throw new NotImplementedException();
        }
    }
}
