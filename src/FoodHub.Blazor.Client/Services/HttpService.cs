using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using System.Net;
using FoodHub.BlazorShared.Dto;

namespace FoodHub.Blazor.Client.Services
{
    public class HttpService
    {
       
        private HttpClient _httpClient;
       
        private readonly string _apiUrl;

        private ILogger<HttpService> _logger;


        public HttpService(HttpClient httpClient, IOptions<BaseUrlConfiguration> options, ILogger<HttpService> logger)
        {
            _httpClient = httpClient;

            _apiUrl = options.Value.ApiBase;

            _logger = logger;

        }

        public async Task<HttpCallResultDto<T>> HttpPost<T>(string uri, object? dataToSend) where T : class,new ()
        {
            try
            {

                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_apiUrl}{uri}");

                if (dataToSend != null)
                {
                    request.Content = ToJson(dataToSend);
                }

                using var response = await _httpClient.SendAsync(request);

                var result = new HttpCallResultDto<T>
                {
                    IsSuccess = response.IsSuccessStatusCode,

                    //Data

                    StatusCode = response.StatusCode,

                    Message = await response.Content.ReadAsStringAsync()
                };

                if (response.IsSuccessStatusCode)
                {
                    var res = await FromHttpResponseMessage<T>(response);

                    result.Data = res;

                }

                return result;
              
            }
            catch (Exception e) {

                _logger.LogError(e.Message);

                return new HttpCallResultDto<T>();
            }
            }

        public async Task<HttpResponseMessage> HttpPost(string path, MultipartFormDataContent? data)
        {

            using var request = new HttpRequestMessage(HttpMethod.Post, path);

            if (data != null)
            {
                request.Content = data;
            }

            var response = await _httpClient.SendAsync(request);

            return response;

        }

        public async Task<HttpCallResultDto<T>> HttpGet<T>(string uri,object? dataToSend) where T : class,new()
        {

            var url = $"{_apiUrl}{uri}";
            
            var queryString = BuildQueryString(dataToSend);

            if (!string.IsNullOrEmpty(queryString)) {

                url += $"?{queryString}";

            }

            var request = new HttpRequestMessage(

                HttpMethod.Get,

                url
                );

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) {

                var failRes = new HttpCallResultDto<T>
                {

                    IsSuccess = false,

                    StatusCode = response.StatusCode,

                    Message = await response.Content.ReadAsStringAsync()
                
                         
                };
                return failRes;
            }

            var data =  await FromHttpResponseMessage<T>(response);

            var sucRes = new HttpCallResultDto<T>
            {

                Data = data,

                IsSuccess = true,

                StatusCode = response.StatusCode,

                Message = await response.Content.ReadAsStringAsync()
            };

            return sucRes;

        }

        public Task<HttpCallResultDto<T>> HttpDelete<T>(string uri, object? dataToSend = null) where T : class,new()
            => SendUriRequestAsync<T>(HttpMethod.Delete, uri, dataToSend);

        public Task<HttpCallResultDto<T>> HttpPut<T>(string uri, object dataToSend) where T : class,new()
           => SendBodyRequestAsync<T>(HttpMethod.Put, uri, dataToSend);
        public StringContent ToJson( object obj) {

            return new StringContent(JsonSerializer.Serialize(obj),Encoding.UTF8,"application/json");

        }

        public async Task<T?> FromHttpResponseMessage<T>(HttpResponseMessage result) where T : class,new()
        {
            try
            {
                if (!result.IsSuccessStatusCode && result.StatusCode != HttpStatusCode.BadRequest)
                {

                    var errorContent = await result.Content.ReadAsStringAsync();

                    Console.WriteLine($"请求失败，状态码:{result.StatusCode},错误内容:{errorContent}");

                    return default;

                }

                var json = await result.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(json))
                {

                    return default;

                }

                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception e) {

                _logger.LogError(e.Message);

                return new T();
            }
        }

        private static string BuildQueryString(object? data) {
            
            if (data == null) {

                return string.Empty;

            }

            var properties = data.GetType().GetProperties();

            var queryParameters = properties
                .Where(p => p.GetValue(data) != null)
                .Select(p =>
                {

                    var value = p.GetValue(data);

                    return $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(value!.ToString()!)}";
                });

            return string.Join("&", queryParameters);
        }

        private async Task<HttpCallResultDto<T>> SendUriRequestAsync<T>(HttpMethod method,string uri,object? dataToSend) where T : class,new()
        {
            var url = $"{_apiUrl}{uri}";

            var queryString = BuildQueryString(dataToSend);

            if (!string.IsNullOrEmpty(queryString)) {
                //拼接参数在路径后面
                url += $"?{queryString}";
                
            }

           using var request = new HttpRequestMessage(method, url);

           using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) {

                return new HttpCallResultDto<T>
                {

                    IsSuccess = false,

                    Message = await response.Content.ReadAsStringAsync(),

                    StatusCode = response.StatusCode,

                };

            }
            
            var data = await FromHttpResponseMessage<T>(response);

            return new HttpCallResultDto<T>
            {

                IsSuccess = true,

                Data = data,

                StatusCode = response.StatusCode,

                Message = await response.Content.ReadAsStringAsync()

            };

        }

        private async Task<HttpCallResultDto<T>> SendBodyRequestAsync<T>(HttpMethod method, string uri, object? dataToSend) where T : class,new() {

            var url = $"{_apiUrl}{uri}";

            using var request = new HttpRequestMessage(method,url);

            if (dataToSend != null) {

                request.Content = ToJson(dataToSend);
            
            }

            using var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode) {

                return new HttpCallResultDto<T>
                {

                    Data = await FromHttpResponseMessage<T>(response),

                    IsSuccess = true,

                    Message = await response.Content.ReadAsStringAsync(),

                    StatusCode = response.StatusCode
                };

            }
            else {

                return new HttpCallResultDto<T>
                {

                    IsSuccess = false,

                    Message = await response.Content.ReadAsStringAsync(),

                    StatusCode = response.StatusCode
                };
            }


        }

        public async Task<HttpResponseMessage> HttpGet(string uri) {

            var fileName = Path.GetFileName(uri);

            var encodedFileName = Uri.EscapeDataString(fileName);

            using var request = new HttpRequestMessage(
                HttpMethod.Get,

                uri
                );

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) {
                return null;
            }

            return response;

        }

       
    }
}
