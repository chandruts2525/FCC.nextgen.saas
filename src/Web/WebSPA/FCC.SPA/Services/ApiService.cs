using FCC.SPA.Abstractions;
using FCC.SPA.Models;
using FCC.Web;
using RestSharp;
using System.Text.Json;

namespace FCC.SPA.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClientService _restClientService;

        public ApiService(IConfiguration configuration, IRestClientService restClientService)
        {
            _configuration = configuration;
            _restClientService = restClientService;
        }

        public async Task<RestDtocs> ExecuteRequestAsync(InputParameters param)
        {
            (var restClient, var request) = GetRestObjects(param);
            if (param.Files != null)
            {
                param.Files.ForEach(file =>
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    request.AddFile("Files", fileBytes, file.FileName);
                });
                //request.AddParameter("ContainerFolderName", param.ContainerFolderName);
            }
            var response = await restClient.ExecuteAsync<Object>(request);
            var restDtocs = new RestDtocs();
            if (response != null && response.IsSuccessStatusCode)
                restDtocs.ResponseValue = response.Data;
            else
            {
                if (response != null && response.Content != null)
                    restDtocs.ResponseValue = JsonSerializer.Deserialize<ErrorResponse>(response.Content);
            }
            restDtocs.StatusCode = response?.StatusCode;
            return restDtocs;
        }

        public async Task<Stream?> DownloadStreamAsync(InputParameters param)
        {
            (var restClient, var request) = GetRestObjects(param);
            var stream = await restClient.DownloadStreamAsync(request);
            return stream;
        }

        public (RestClient, RestRequest) GetRestObjects(InputParameters param)
        {
            var baseUrl = GetBaseUrl(param.endpoint, param.apiType);
            var parameters = new List<KeyValuePair<string, string>>();
            var method = Enum.Parse<Method>(param.apiMethod.ToString());
            return (_restClientService.GetRestClient(), _restClientService.GetRestRequest(baseUrl,
                param.payload, parameters, new List<KeyValuePair<string, string>>(), method));
        }

        private string GetBaseUrl(string endpoint, string apiType)
        {
			var baseUrl = string.Empty;
			if (apiType != string.Empty)
			{
				if (apiType == Constants.IAC)
				{
					baseUrl = $"{_configuration[Keys.IACApiUrl]}{endpoint}";
				}
				else if (apiType == Constants.WorkManagement)
				{
					baseUrl = $"{_configuration[Keys.WorkManagementApiUrl]}{endpoint}";
				}
                else if (apiType == Constants.OrganizationManagement)
                {
                    baseUrl = $"{_configuration[Keys.OrganizationManagementApiUrl]}{endpoint}";
                }
            }
			return baseUrl;
		}
    }
}
