namespace FoodHub.BlazorShared.Request
{
    public class PagedGetDishesRequest : BaseRequest
    {

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? Keywords { get; set; }

        public KindofFood? Category { get; set; }

        public bool? IsOnSale { get; set; }

        public PagedGetDishesRequest() { }

        public PagedGetDishesRequest(int page, int pageSize, string? keywords, KindofFood category, bool? isOnSale):this()
        {
            Page = page;
            PageSize = pageSize;
            Keywords = keywords;
            Category = category;
            IsOnSale = isOnSale;
        }
    }
}
