using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared
{
    public abstract class BaseResponse : BaseMessage
    {
        protected BaseResponse() { }

        protected BaseResponse(Guid guid) {

            base.guid = guid;

        }
    }
}
