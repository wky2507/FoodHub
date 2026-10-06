
namespace FoodHub.BlazorShared.Dto
{
    public class OrderDto 
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;
        //下单时间
        public DateTimeOffset OrderDate { get;  set; } = DateTimeOffset.UtcNow;

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public string Address { get;  set; } = string.Empty;

        public BuyerDto BuyerDto { get; set; } = new();
        
        public List<OrderItemDto> orderItemDtos { get; set; } = new List<OrderItemDto>();

        //配送费
        public decimal DeliveryFee { get; set; }

        //优惠金额
        public decimal DiscountAmount { get; set; }

        //实付金额
        public decimal ActualPayment { get; set; }
        
        //支付时间
        public DateTimeOffset? PaymentTime { get; set; }

        //评价
        public string Remark { get; set; } = string.Empty;

        //是否支付
        public bool IsPaid { get; set; }
        
        //接单时间
        public DateTimeOffset? AcceptedTime { get; set; }

        //拒单时间
        public DateTimeOffset? RejectedTime { get; set; }

        //出餐时间
        public DateTimeOffset? MealFinishedTime { get; set; }

        //取消订单时间
        public DateTimeOffset? CanceledTime { get; set; }

        //订单完成时间
        public DateTimeOffset? CompletedTime { get; set; }
    }
}
