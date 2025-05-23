﻿using LPRSystem.Web.Utility;
using Microsoft.Extensions.Options;

namespace LPRSystem.Web.UI.Factory
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(IOptions<LPRSystemConfig> coreConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            var coreConfigValue = coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfigValue.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = TimeSpan.FromSeconds(60);
        }
        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
