using FoodHub.BlazorShared.Dto;
using Microsoft.AspNetCore.Components.Forms;

namespace FoodHub.Blazor.Client.Interfaces
{
    public interface IImageTools
    {
        Task<HttpCallResultDto<UploadResultDto>> UploadDishesImage(InputFileChangeEventArgs e);

        Task DeleteImage(ProductDto productDto);

        Task<HttpCallResultDto<UploadResultDto>> UploadLicenseImage(InputFileChangeEventArgs e);

        //下载License
        Task<HttpResponseMessage> GetLicenseImageAsync(string uri);

        //下载Dishes
        Task<HttpResponseMessage> GetDishesImageAsync(string uri);
    }
}
