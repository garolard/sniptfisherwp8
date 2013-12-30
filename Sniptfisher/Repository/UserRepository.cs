using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Exceptions;

namespace Sniptfisher.Repository
{
    public class UserRepository : Interfaces.IUserRepository
    {
        public const string PUBLIC_API_URI = "https://snipt.net/api/public/";
        public const string PRIVATE_API_URI = "https://snipt.net/api/private/";
        private const string USER_API_RESOURCE = "user";
        private readonly RestSharp.RestClient restClient;

        public UserRepository()
        {
            // Does not assign a base uri, this client may request both public and private endpoints
            this.restClient = new RestSharp.RestClient();
        }

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

        public Task<Model.Public.User> FindByUsername(string username)
        {
            IRestRequest request = new RestRequest(PUBLIC_API_URI + USER_API_RESOURCE, Method.GET);
            request.AddParameter("username", username);
            var taskCompletionSource = new TaskCompletionSource<Model.Public.User>();

            restClient.ExecuteAsync<Model.Public.UserApiResponse>(request, (response) =>
                {
                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método Find()"));
                    }

                    if (response.ResponseStatus == ResponseStatus.Completed &&
                        response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var item = response.Data;
                        taskCompletionSource.TrySetResult(item.objects[0]);
                    }
                    else if (response.ResponseStatus == ResponseStatus.Completed &&
                            (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                             response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                             response.StatusCode == System.Net.HttpStatusCode.InternalServerError))
                    {
                        taskCompletionSource.TrySetException(
                            new ApiRequestException("Error realizando petición a la API en el método Find(): " + response.StatusCode + " " + response.StatusDescription));
                    }
                });

            return taskCompletionSource.Task;
        }
    }
}
