using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;

namespace Sniptfisher.Services
{
    public class PersistentResourceService : Interfaces.IPersistentResourceService
    {
        public Task<bool> Save<T>(string key, T obj)
        {
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(key + ".xml", FileMode.OpenOrCreate, isolatedStorage))
            {
                serializer.Serialize(fileStream, obj);
                var taskCompletionSource = new TaskCompletionSource<bool>();
                taskCompletionSource.TrySetResult(true);
                return taskCompletionSource.Task;
            }
        }

        public Task<T> Load<T>(string key)
        {
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(key + ".xml", FileMode.Open, isolatedStorage))
            {
                var obj = (T)serializer.Deserialize(fileStream);
                var taskCompletionSource = new TaskCompletionSource<T>();
                taskCompletionSource.TrySetResult(obj);
                return taskCompletionSource.Task;
            }
        }
    }
}
