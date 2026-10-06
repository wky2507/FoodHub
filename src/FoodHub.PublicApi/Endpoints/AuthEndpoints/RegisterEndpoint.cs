using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.BlazorShared.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FoodHub.Domain.Entity.Identity;
using Microsoft.EntityFrameworkCore;
namespace FoodHub.PublicApi.Endpoints.AuthEndpoints
{
    public class RegisterEndpoint : EndpointBaseAsync.WithRequest<RegisterRequest>.WithActionResult<RegisterResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterEndpoint(UserManager<ApplicationUser> registerManager,RoleManager<IdentityRole> roleManager){

            _userManager = registerManager;

            _roleManager = roleManager;

        }

        [HttpPost("api/auth/register")]
        [AllowAnonymous]
        public override async Task<ActionResult<RegisterResponse>> HandleAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var response = new RegisterResponse(request.correlation());

            bool exsisting = await _userManager.Users.AnyAsync(u => u.PhoneNumber == request.Phone);

            if (exsisting)
            {
                response.Result = false;

                response.Message = "该手机号已经注册,注册失败!";

                return BadRequest(response);

            }

            var user = new ApplicationUser { UserName = request.UserName, Email = request.Email, PhoneNumber = request.Phone};

            var res = await _userManager.CreateAsync(user,request.Password);

            if (!res.Succeeded) {

                response.Result = false;

                response.Message = string.Join(";", res.Errors.Select(e => e.Description));

                return BadRequest(response);
            }

            const string defaultRole = RoleConstants.Roles.USER;

            if (!await _roleManager.RoleExistsAsync(defaultRole)) {

                await _roleManager.CreateAsync(new IdentityRole(defaultRole));
            
            }

            var roleResult = await _userManager.AddToRoleAsync(user, defaultRole);

            if (!roleResult.Succeeded) {

                //如果角色分配失败，直接返回错误    
                response.Result = false;

                response.Message = "用户创建成功，但角色分配失败" + string.Join(";", roleResult.Errors.Select(e => e.Description));

                return BadRequest(response);
            }
            
            response.Id = await _userManager.GetUserIdAsync(user);

            response.Result = true;

            response.Message = "注册成功";

            response.UserName = request.UserName;

            return Created("",response);

        }

    }
}
