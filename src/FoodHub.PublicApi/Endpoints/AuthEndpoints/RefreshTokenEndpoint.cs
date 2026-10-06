using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Interface;
using FoodHub.Infrastructure.Model;
using FoodHub.Infrastructure.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FoodHub.PublicApi.Endpoints.AuthEndpoints
{
    public class RefreshTokenEndpoint : EndpointBaseAsync.WithRequest<RefreshTokenReuqest>.WithActionResult<TokenResultDto>
    {
        private readonly IdentityTokenService _tokenService ;

        private readonly IRepository<AppUserRefreshToken> _repositoryBase;

        public RefreshTokenEndpoint(IdentityTokenService tokenService ,IRepository<AppUserRefreshToken> repositoryBase) {

            _tokenService = tokenService;

            _repositoryBase = repositoryBase;

        }

        //RefreshTokenRequest是请求DTO，但是我这里没有对应匹配的响应DTO，用TokenResult充当响应DTO
        [HttpPost("api/auth/refresh")]
        [AllowAnonymous]
        public override async Task<ActionResult<TokenResultDto>> HandleAsync(RefreshTokenReuqest refreshTokenReuqest, CancellationToken cancellationToken)
        {
            var refreshTokenDto = new RefreshTokenDto
            {

                RefreshToken = refreshTokenReuqest.RefreshToken,

                TokenStorageMode = refreshTokenReuqest.TokenStorageMode
            };
           
            var tokenResult = await _tokenService.RefreshTokenAsync(refreshTokenDto);


            if (tokenResult == null)
            {
                //401
                return Unauthorized("令牌过期失效或者不存在!");

            }

            return Ok(tokenResult);
        }
          

    }
}
