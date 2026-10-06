using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;

namespace FoodHub.Domain.Entity.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        //订单编号
        public string OrderNumber { get; private set; }

        public int BuyerId { get; private set; }

        public int StoreId { get; private set; }

        public OrderStatus Status { get; private set; }
        //订单价
        public decimal TotalPrice { get; private set; }
        //下单时间
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.UtcNow;

        public string Address { get; private set; }

        //导航
        public Store Store { get; private set; }

        public Buyer Buyer { get; private set; }

        //支付状态
        public bool IsPaid { get; private set; }

        //支付时间
        public DateTimeOffset PaymentTime { get; private set; }

        //顾客备注
        public string Remarks { get; private set; } = string.Empty;

        //配送费
        public decimal DeliveryFee { get; private set; }

        //优惠金额
        public decimal DiscountAmount { get; private set; }

        //实付金额
        public decimal ActualPayment { get; private set; }

        //接单时间
        public DateTimeOffset AcceptedTime { get; private set; }

        //出餐时间
        public DateTimeOffset MealFinishedTime { get; private set; }

        //拒单时间
        public DateTimeOffset RejectedTime { get; private set; }

        //取消订单时间
        public DateTimeOffset CanceledTime { get; private set; }

        //完成订单时间
        public DateTimeOffset CompletedTime { get; private set; }

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

#pragma warning disable
        private Order() { }

        public Order(string orderNumber,DateTimeOffset orderDate, int buyerId, int storeId, OrderStatus status, string address, bool isPaid, string remark, decimal deliveryFee, decimal discountAmount) : this()
        {
            OrderNumber = orderNumber;

            BuyerId = buyerId;

            StoreId = storeId;

            Status = status;

            Address = address;

            IsPaid = isPaid;

            Remarks = remark;

            DeliveryFee = deliveryFee;

            DiscountAmount = discountAmount;

            OrderDate = orderDate;
        }

        public decimal TotalPriceCalcu() {

            var total = 0m;

            foreach (var item in _orderItems) {

                total += item.Price;

            }
            return total;
        }

        public void AddOrderItems(IEnumerable<OrderItem> orderItems) {

            _orderItems.AddRange(orderItems);

            var totalPrice = TotalPriceCalcu();

            //商品价格
            TotalPrice = totalPrice;

            //实付金额 = 商品总价 + 配送费用 - 优惠金额
            ActualPayment = TotalPrice + DeliveryFee - DiscountAmount;
        }

        public void ChangeStatus(OrderStatus status) {

            Status = status;

        }

        //修改接单时间
        public void AddAcceptTime(DateTimeOffset dateTimeOffset) {

            AcceptedTime = dateTimeOffset;

        }
        //修改拒单时间
        public void AddRejectTime(DateTimeOffset dateTimeOffset) {

            RejectedTime = dateTimeOffset;

        }
        //修改出餐时间
        public void AddFinishTime(DateTimeOffset dateTimeOffset) {

            MealFinishedTime = dateTimeOffset;
        
        }
        //修改取消订单时间
        public void AddCancelTime(DateTimeOffset dateTimeOffset) {

            CanceledTime = dateTimeOffset;
        }

    }
}
