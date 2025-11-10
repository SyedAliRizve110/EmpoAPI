using Consul;
using Empo.Shared.Utility.Constants;
using Empo.Shared.Utility.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Serilog;
using System.Net.Http.Headers;
using System.Text;

namespace Empo.Shared.Utility.ServiceDiscovery
{
    public class ServiceDiscoveryAPIClient
    {
        private readonly List<Uri> _serverUrls;
        private readonly HttpClient _apiClient;
        private AsyncRetryPolicy _serverRetryPolicy;
        //private readonly string _serviceDiscoveryAdress;
        //private readonly string _requestedServiceName;
        private int _currentConfigIndex;
        readonly ILogger _logger;
        private IHttpContextAccessor _httpContextAccessor;
        public IConsulClient _consulClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDiscoveryAdress">Service discovery base address mentioned in docker-compose.override.yml file</param>
        /// <param name="requestedServiceName">Service Name to be requested </param>
        public ServiceDiscoveryAPIClient(ILogger logger, IHttpContextAccessor httpContextAccessor, IConsulClient consulClient)
        {
            _apiClient = new HttpClient();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationName.MediaTypeJson));
            _serverUrls = new List<Uri>();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _consulClient = consulClient;
        }
        //public ServiceDiscoveryAPIClient(string serviceDiscoveryAdress, string requestedServiceName)
        //{
        //    _apiClient = new HttpClient();
        //    _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationName.MediaTypeJson));
        //    _serverUrls = new List<Uri>();
        //    _serviceDiscoveryAdress = serviceDiscoveryAdress;
        //    _requestedServiceName = requestedServiceName;
        //}
        public async Task Initialize(string requestedServiceName)
        {
            //ConsulClient consulClient = new ConsulClient(c =>
            //{
            //    var uri = new Uri(_serviceDiscoveryAdress);
            //    c.Address = uri;
            //});

            _logger.Information("Discovering Services from Consul.");
            _serverUrls.Clear();
            QueryResult<Dictionary<string, AgentService>> services = await _consulClient.Agent.Services();
            if (services != null && services.Response != null)
            {
                foreach (var service in services.Response)
                {
                    bool isServiceApi = service.Value.Service == requestedServiceName;
                    if (isServiceApi)
                    {
                        Uri serviceUri = new Uri($"{service.Value.Address}:{service.Value.Port}/");
                        _serverUrls.Add(serviceUri);
                    }
                }
            }

            _logger.Information($"{_serverUrls.Count} endpoints found.");
            int retries = _serverUrls.Count * 2 - 1;
            _logger.Information($"Retry count set to {retries}");

            _serverRetryPolicy = Polly.Policy.Handle<HttpRequestException>()
               .RetryAsync(retries, (exception, retryCount) =>
               {
                   ChooseNextServer(retryCount);
               });
        }
        private void ChooseNextServer(int retryCount)
        {
            if (retryCount % 2 == 0)
            {
                _logger.Warning("Trying next server... \n");
                _currentConfigIndex++;

                if (_currentConfigIndex > _serverUrls.Count - 1)
                    _currentConfigIndex = 0;
            }
        }
        public Task<T> GetResponse<T>(string requestedServiceName, string apiPath, HttpMethodType httpMethodType, object postData = null)
        {
            this.Initialize(requestedServiceName).Wait();
            return _serverRetryPolicy.ExecuteAsync(async () =>
            {
                Uri serverUrl = _serverUrls[_currentConfigIndex];
                T returnValue = default;
                HttpContent postDataContent = null;
                string requestPath = $"http://{serverUrl}{apiPath}";
                _logger.Information($"Making request to {requestPath}");
                HttpResponseMessage response = null;
                foreach (var header in _httpContextAccessor.HttpContext.Request.Headers)
                {
                    if ((header.Key == "culture-code" || header.Key == "tenant-id") && _apiClient.DefaultRequestHeaders.Contains(header.Key) == false)
                    {
                        string _value = header.Value;
                        _apiClient.DefaultRequestHeaders.Add(header.Key, _value);
                    }
                }
                switch (httpMethodType)
                {
                    case HttpMethodType.Get:
                        response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
                        break;
                    case HttpMethodType.Post:
                        postDataContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, ApplicationName.MediaTypeJson);
                        response = await _apiClient.PostAsync(requestPath, postDataContent).ConfigureAwait(false);
                        break;
                    case HttpMethodType.Delete:
                        response = await _apiClient.DeleteAsync(requestPath).ConfigureAwait(false);
                        break;
                    case HttpMethodType.Put:
                        postDataContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, ApplicationName.MediaTypeJson);
                        response = await _apiClient.PutAsync(requestPath, postDataContent).ConfigureAwait(false);
                        break;
                    case HttpMethodType.FormUrlEncoded:
                        response = await _apiClient.PostAsync(requestPath, (FormUrlEncodedContent)postData).ConfigureAwait(false);
                        break;

                }
                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrEmpty(result))
                    {
                        var data = JsonConvert.DeserializeObject<ApiResponse>(result).Data.ToString();
                        returnValue = JsonConvert.DeserializeObject<T>(data);
                    }

                }
                else
                {
                    _logger.Error($"Error received {response?.ReasonPhrase}{response?.RequestMessage}");
                }
                return returnValue;
            });
        }
        public Task<bool> CallAsync(string requestedServiceName, string apiPath, HttpMethodType httpMethodType, object postData = null)
        {
            this.Initialize(requestedServiceName).Wait();
            return _serverRetryPolicy.ExecuteAsync(async () =>
            {
                Uri serverUrl = _serverUrls[_currentConfigIndex];
                HttpContent postDataContent = null;
                string requestPath = $"http://{serverUrl}{apiPath}";
                _logger.Information($"Making request to {requestPath}");
                HttpResponseMessage response = null;
                switch (httpMethodType)
                {
                    case HttpMethodType.Get:
                        response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
                        break;
                    case HttpMethodType.Post:
                        postDataContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, ApplicationName.MediaTypeJson);
                        response = await _apiClient.PostAsync(requestPath, postDataContent).ConfigureAwait(false);
                        break;
                    case HttpMethodType.Delete:
                        response = await _apiClient.DeleteAsync(requestPath).ConfigureAwait(false);
                        break;
                    case HttpMethodType.Put:
                        postDataContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, ApplicationName.MediaTypeJson);
                        response = await _apiClient.PutAsync(requestPath, postDataContent).ConfigureAwait(false);
                        break;

                }
                if (response != null && response.IsSuccessStatusCode)
                {
                    //string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return true;
                }
                else
                {
                    _logger.Error($"Error received {response.ReasonPhrase}{response.RequestMessage}");
                    return false;
                }
            });
        }
        public Task<byte[]> GetImageAsByteArray(string requestedServiceName, string imageApiPath)
        {
            this.Initialize(requestedServiceName).Wait();
            return _serverRetryPolicy.ExecuteAsync(async () =>
            {
                Uri serverUrl = _serverUrls[_currentConfigIndex];
                string requestPath = $"http://{serverUrl}/{imageApiPath}";
                _logger.Information($"Making request to {requestPath}");
                HttpResponseMessage response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
                byte[] byteArray = default;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
                    var data = JsonConvert.DeserializeObject<ApiResponse>(result).Data.ToString();
                    byteArray = Convert.FromBase64String(data);
                }
                else
                {
                    _logger.Error($"Error received {response?.ReasonPhrase}{response?.RequestMessage}");
                }
                return byteArray;
            });
        }
    }
}
