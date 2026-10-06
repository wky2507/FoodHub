using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.BlazorShared.Response;
using Microsoft.AspNetCore.Mvc;
using FoodHub.BlazorShared;
using FoodHub.Domain.Interface;
using FoodHub.Domain.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace FoodHub.PublicApi.Endpoints.AuthEndpoints
{
    public class LogoutEndPoint :EndpointBaseAsync.WithRequest<LogoutRequest>.WithActionResult<BaseApiResponse<LogoutResponse>>
    {

        private readonly IRepository<AppUserRefreshToken> _repository;

        private readonly ILogger<LogoutEndPoint> _logger;
        public LogoutEndPoint(IRepository<AppUserRefreshToken> repository, ILogger<LogoutEndPoint> logger)
        {

            _repository = repository;

            _logger = logger;

        }

        [HttpPost("api/auth/logout")]
        //[AllowAnonymous]
        [Authorize]
        //[Authorize]
        public override async Task<ActionResult<BaseApiResponse<LogoutResponse>>> HandleAsync(LogoutRequest request, CancellationToken cancellationToken = default)
        {

            if (request == null)
            {


                return BadRequest(new BaseApiResponse<LogoutResponse>
                {

                    Code = 400,

                    Message = "请求参数错误，无效!",

                    Data = new LogoutResponse(false)

                });
                //return Task.FromResult<ActionResult<BaseApiResponse<LogoutResponse>>>(resBad);
     
            }

            string? header = HttpContext.Request.Headers.Authorization.FirstOrDefault();

            string? accessToken = null;

            if (!string.IsNullOrEmpty(header) && header.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase)) {

                accessToken = header.Substring("Bearer".Length).Trim();
            
            }

            var userId = HttpContext.User.FindFirstValue("sub");

            var refreshTokenFromRequest = request.RefreshToken;

            if ( !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(refreshTokenFromRequest)) {

                var specification = new RefreshTokenByUserIdSpecification(userId, refreshTokenFromRequest);

                var appUserRefreshTokenEntity = await _repository.FirstOrDefaultAsync(specification,cancellationToken);
                
                if (appUserRefreshTokenEntity == null) {

                    return Ok(new BaseApiResponse<LogoutResponse>
                    {
                        Code = 200,

                        Message = "没有需要吊销的长期会话凭证",

                        Data = new LogoutResponse(true)
                    });
                     

                }
                appUserRefreshTokenEntity.Revoke(true, DateTime.UtcNow);

                await _repository.SaveChangesAsync();
            }

            return Ok(new BaseApiResponse<LogoutResponse>
            {
                Code = 200,

                Message = "退出登录成功",

                Data = new LogoutResponse(true)

            });

            //return Task.FromResult<ActionResult<BaseApiResponse<LogoutResponse>>>(resOk);


        }

    }
}
