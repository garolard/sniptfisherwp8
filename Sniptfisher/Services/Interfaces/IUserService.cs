using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Services.Interfaces
{
    public interface IUserService
    {
        bool LogIn(string username, string apiKey);
        void LogOut();
        List<Model.Public.Snipt> GetAllSnipts(int userId);
        bool FavoriteSnipt(int userId, int targetSniptId);
        bool UnfavoriteSnipt(int userId, int targetSniptId);
    }
}
