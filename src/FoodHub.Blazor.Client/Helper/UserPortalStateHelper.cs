using System.Security.Claims;
namespace FoodHub.Blazor.Client.Helper
{
    public enum UserPortalState {
        
        ADMINISTRATORS,

        MERCHANT,

        APPLICANT,

        USER
    }



    public static class UserPortalStateHelper
    {
        public static UserPortalState GetState(ClaimsPrincipal user) {

            if (user.IsInRole(RoleConstants.Roles.ADMINISTRATORS))
            {

                return UserPortalState.ADMINISTRATORS;

            }
            else if (user.IsInRole(RoleConstants.Roles.MERCHANT))
            {

                return UserPortalState.MERCHANT;

            }
            else if (user.IsInRole(RoleConstants.Roles.APPLICANT))
            {

                return UserPortalState.APPLICANT;

            }

            return UserPortalState.USER;
        
        }

    }
}
