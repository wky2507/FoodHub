using FoodHub.BlazorShared.Response;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using FoodHub.BlazorShared.Dto;
using System.Security.Claims;
using FoodHub.Domain.Specifications;
using FoodHub.Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
namespace FoodHub.PublicApi.Endpoints.ApplyEndpoints
{
    public class SubmitApplyEndpoint : EndpointBaseAsync.WithRequest<ApplicantDto>.WithActionResult<ApplicantResponse>
    {


        private readonly IRepository<MerchantApplication> _applicationRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        public SubmitApplyEndpoint(IRepository<MerchantApplication> applicationRepository,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {

            _applicationRepository = applicationRepository;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        [Authorize(Roles = "USER")]
        [HttpPost("api/apply/applicant")]
        public override async Task<ActionResult<ApplicantResponse>> HandleAsync(ApplicantDto request, CancellationToken cancellationToken)
        {

            var response = new ApplicantResponse(request.correlation());

            if (!ModelState.IsValid)
            {

                response.Success = false;

                response.Message = "请求数据异常";

                return BadRequest(response);

            }

            var userId = HttpContext.User.FindFirstValue("sub");

            if (userId == null)
            {

                response.Success = false;

                response.Message = "请求数据异常";

                return BadRequest(response);
            }


            var applyMerchant = new MerchantApplication(userId, request.StoreName, request.Address, request.Phone, request.LicenseImage_Url, ApplicationStatus.Pending, DateTime.UtcNow);

            //var  applyMerchant = new MerchantApplication(userId, request.StoreName, request.Address, request.LicenseImage_Url, ApplicationStatus.Pending, DateTime.UtcNow);


            var contactModel = request.Contacts;


            foreach (var contact in contactModel)
            {

                if (string.IsNullOrWhiteSpace(contact.Name) || string.IsNullOrWhiteSpace(contact.Phone) || string.IsNullOrWhiteSpace(contact.ID_Number))
                {
                    response.Success = false;

                    response.Message = "联系人信息不完整";

                    return BadRequest(response);    
                
                }
                
                    applyMerchant.AddContact(contact.Name, contact.Phone, contact.ID_Number, contact.PersonRoles);

            }

                //找出来这个商铺申请
                var merchantApplication = await _applicationRepository.FirstOrDefaultAsync(new MerchantApplicationByUserIdSpec(userId), cancellationToken);


                if (merchantApplication != null  )
                {
                if (merchantApplication?.Status == ApplicationStatus.Pending)
                {

                    response.Success = false;

                    response.Message = "您上一个提交的商户的申请还在审核中，请勿重复提交!";

                    return BadRequest(response);
                }
                }

                var result = await _applicationRepository.AddAsync(applyMerchant,cancellationToken);

                if (result == null)
                {

                    response.Success = false;

                    response.Message = "请求数据异常";

                    return BadRequest(response);
                }

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    response.Success = false;

                    response.Message = "请求失败";

                    return NotFound(response);
                }
                const string applicantRole = RoleConstants.Roles.APPLICANT;

                if (!await _roleManager.RoleExistsAsync(applicantRole)) {

                    await _roleManager.CreateAsync(new IdentityRole(applicantRole));
            
                }
               
               var addToRoleRes = await _userManager.AddToRoleAsync(user, applicantRole);

                if (!addToRoleRes.Succeeded) {

                    response.Success = false;

                    response.Message = "角色分配失败!";
                
                    return BadRequest(response);
                 }

                response.Success = true;

                response.Message = "申请已经提交，等候管理员审核";

                return Ok(response);

        }
    }
}
