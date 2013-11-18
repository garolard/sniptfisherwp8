using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Repository
{
    public class UserRepository : Interfaces.IUserRepository
    {
        public bool LogIn()
        {
            throw new NotImplementedException();
        }

        public bool LogOut()
        {
            throw new NotImplementedException();
        }

        public Task<Model.Public.UserModel> Create(Model.Public.UserModel obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Public.UserModel obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int objKey)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Public.UserModel> Find(int objKey)
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.ObjectModel.ObservableCollection<Model.Public.UserModel>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
