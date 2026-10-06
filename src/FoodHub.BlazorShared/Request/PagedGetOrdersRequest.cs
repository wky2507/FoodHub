
namespace FoodHub.BlazorShared.Request
{
    public class PagedGetOrdersRequest :BaseRequest
    {
        public int Page { get; set; }
        
        public int PageSize { get; set; }

        public OrderStatus? Status { get; set; }

        public string? OrderDate { get; set; }

        public string? Keywords { get; set; }

        public PagedGetOrdersRequest() { }

        public PagedGetOrdersRequest(int page, int pageSize, OrderStatus? status, string? keywords,string? orderDate):this() {

            Page = page;

            PageSize = pageSize;

            Status = status;

            Keywords = keywords;

            OrderDate = orderDate;

        }

        public void AddOptions(int page, int pageSize, OrderStatus? status, string? keywords, string? orderDate) {

            Page = page;

            PageSize = pageSize;

            Status = status;

            Keywords = keywords;

            OrderDate = orderDate;

        }


    }
}
