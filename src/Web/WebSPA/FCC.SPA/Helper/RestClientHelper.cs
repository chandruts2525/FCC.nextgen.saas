using RestSharp;

namespace FCC.SPA.Helper
{
    public static class RestClientHelper
    {
        private static IConfiguration _configuration;
        public static void RestClientHelperConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public static RestRequest GetRestRequest<T>(string uri, T? body, List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headers, Method method = Method.Get) where T : class
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
            if (method == Method.Post && body != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody<T>(body);
            }
            return request;
        }

        public static RestClient GetRestClient()
        {
            RestClient restClient = new();
            return restClient;
        }

    }
}
