using System.Net.Http;
using System.Text.Json;
using System.Text;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using WEB_153505_PIKHTOVNIKAVA.Services.ProductService;

namespace WEB_153505_PIKHTOVNIKAVA.Services.SeasonCategoryService
{
    public class ApiSeasonCategoryService : ISeasonCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiProductService> _logger;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiSeasonCategoryService(HttpClient httpClient,
                                   ILogger<ApiProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task<ResponseData<List<SeasonCategory>>> GetCategoryListAsync()
            {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}SeasonCategories/");


            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<List<SeasonCategory>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");

                    return new ResponseData<List<SeasonCategory>>(data: null, error_mess: $"Ошибка: {ex.Message}");
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode}");

            return new ResponseData<List<SeasonCategory>>(data: null, error_mess: $"Данные не получены от сервера. Error: {response.StatusCode}");
        }

    }
}
