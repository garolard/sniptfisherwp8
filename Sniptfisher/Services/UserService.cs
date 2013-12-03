using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Services
{
    public class UserService : Interfaces.IUserService
    {
        public bool LogIn()
        {
            // Primero se consulta https://snipt.net/api/public/user/?username=:username
            // para recuperar el ID del usuario.
            //
            // Seguidamente se consulta https://snipt.net/api/private/user/{userId}/
            // para recuperar los datos privados del usuaro.
            // Si los recuperamos correctamente hemos hecho Log In correctamente.
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
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
