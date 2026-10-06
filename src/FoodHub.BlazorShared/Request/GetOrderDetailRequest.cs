
namespace FoodHub.BlazorShared.Request
{
    public class GetOrderDetailRequest
    {
        public int OrderId { get; set; }

        public GetOrderDetailRequest() { }

        public GetOrderDetailRequest(int orderId) {

            OrderId = orderId;

        }

    }
}
