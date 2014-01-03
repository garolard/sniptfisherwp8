using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;

namespace Sniptfisher.Repository.Interfaces
{
    /// <summary>
    /// Public interface that declare operations executed over
    /// the <see cref="User"/> entity. All methods are async by default and
    /// support async/await operators.
    /// </summary>
    public interface IUserRepository : IRepository<User, int>
    {
        /// <summary>
        /// Retrieve all the public information from an user
        /// throught its public username.
        /// </summary>
        /// <param name="username">The user username.</param>
        /// <returns>An <see cref="User"/> with only the public information.</returns>
        Task<User> FindByUsername(string username);

        /// <summary>
        /// Retrieve the private information from an <see cref="User"/>
        /// using its ID, username and api key. For login and private interactions
        /// purposes.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="username">The user username.</param>
        /// <param name="apiKey">The use private api key.</param>
        /// <returns>An <see cref="User"/> with the public and private information.</returns>
        Task<User> FindPrivate(int userId, string username, string apiKey); // TODO: Mejorar seteo de credenciales
    }
}
