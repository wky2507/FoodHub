using FoodHub.BlazorShared.Dto;
using FoodHub.Domain.Share.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Request
{
    public class GetMerchantApplicationsRequest : BaseRequest
    {
        public GetMerchantApplicationsRequest() { }

        public GetMerchantApplicationsRequest(int page, int pageSize, ApplicationStatus? status, string keywords) {
            
            Page = page;

            PageSize = pageSize;

            Status = status;

            Keyword = keywords;
        }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
             
        public ApplicationStatus? Status { get; set; }

        public string? Keyword { get; set; }
    }
}
