using Ardalis.Specification;
using FoodHub.Domain.Entity.Identity;

namespace FoodHub.Domain.Specifications
{
    public sealed class RefreshTokenWithUserSpecification:Specification<AppUserRefreshToken>
    {
        public RefreshTokenWithUserSpecification(string refreshToken) {

            Query.Where(c => c.RefreshToken == refreshToken)
                .Include(c => c.ApplicationUser);

        }

    }
}
