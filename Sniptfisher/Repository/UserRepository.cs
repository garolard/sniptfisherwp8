using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Repository
{
    public class UserRepository : Interfaces.IUserRepository
    {
        public Task<Model.Public.User> Create(Model.Public.User obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Public.User obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int objKey)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Public.User> Find(int objKey)
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.ObjectModel.ObservableCollection<Model.Public.User>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
