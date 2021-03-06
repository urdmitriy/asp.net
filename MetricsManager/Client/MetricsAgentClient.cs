using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsManager
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public AllHddMetricsResponse GetHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.LocalDateTime.ToString("O");
            var toParameter = request.ToTime.LocalDateTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/hdd/left/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public AllRamMetricsResponse GetRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.LocalDateTime.ToString("O");
            var toParameter = request.ToTime.LocalDateTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/ram/available/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllCpuMetricsResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.LocalDateTime.ToString("O");
            var toParameter = request.ToTime.LocalDateTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
            
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;


                var res = JsonSerializer.DeserializeAsync<AllCpuMetricsResponse>(responseStream,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}).Result;
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllDotNetMetricsResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.LocalDateTime.ToString("O");
            var toParameter = request.ToTime.LocalDateTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/dotnet/errors-count/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllNetworkMetricsResponse GetNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.LocalDateTime.ToString("O");
            var toParameter = request.ToTime.LocalDateTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/network/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;


                return JsonSerializer.DeserializeAsync<AllNetworkMetricsResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
