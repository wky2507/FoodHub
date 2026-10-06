
using System.ComponentModel.DataAnnotations;

namespace FoodHub.Domain.Share.Constants
{
    public enum OrderStatus
    {
  
       [Display(Name ="待接单")]
       Pending = 0,
       [Display(Name ="制作中")]
       Making = 1,
       [Display(Name ="待取货")]
       WaitingPick = 2,
       [Display(Name ="配送中")]
       Sending = 3,
       [Display(Name ="已完成")]
       Completed = 4,
       [Display(Name ="已取消")]
       Cancle = 5,
       [Display(Name="已拒单")]
       Reject = 6
   
    }
}
