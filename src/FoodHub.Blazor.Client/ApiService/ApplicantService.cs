using FoodHub.BlazorShared.Dto;
using FoodHub.BlazorShared.Request;
using FoodHub.BlazorShared.Response;
using FoodHub.Blazor.Client.Services;

namespace FoodHub.Blazor.Client.ApiService
{

    public class ApplicantService
    {
        private readonly HttpService _httpService;

        private readonly ILogger<ApplicantService> _logger;
        public ApplicantService(HttpService httpService, ILogger<ApplicantService> logger) {

            _httpService = httpService;

            _logger = logger;

        }

        public async Task<HttpCallResultDto<ApplicantResponse>>? ApplicantSubmit(ApplicantDto request) {

            var response = new ApplicantResponse();

            if (request == null) {

                _logger.LogError( "请求参数异常");

                throw new ArgumentNullException(nameof(request), "请求参数为空");
            }

            var res =  await _httpService.HttpPost<ApplicantResponse>("apply/applicant/", request);

            return res;
        
        }



    }



}
