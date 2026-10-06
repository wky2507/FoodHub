namespace FoodHub.BlazorShared.Dto
{
    public class DashBoardDto
    {
        //今日营业额
       public decimal TodayTurnover { get; set; }
        //今日订单数量
       public int TodayOrders { get; set; }
        //待处理订单数量
       public int PendingOrders { get; set; } 
        //菜品数量
       public int DishesCount { get; set; }
        //在售商品数量
       public int OnSaleDishesCount { get; set; }
        //今日最近订单,只要前面的4个
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
        //热销菜品及售卖量
        public List<HotSaleDto> HotSaleDishes { get; set; } = new List<HotSaleDto>();
    }
}
