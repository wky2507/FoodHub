
using System.ComponentModel.DataAnnotations;


namespace FoodHub.Domain.Share.Constants
{
    public enum KindofFood
    {
        [Display(Name = "全部分类")]
        None = 0,
        [Display(Name = "热菜")]
        HotDish = 1,
        [Display(Name = "凉菜")]
        ColdDish = 2,
        [Display(Name = "主食")]
        Staple = 3,
        [Display(Name = "饮品")]
        Drink = 4,
        [Display(Name = "甜品")]
        Dessert = 5
    }
}
