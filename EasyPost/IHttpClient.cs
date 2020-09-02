using System;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public interface IHttpClient
    {
        int Timeout { get; set; }
        Uri BaseUrl { get; }

        Task<IRestResponse> ExecuteAsync(RestRequest request);
        Task<IRestResponse<TResponse>> ExecuteAsync<TResponse>(RestRequest request) where TResponse : new();
        IRestResponse<TResponse> Execute<TResponse>(RestRequest request) where TResponse : new();

        RestRequest AddAuthorizationToRequest(EasyPostRequest request);
    }
}