using System.Text;
using System.Text.Json;
using intproje.Mvc.Models; // JobPost modelini tanıması için şart

namespace intproje.Mvc.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _httpClient = httpClientFactory.CreateClient();
            
            if (environment.IsDevelopment())
            {
                _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5101/api";
            }
            else
            {
                _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7188/api";
            }
            
            if (!_apiBaseUrl.EndsWith("/"))
            {
                _apiBaseUrl += "/";
            }
            
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
        }

        public async Task<List<T>?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return null;
            }
            catch (Exception) { return null; }
        }

        public async Task<T?> GetByIdAsync<T>(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync($"{endpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return default;
        }

        public async Task<T?> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return default;
            }
            catch (Exception) { return default; }
        }

        public async Task<bool> PutAsync<T>(string endpoint, int id, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{endpoint}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint, int id)
        {
            var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        // ÖZEL METODLAR - Sınıfın (ApiService) içinde kalmalı
        public async Task<List<T>?> GetApplicantsByJobAsync<T>(int jobId)
        {
            return await GetAsync<T>($"Applicants/ByJob/{jobId}");
        }

        // HomeController'da hata veren metod tam olarak buraya gelmeli
        public async Task<List<JobPost>?> GetJobPostsAsync()
        {
            return await GetAsync<JobPost>("Jobs");
        }

    } // ApiService sınıfı burada biter
} // Namespace burada biter