using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Services.Interfaces
{
    public interface IResourceService
    {
        /// <summary>
        /// Serializes an object as XML in app's IsolatedStorageFile.
        /// </summary>
        /// <typeparam name="T">the type of the object to be saved.</typeparam>
        /// <param name="key">the key for the object. The file's name where the object 
        /// will be serialized will be this key and the XML file extension.</param>
        /// <param name="obj">the object to be serialized.</param>
        /// <returns>true if the file was successfully serialized, false if not.</returns>
        Task<bool> Save<T>(string key, T obj);

        /// <summary>
        /// De-serializes an object from app's IsolatedStorageFile.
        /// </summary>
        /// <typeparam name="T">the type of the object to be de-serialized.</typeparam>
        /// <param name="key">the key for the object.</param>
        /// <returns>the object requested.</returns>
        Task<T> Load<T>(string key);
    }
}
