using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniptfisher.Model.Public;
using RestSharp;
using Sniptfisher.Exceptions;

namespace Sniptfisher.Repository
{
    public class SniptRepository : Interfaces.ISniptRepository
    {
        public const string PUBLIC_API_URI = "https://snipt.net/api/public/";
        private const string SNIPT_API_RESOURCE = "snipt";
        private readonly RestSharp.RestClient restClient;

        public SniptRepository()
        {
            restClient = new RestSharp.RestClient(PUBLIC_API_URI);
        }

        public Task<Snipt> Create(Snipt obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Public.Snipt obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int objKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Encuentra un <see cref="Snipt"/> a traves de la API pública
        /// </summary>
        /// <param name="objKey">El ID del SniptModel</param>
        /// <returns>Un objeto de tipo <see cref="Snipt"/></returns>
        public Task<Snipt> Find(int objKey)
        {
            IRestRequest request = new RestRequest(SNIPT_API_RESOURCE + "/{sniptId}", Method.GET);
            var taskCompletionSource = new TaskCompletionSource<Snipt>();
            request.AddUrlSegment("sniptId", objKey.ToString());

            restClient.ExecuteAsync<Snipt>(request, (response) =>
                {
                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método Find()"));
                    }

                    if (response.ResponseStatus == ResponseStatus.Completed &&
                        response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var item = response.Data;
                        taskCompletionSource.TrySetResult(item);
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

        /// <summary>
        /// Devuelve una lista de <see cref="Snipt"/>'s públicos a traves
        /// del API público. Actualmente se limita a los primeros 20, faltaría
        /// algún mecanismo para parametrizarlo fácilmente.
        /// </summary>
        /// <returns>Una colección observable de <see cref="Snipt"/></returns>
        public Task<ObservableCollection<Snipt>> FindAll()
        {
            // Preparo la petición a la API y un objeto de "Finalización de Tarea"
            IRestRequest request = new RestRequest(SNIPT_API_RESOURCE, Method.GET);
            var taskCompletionSource = new TaskCompletionSource<ObservableCollection<Snipt>>();

            // Ejecuto la petición y le asigno una lambda que terminará la tarea
            restClient.ExecuteAsync<ApiResponse>(request, (response) =>
                {
                    // Si hay algún error intentando realizar la peticion HTTP lanzo excepción
                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método Find()"));
                    }

                    // Si todo ha ido bien agrego un resultado a la tarea para finalizarla,
                    // si la consulta se realizó pero la respuesta no es la adecuada, lanzo excepción.
                    if (response.ResponseStatus == ResponseStatus.Completed &&
                        response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ObservableCollection<Snipt> items = new ObservableCollection<Snipt>();
                        foreach (Snipt item in response.Data.objects)
                        {
                            items.Add(item);
                        }
                        taskCompletionSource.TrySetResult(items); // Termina la tarea y notifica su finalización
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

        public Task<ObservableCollection<Snipt>> FindByUserId(int userId)
        {
            IRestRequest request = new RestRequest(SNIPT_API_RESOURCE, Method.GET);
            var taskCompletionSource = new TaskCompletionSource<ObservableCollection<Snipt>>();

            request.AddParameter("user", userId);

            restClient.ExecuteAsync<ApiResponse>(request, (response) =>
                {
                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método Find()"));
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ObservableCollection<Snipt> items = new ObservableCollection<Snipt>();
                        foreach (Snipt item in response.Data.objects)
                        {
                            items.Add(item);
                        }
                        taskCompletionSource.TrySetResult(items);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                             response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                             response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        taskCompletionSource.TrySetException(
                            new ApiRequestException("Error realizando petición a la API en el método Find(): " + response.StatusCode + " " + response.StatusDescription));
                    }
                });

            return taskCompletionSource.Task;
        }

        public Task<ObservableCollection<Snipt>> FindWithOffset(int offset)
        {
            IRestRequest request = new RestRequest(SNIPT_API_RESOURCE, Method.GET);
            var taskCompletionSource = new TaskCompletionSource<ObservableCollection<Snipt>>();

            request.AddParameter("offset", offset);

            restClient.ExecuteAsync<ApiResponse>(request, (response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método Find()"));
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Snipt> items = new ObservableCollection<Snipt>();
                    foreach (Snipt item in response.Data.objects)
                    {
                        items.Add(item);
                    }
                    taskCompletionSource.TrySetResult(items);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                         response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                         response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    taskCompletionSource.TrySetException(
                        new ApiRequestException("Error realizando petición a la API en el método Find(): " + response.StatusCode + " " + response.StatusDescription));
                }
            });

            return taskCompletionSource.Task;
        }

        public Task<ObservableCollection<Snipt>> FindWithQuery(string query)
        {
            IRestRequest request = new RestRequest(SNIPT_API_RESOURCE, Method.GET);
            var taskCompletionSource = new TaskCompletionSource<ObservableCollection<Snipt>>();

            request.AddParameter("q", query);

            restClient.ExecuteAsync<ApiResponse>(request, (response) =>
                {
                    // Si hay algún error intentando realizar la peticion HTTP lanzo excepción
                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método FindWithQuery()"));
                    }

                    // Si todo ha ido bien agrego un resultado a la tarea para finalizarla,
                    // si la consulta se realizó pero la respuesta no es la adecuada, lanzo excepción.
                    if (response.ResponseStatus == ResponseStatus.Completed &&
                        response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ObservableCollection<Snipt> items = new ObservableCollection<Snipt>();
                        foreach (Snipt item in response.Data.objects)
                        {
                            items.Add(item);
                        }
                        taskCompletionSource.TrySetResult(items); // Termina la tarea y notifica su finalización
                    }
                    else if (response.ResponseStatus == ResponseStatus.Completed &&
                            (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                             response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                             response.StatusCode == System.Net.HttpStatusCode.InternalServerError))
                    {
                        taskCompletionSource.TrySetException(
                            new ApiRequestException("Error realizando petición a la API en el método FindWithQuery(): " + response.StatusCode + " " + response.StatusDescription));
                    }
                });

            return taskCompletionSource.Task;
        }
        
        public Task<ObservableCollection<Snipt>> FindWithOffsetAndQuery(int offset, string query)
        {
            IRestRequest request = new RestRequest(SNIPT_API_RESOURCE, Method.GET);
            var taskCompletionSource = new TaskCompletionSource<ObservableCollection<Snipt>>();
            request.AddParameter("offset", offset);
            request.AddParameter("q", query);

            restClient.ExecuteAsync<ApiResponse>(request, (response) =>
            {
                // Si hay algún error intentando realizar la peticion HTTP lanzo excepción
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    taskCompletionSource.TrySetException(new ApiRequestException("Error de conexión realizando petición a la API en el método FindWithOffsetAndQuery()"));
                }

                // Si todo ha ido bien agrego un resultado a la tarea para finalizarla,
                // si la consulta se realizó pero la respuesta no es la adecuada, lanzo excepción.
                if (response.ResponseStatus == ResponseStatus.Completed &&
                    response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Snipt> items = new ObservableCollection<Snipt>();
                    foreach (Snipt item in response.Data.objects)
                    {
                        items.Add(item);
                    }
                    taskCompletionSource.TrySetResult(items); // Termina la tarea y notifica su finalización
                }
                else if (response.ResponseStatus == ResponseStatus.Completed &&
                        (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                            response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                            response.StatusCode == System.Net.HttpStatusCode.InternalServerError))
                {
                    taskCompletionSource.TrySetException(
                        new ApiRequestException("Error realizando petición a la API en el método FindWithOffsetAndQuery(): " + response.StatusCode + " " + response.StatusDescription));
                }
            });

            return taskCompletionSource.Task;
        }
    }
}
