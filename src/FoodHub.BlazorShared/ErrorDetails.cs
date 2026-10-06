using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared
{
    public class ErrorDetails :BaseMessage
    {
        public int code { get; set; }

        public string message { get; set; } = string.Empty;

    }
}
