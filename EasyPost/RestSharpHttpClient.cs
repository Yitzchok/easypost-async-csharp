using System;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public class RestSharpHttpClient : IHttpClient
    {
        internal readonly IRestClient restClient;
        internal readonly ClientConfiguration Configuration;

        public RestSharpHttpClient(ClientConfiguration clientConfiguration)
        {
            if (clientConfiguration == null)
                throw new ArgumentNullException(nameof(clientConfiguration));

            Configuration = clientConfiguration;
            restClient = new RestClient(clientConfiguration.ApiBase);

            if (clientConfiguration.Timeout > 0)
            {
                restClient.Timeout = clientConfiguration.Timeout;
            }
        }

        public RestSharpHttpClient(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public int Timeout
        {
            get => restClient.Timeout;
            set => restClient.Timeout = value;
        }

        public Uri BaseUrl => restClient.BaseUrl;

        public Task<IRestResponse> ExecuteAsync(IRestRequest request) =>
            restClient.ExecuteAsync(request);

        public Task<IRestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request) where TResponse : new() =>
            restClient.ExecuteAsync<TResponse>(request);

        public IRestResponse<TResponse> Execute<TResponse>(IRestRequest request) where TResponse : new() =>
            restClient.Execute<TResponse>(request);

        public IRestRequest AddAuthorizationToRequest(EasyPostRequest request)
        {
            var restRequest = request.RestRequest;

            restRequest.AddHeader("authorization", "Bearer " + Configuration.ApiKey);

            return restRequest;
        }
    }
}