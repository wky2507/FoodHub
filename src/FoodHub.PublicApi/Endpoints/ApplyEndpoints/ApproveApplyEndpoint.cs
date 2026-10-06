using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FoodHub.Domain.Entity.StoreEntity;
using Microsoft.AspNetCore.Authorization;
namespace FoodHub.PublicApi.Endpoints.ApplyEndpoints
{
    public class ApproveApplyEndpoint : EndpointBaseAsync.WithRequest<ApproveRequest>.WithActionResult
    {
        private readonly IRepository<MerchantApplication> _merchantRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Store> _storeRepository;
        public ApproveApplyEndpoint(IRepository<MerchantApplication> merchantRepository,UserManager<ApplicationUser> userManager,IRepository<Store> storeRepository) {

            _merchantRepository = merchantRepository;

            _userManager = userManager;

            _storeRepository = storeRepository;
        }
        [HttpPost("api/apply/approveApply")]
        [Authorize(Roles = RoleConstants.Roles.ADMINISTRATORS)]
        public override async Task<ActionResult> HandleAsync(ApproveRequest request,CancellationToken cancellationToken) {

            if (!ModelState.IsValid) {

                throw new ArgumentException("请求异常");

            }

            var merchantApply = await _merchantRepository.GetByIdAsync(request.Id, cancellationToken);

            if (merchantApply == null) {

                return NotFound("未知的申请");

            }

            if (!(request.Status == ApplicationStatus.Pending)) {

                return BadRequest("该申请状态异常");

            }

            merchantApply.Approve();

            int num = await _merchantRepository.UpdateAsync(merchantApply, cancellationToken);

            if (!(num > 0)) {

                throw new InvalidOperationException("服务器异常");
            
            }

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null) {

                return BadRequest("服务器异常");
            
            }

           var listRole = await _userManager.GetRolesAsync(user);

            if (!listRole.Contains(RoleConstants.Roles.MERCHANT) && listRole.Contains(RoleConstants.Roles.APPLICANT))
            {

                var addRoleRes = await _userManager.AddToRoleAsync(user, RoleConstants.Roles.MERCHANT);

                var removeRoleRes = await _userManager.RemoveFromRoleAsync(user, RoleConstants.Roles.APPLICANT);

                if(!addRoleRes.Succeeded || !removeRoleRes.Succeeded) {

                        throw new InvalidOperationException("服务器异常");
            
                   }

                var store = new Store(merchantApply.UserId, merchantApply.StoreName, merchantApply.Phone, merchantApply.Address, merchantApply.LicenseImage_Url, true);

                var result = await _storeRepository.AddAsync(store, cancellationToken);

                return Ok();
            }


            //var store = new Store(merchantApply.UserId, merchantApply.StoreName, merchantApply.Address, merchantApply.LicenseImage_Url, true);

            return BadRequest();

           
        }


    }
}
