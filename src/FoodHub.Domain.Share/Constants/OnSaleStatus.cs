using System.ComponentModel.DataAnnotations;
namespace FoodHub.Domain.Share.Constants
{
    public enum OnSaleStatus
    {

        [Display(Name = "在售")]
        IsOnSale  = 1,

        [Display(Name ="停售")]
        IsOffSale = 2,

        [Display(Name ="全部")]
         All  = 0
    }
}
