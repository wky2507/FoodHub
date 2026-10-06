
using System.ComponentModel.DataAnnotations;


namespace FoodHub.Domain.Share.Constants
{
    public enum ContactPersonRoles
    {
        [Display(Name = "法定代表人")]
        LegalRepresentative = 1, 

        [Display(Name = "财务负责人")]
        Finance = 2,    
        
        [Display(Name = "运营/技术负责人")]
        Operations = 3          

    }
}
