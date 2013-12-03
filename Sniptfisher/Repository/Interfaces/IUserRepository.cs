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
        // Métodos propios de los usuarios
        bool LogIn();
        bool LogOut();
    }
}
