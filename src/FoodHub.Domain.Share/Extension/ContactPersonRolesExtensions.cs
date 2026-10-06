using FoodHub.Domain.Share.Constants;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace FoodHub.Domain.Share.Extension
{
    public static class ContactPersonRolesExtensions
    {
        public static string GetDisplayName(this ContactPersonRoles role)
        {
            var member = typeof(ContactPersonRoles).GetMember(role.ToString()).FirstOrDefault();

            return member?
                .GetCustomAttribute<DisplayAttribute>()?
                .Name
                ?? role.ToString();
        }
    }
}