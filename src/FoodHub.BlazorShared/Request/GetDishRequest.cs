using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Request
{
    public class GetDishRequest
    {
        public GetDishRequest() { }

        public GetDishRequest(int producId) : this() {

            ProductId = producId;

        }

        public int ProductId { get; set; }


    }
}
