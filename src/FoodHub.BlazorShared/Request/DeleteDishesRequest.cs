namespace FoodHub.BlazorShared.Request
{
    public class DeleteDishesRequest :BaseRequest
    {
        public DeleteDishesRequest() { }
        public DeleteDishesRequest(int storeId, int productId ):this()
        {
            StoreId = storeId;

            ProductId = productId;
        }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

    }
}
