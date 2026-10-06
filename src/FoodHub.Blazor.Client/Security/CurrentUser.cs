using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using FoodHub.Blazor.Client.Interfaces;
namespace FoodHub.Blazor.Client.Security
{
    public class CurrentUser : ICurrentUser
    {
        private readonly AuthenticationStateProvider _provider;

        public CurrentUser(AuthenticationStateProvider provider)
        {
            _provider = provider;
        }

        public string? UserId
        {
            
            get {
                var user = GetUser();
                return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        
        }

        public string? UserName
        {
            get {
                
                var user = GetUser();
                return user?.FindFirst(ClaimTypes.Name)?.Value;
            
            }
        }

        public string? Email
        {
            get {

                var user = GetUser();

                return user?.FindFirst(ClaimTypes.Email)?.Value;
            }
        
        }

        public IEnumerable<string> Roles
        {
            get {

                var user = GetUser();

                return user?.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .ToArray() ?? Array.Empty<string>();
                       }
        
        }

        public bool IsAuthenticated {

            get {
                var user = GetUser();

                return user?.Identity?.IsAuthenticated == true;

            }
        
        }

        private ClaimsPrincipal? GetUser()
        {
            var state = _provider.GetAuthenticationStateAsync().Result;

            return state.User;
        
        }

    }
}
