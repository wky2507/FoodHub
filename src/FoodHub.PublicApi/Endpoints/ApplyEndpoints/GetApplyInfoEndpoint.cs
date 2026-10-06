using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.PublicApi.Endpoints.ApplyEndpoints
{
    public class GetApplyInfoEndpoint : EndpointBaseAsync.WithRequest<GetMerchantApplicationsRequest>.WithActionResult<MerchantApplicantDto>
    {
        //数据库
        private readonly IReadRepository<MerchantApplication> _merchantApplicationRepository;

        //构造DI注入
        public GetApplyInfoEndpoint(IReadRepository<MerchantApplication> merchantApplicationRepository)
        {

            _merchantApplicationRepository = merchantApplicationRepository;
            
        }

        [HttpGet("api/apply/getApplyInfo")]
        [Authorize(Roles = RoleConstants.Roles.ADMINISTRATORS)]
        public override async Task<ActionResult<MerchantApplicantDto>> HandleAsync([FromQuery] GetMerchantApplicationsRequest request,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) {

                return BadRequest(ModelState);
            
            }

            var spec = new MerchantApplicantionPagedSpec(request.Page, request.PageSize, request.Status, request.Keyword);

            var countSpec = new MerchantApplicationCountSpec(request.Status, request.Keyword);

            var items = await _merchantApplicationRepository.ListAsync(spec, cancellationToken);

            var totalCount = await _merchantApplicationRepository.CountAsync(countSpec, cancellationToken);
            
            if (items == null) {

                return BadRequest("服务器异常"); 

            }
            
            var result = new PagedResult<MerchantApplicantDto>
            {
                Items = MapToDto.MapToDtoMerchantApplicant(items),

                Page = request.Page,

                PageSize = request.PageSize,

                TotalCount = totalCount,
                
            };

            return Ok(result);

        }
    }
}
