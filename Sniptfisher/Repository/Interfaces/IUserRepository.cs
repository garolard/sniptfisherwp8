using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;

namespace Sniptfisher.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindByUsername(string username);
        Task<User> FindPrivate(int userId, string username, string apiKey); // TODO: Mejorar seteo de credenciales
    }
}
