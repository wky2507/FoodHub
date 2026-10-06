using Ardalis.Specification;
using FoodHub.Domain.Entity.Identity;

namespace FoodHub.Domain.Specifications
{
    public sealed class RefreshTokenByUserIdSpecification : Specification<AppUserRefreshToken>
    {
        public RefreshTokenByUserIdSpecification()
        {
        }

        public RefreshTokenByUserIdSpecification(string userId) {

            Query.Where(c => c.UserId == userId);
        }

        public RefreshTokenByUserIdSpecification(string userId, string refreshToken) 
        {
            
            Query.Where(c => c.UserId == userId && c.RefreshToken == refreshToken);
        
        }


    }
}
