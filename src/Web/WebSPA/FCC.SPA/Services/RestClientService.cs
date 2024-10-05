using FCC.SPA.Abstractions;
using RestSharp;

namespace FCC.SPA.Services
{
    public class RestClientService : IRestClientService
    {
        private readonly IConfiguration _configuration;

        public RestClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public RestRequest GetRestRequest<T>(string uri, T? body, List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headers, Method method = Method.Get) where T : class
        {
            var request = new RestRequest(uri, method);

            if (headers.Any())
            {
                headers.ForEach(header =>
               request.AddHeader(header.Key, header.Value));
            }
            if (parameters.Any())
            {
                parameters.ForEach(param =>
               request.AddParameter(param.Key, param.Value));
            }
            if ((method == Method.Post || method == Method.Put) && body != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }
            return request;
        }
        public RestClient GetRestClient()
        {
            RestClientOptions restClientOptions = new RestClientOptions()
            {
                MaxTimeout = 1800000
            };
            RestClient restClient = new(restClientOptions);
            return restClient;
        }
    }
}