#nullable disable
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using FoodHub.BlazorShared.Dto;
using System.Security.Cryptography;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Specifications;
using FoodHub.Infrastructure.Model;
namespace FoodHub.Infrastructure.Services.Jwt
{
    public class IdentityTokenService 
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly JwtOptions _jwtOptions;

        private readonly IRepository<AppUserRefreshToken> _repository;

        public IdentityTokenService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions,IRepository<AppUserRefreshToken> repository)
        {

            _userManager = userManager;

            _jwtOptions = jwtOptions.Value;

            _repository = repository;

        }

        public async Task<TokenResultDto> GenerateTokenAsync(ApplicationUser user, TokenStorageMode mode)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim> {

                new Claim("sub",user.Id),

                //new Claim("email",user.Email),

                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),

                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            //DateTime.UtcNow.AddDays(7)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinute),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = GenerateRefreshToken();

            var refreshTokenExpire = new DateTime();

            if (mode == TokenStorageMode.Local)
            {

                 refreshTokenExpire = DateTime.UtcNow.AddDays(7);

            }

            else {

                refreshTokenExpire = DateTime.UtcNow.AddDays(1);
            
            }

            var appUserRefreshTokenEntity = new AppUserRefreshToken(user.Id, refreshToken, false, DateTime.UtcNow, refreshTokenExpire);

            await _repository.AddAsync(appUserRefreshTokenEntity, default);

            return new TokenResultDto
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken,

                AccessTokenExpire = tokenDescriptor.Expires!.Value,

                RefreshTokenExpire = refreshTokenExpire
            };

        }

        public async Task<TokenResultDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto) 
        {

            var refreshToken = refreshTokenDto.RefreshToken;

            var specification = new RefreshTokenWithUserSpecification(refreshToken);

            var user = await _repository.FirstOrDefaultAsync(specification , default);

            if (user == null) {

                return null;

            }

            if (user.ExpiresAt < DateTime.UtcNow) {

                user.Revoke(true , DateTime.UtcNow);

                await _repository.UpdateAsync(user);

                return null;

            }

            var mode = refreshTokenDto.TokenStorageMode;

            //前端往后端去发请求携带TokenStorageMode，
            return await GenerateTokenAsync(user.ApplicationUser, mode);
                 
        }

        private static string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(bytes);
                
        }
    }
}
