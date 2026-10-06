using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.BlazorShared.Response;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodHub.PublicApi.Endpoints.ApplyEndpoints
{
    public class RejectApplyEndpoint : EndpointBaseAsync.WithRequest<RejectApplyRequest>.WithActionResult<RejectApplyResponse>
    {

        private readonly ILogger<RejectApplyEndpoint> _logger;

        private readonly IRepository<MerchantApplication> _merchantApplicationRepository;

        private readonly UserManager<ApplicationUser> _userManager;
        public RejectApplyEndpoint(ILogger<RejectApplyEndpoint> logger,IRepository<MerchantApplication> merchantApplicationRepository,UserManager<ApplicationUser> userManager) {

            _logger = logger;

            _merchantApplicationRepository = merchantApplicationRepository;

            _userManager = userManager;

        }
        [Authorize(Roles = RoleConstants.Roles.ADMINISTRATORS)]
        [HttpPost("api/apply/rejectApply")]
        public override async Task<ActionResult<RejectApplyResponse>> HandleAsync(RejectApplyRequest request,CancellationToken cancellationToken) {

            var response = new RejectApplyResponse();

            if (!ModelState.IsValid) {

                response.IsSuccess = false;

                //response.Message = "服务端异常";

                throw new ArgumentException("服务端异常");
            }

            if (request.Status != ApplicationStatus.Pending) {

                response.IsSuccess = false;

                //response.Message = "请求异常";

                throw new InvalidOperationException("请求异常");

            }

          var merchantapply =  await _merchantApplicationRepository.GetByIdAsync(request.Id, cancellationToken);

           if (merchantapply == null) {

                response.IsSuccess = false;

                response.Message = "服务异常";

                return BadRequest(response);
            }

            var result = merchantapply.Reject(request.ReviewComment);

            if (!result) {
                
                response.IsSuccess = false;

                response.Message = "评论不能为空";

                return BadRequest(response);
            }

           int num =  await _merchantApplicationRepository.UpdateAsync(merchantapply, cancellationToken);

            if (!(num > 0)) {
            
                response.IsSuccess = false;

                throw new DbUpdateException("服务异常");
            }

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return BadRequest("服务器异常!");
            }

            var listRole =  await _userManager.GetRolesAsync(user);

            if (listRole.Contains(RoleConstants.Roles.APPLICANT)) {

                var removeRoleRes = await _userManager.RemoveFromRoleAsync(user, RoleConstants.Roles.APPLICANT);

                if (!removeRoleRes.Succeeded) {

                    throw new InvalidOperationException("服务器异常");
                
                }
            }

            response.IsSuccess = true;

            response.Message = "操作成功";

            return Ok(response);
        }

    }
}
