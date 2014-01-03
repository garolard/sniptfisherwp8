using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;

namespace Sniptfisher.Repository.Interfaces
{
    /// <summary>
    /// Public interface that declare operations executed over
    /// the <see cref="Snipt"/> entity. All methods are async by default and
    /// support async/await operators.
    /// </summary>
    public interface ISniptRepository : IRepository<Snipt, int>
    {
        /// <summary>
        /// Finds all snippets from an user throught his ID.
        /// </summary>
        /// <param name="usrId">The user unique ID.</param>
        /// <returns>An <see cref="System.Collections.ObjectModel.ObservableCollection&lt;Snipt&gt;"/> that contains all the user's <see cref="Snipt"/>.</returns>
        Task<ObservableCollection<Snipt>> FindByUserId(int usrId);

        /// <summary>
        /// Query the public timeline with a given offset.
        /// </summary>
        /// <param name="offset">The desired element offset.</param>
        /// <returns>An <see cref="System.Collections.ObjectModel.ObservableCollection&lt;Snipt&gt;"/> that contais 
        /// <see cref="Snipt"/> starting at <paramref name="offset"/> in the public timeline.</returns>
        Task<ObservableCollection<Snipt>> FindWithOffset(int offset);

        /// <summary>
        /// Query the public timeline using a query term to filter the request.
        /// </summary>
        /// <param name="query">The query term.</param>
        /// <returns>An <see cref="System.Collections.ObjectModel.ObservableCollection&lt;Snipt&gt;"/> with <see cref="Snipt"/> that matches the <paramref name="query"/>.</returns>
        Task<ObservableCollection<Snipt>> FindWithQuery(string query);

        /// <summary>
        /// A mix between the two previous methods. It is used in
        /// search page in order to load additional results when the user
        /// reaches the end of the results list.
        /// </summary>
        /// <param name="offset">The desired offset.</param>
        /// <param name="query">The query term.</param>
        /// <returns>An <see cref="System.Collections.ObjectModel.ObservableCollection&lt;Snipt&gt;"/> with the result.</returns>
        Task<ObservableCollection<Snipt>> FindWithOffsetAndQuery(int offset, string query);
    }
}
