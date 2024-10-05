using RestSharp;

namespace FCC.SPA.Abstractions
{
    public interface IRestClientService
    {
        RestClient GetRestClient();
        RestRequest GetRestRequest<T>(string uri, T? body, List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headers, Method method = Method.Get) where T : class;
    }
}
