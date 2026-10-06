using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoodHub.Blazor.Client.Helper
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue) {

            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName() ?? enumValue.ToString();
 
        }

    }
}
