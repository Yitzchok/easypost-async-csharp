using System;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public interface IHttpClient
    {
        int Timeout { get; set; }
        Uri BaseUrl { get; }

        Task<IRestResponse> ExecuteAsync(IRestRequest request);
        Task<IRestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request) where TResponse : new();
        IRestResponse<TResponse> Execute<TResponse>(IRestRequest request) where TResponse : new();

        IRestRequest AddAuthorizationToRequest(EasyPostRequest request);
    }
}