using System;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public class RestSharpHttpClient : IHttpClient
    {
        internal readonly IRestClient restClient;

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

        public Task<IRestResponse> ExecuteTaskAsync(RestRequest request) =>
            restClient.ExecuteTaskAsync(request);

        public Task<IRestResponse<TResponse>> ExecuteTaskAsync<TResponse>(RestRequest request) where TResponse : new() =>
            restClient.ExecuteTaskAsync<TResponse>(request);

        public IRestResponse<TResponse> Execute<TResponse>(RestRequest request) where TResponse : new() =>
            restClient.Execute<TResponse>(request);
    }
}