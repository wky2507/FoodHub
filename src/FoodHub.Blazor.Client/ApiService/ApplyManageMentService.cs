

using FoodHub.Blazor.Client.Services;
using FoodHub.BlazorShared.Dto;
using FoodHub.BlazorShared.Request;
using FoodHub.BlazorShared.Response;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FoodHub.Blazor.Client.ApiService
{
    public class ApplyManageMentService
    {

        private readonly HttpService _httpService;

        private readonly HttpClient _httpClient;

        private IWebAssemblyHostEnvironment _enviroment;
        public ApplyManageMentService(HttpService httpService,HttpClient httpClient,IWebAssemblyHostEnvironment environment)
        {
            _httpService = httpService;

            _httpClient = httpClient;

            _enviroment = environment;
        }

        public async Task<HttpCallResultDto<PagedResult<MerchantApplicantDto>>> GetApplyInfo(GetMerchantApplicationsRequest request)
        {

            if (request == null) {

                throw new ArgumentNullException("请求参数为空");

            }
            var response = await _httpService.HttpGet<PagedResult<MerchantApplicantDto>>("apply/getApplyInfo/", request);

            return response;
        }

        public async Task<HttpCallResultDto<RejectApplyResponse>> RejectApply(RejectApplyRequest request) {

            if (request == null) {

                throw new ArgumentNullException("请求参数异常");
            
            }

            var response =await _httpService.HttpPost<RejectApplyResponse>("apply/rejectApply/", request);

            return response;

        }

        public async Task<HttpCallResultDto<object>> ApproveApply(ApproveRequest request) {

            if (request == null) {

                throw new ArgumentNullException("请求参数异常");
            
            }

            var response = await _httpService.HttpPost<object>("apply/approveApply/", request);

            return response;
        }

    }
}
