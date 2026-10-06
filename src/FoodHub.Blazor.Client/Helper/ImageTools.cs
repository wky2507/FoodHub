using FoodHub.Blazor.Client.ApiService.Merchant;
using FoodHub.Blazor.Client.Interfaces;
using FoodHub.Blazor.Client.Services;
using FoodHub.BlazorShared.Dto;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using System.Net;

namespace FoodHub.Blazor.Client.Helper
{
    public class ImageTools : IImageTools
    {

        private readonly MerChantService _merChantService;

        private readonly ToastService _toastService;

        private readonly PathTools _pathTools;

        private readonly HttpService _httpService;

        private readonly IOptions<BaseUrlConfiguration> _options;

        private readonly string _apiUri;
        public ImageTools(MerChantService merChantService, ToastService toastService, PathTools pathTools, HttpService httpService,IOptions<BaseUrlConfiguration> options) {

            _merChantService = merChantService;

            _toastService = toastService;

            _pathTools = pathTools;

            _httpService = httpService;

            _options = options;

            _apiUri = options.Value.ApiBase;
        }


        public async Task<HttpCallResultDto<UploadResultDto>> UploadDishesImage(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (file is null)
            {
                _toastService.ShowError("文件上传失败,请重试");

                return FailResult;
            }

            long maxFileSize = 1024 * 1024 * 5;

            if (file.Size > maxFileSize)
            {

                _toastService.ShowError("图片大小不可以超过5MB,请重新上传");

                return FailResult;
            }

            try
            {
                using var content = new MultipartFormDataContent();

                //把流包装成HttoContent内容
                var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                content.Add(fileContent, "file", file.Name);

                var path = _pathTools.DishesImageUploadCombine();

                //上传图片
                var response = await _httpService.HttpPost(path, content);

                if (response.IsSuccessStatusCode)
                {
                   var data = await _httpService.FromHttpResponseMessage<UploadResultDto>(response);

                    if (data == null) {

                        _toastService.ShowError("解析出错");
                        
                        throw new InvalidOperationException();
                    }

                    return SuccessResult<UploadResultDto>(data, response.StatusCode);

                }
                else
                {

                   _toastService.ShowError("图片上传失败!");

                    return FailResult;
                }

            }
            catch (Exception ex)
            {
                _toastService.ShowError($"出现异常{ex.Message}");

                return FailResult;
            }
           
        }

        public async Task<HttpCallResultDto<UploadResultDto>> UploadLicenseImage(InputFileChangeEventArgs e) {

            var file = e.File;

            if (file == null) {

                return FailResult;
            
            }

            int maxSize = 1024 * 1024 * 5;

            if (file.Size > maxSize) {

                return FailResult;

            }

            try {

                using var content = new MultipartFormDataContent();

                var fileContent = new StreamContent(file.OpenReadStream(maxSize));

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                content.Add(fileContent, "file", file.Name);
                //发送请求的路径
                var path = _pathTools.LisenceImageCombine();

                //上传照片
                var response = await _httpService.HttpPost(path, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await _httpService.FromHttpResponseMessage<UploadResultDto>(response);

                    if (data == null)
                    {
                        return FailResult;
                    }

                    return SuccessResult<UploadResultDto>(data, response.StatusCode);

                }
                else {

                    return FailResult;
                }
            }
            catch (Exception) {

                return FailResult;

            }

           

        }

        public async Task DeleteImage(ProductDto productDto)
        {
            try
            {

                var fileName = Path.GetFileName(productDto.PictureUri);

                if (fileName == null) {

                    return;

                }

                var request = new DeleteFoodImageRequest(fileName, productDto.Id);

                var result = await _merChantService.DeleteFoodImageTemproryAsync(request);

                if (!result.IsSuccess)
                {

                    _toastService.ShowError("删除图片失败");

                    return;
                }

                _toastService.ShowSuccess("删除成功!");
            }
            catch (ArgumentNullException)
            {

                _toastService.ShowError("请求参数不能为空");

            }
            catch (Exception ex)
            {

                _toastService.ShowError($"{ex.Message}");

            }
            finally
            {

                productDto.PictureUri = string.Empty;

            }

        }

        public HttpCallResultDto<UploadResultDto> FailResult = new HttpCallResultDto<UploadResultDto>
        {

            IsSuccess = false

        };

        public HttpCallResultDto<T> SuccessResult<T>(T data,HttpStatusCode statusCode) where T : new(){

            return new HttpCallResultDto<T>
            {
                Data = data,

                IsSuccess = true,

                StatusCode = statusCode
            };

        }

        public async Task<HttpResponseMessage> GetLicenseImageAsync(string uri) {
            
            if (string.IsNullOrWhiteSpace(uri)) return null;

            var encodedFileName = Path.GetFileName(uri);
            //$"download/license/{encodedFileName}"
            uri = $"{_apiUri}download/license/{encodedFileName}";

            var response = await _httpService.HttpGet(uri);

            return response;
        
        }
        public async Task<HttpResponseMessage> GetDishesImageAsync(string uri){

            if (string.IsNullOrWhiteSpace(uri)) return null;

            var encodedFileName = Path.GetFileName(uri);

            uri = $"{_apiUri}download/dishesImage/{encodedFileName}";

            var response = await _httpService.HttpGet(uri);

            return response;

        }

    }
}
