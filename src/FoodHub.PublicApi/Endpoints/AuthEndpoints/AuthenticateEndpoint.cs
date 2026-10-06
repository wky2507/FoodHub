using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Infrastructure.Services.Jwt;
using FoodHub.PublicApi.AuthEndpoints;

namespace FoodHub.PublicApi.Endpoints.AuthEndpoints
{
    public class AuthenticateEndpoint : EndpointBaseAsync.WithRequest<AuthenticateRequest>.WithActionResult<AuthenticateResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IdentityTokenService _tokenService;
        public AuthenticateEndpoint(SignInManager<ApplicationUser> signInManager,
                                    IdentityTokenService tokenService,
                                    UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;

            _tokenService = tokenService;

            _userManager = userManager;
        }

        [HttpPost("api/auth/login")]
        [AllowAnonymous]
        public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
        {

            var response = new AuthenticateResponse(request.correlation());

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
            
                return BadRequest("用户名或者密码错误");

            }

            if (await _userManager.IsLockedOutAsync(user))
            {
             
                return BadRequest("用户已经被锁定，请稍后重试或者联系管理员!");

            }
           
            var mode = request.TokenStorageMode;

            var tokenResult = await _tokenService.GenerateTokenAsync(user, mode);

            response.TokenResult = tokenResult;

            return Ok(response);

        }
    }
}
