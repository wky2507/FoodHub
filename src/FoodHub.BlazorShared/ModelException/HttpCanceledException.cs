using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.ModelException
{
    public class HttpCanceledException :Exception
    {
        public HttpCanceledException(string mes,Exception? ex) : base(mes,ex){ }

    }
}
