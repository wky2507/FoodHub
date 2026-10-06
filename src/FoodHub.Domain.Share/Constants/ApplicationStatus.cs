using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.Domain.Share.Constants
{
    public enum ApplicationStatus
    {
       //悬而未决
       Pending = 0,
       //通过
       Approved = 1,
       //拒绝
       Rejected = 2,

       All = 3

    }
}
