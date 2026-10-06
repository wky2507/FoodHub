using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Response
{
    public class ApplicantResponse :BaseResponse
    {
        public ApplicantResponse(Guid guid) {

            base.guid = guid;

        }
        public ApplicantResponse() { }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
