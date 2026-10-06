namespace FoodHub.BlazorShared.Request
{
    public class ChangeOrderStatusRequest
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public ChangeOrderStatusRequest(int id, OrderStatus status) {

            this.Id = id;

            this.Status = status;
        
        }

    }
}
