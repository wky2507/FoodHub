using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared
{
    public abstract class BaseMessage
    {
        public Guid guid { get; set; } = Guid.NewGuid();

        public Guid correlation() => guid;

    }
}
