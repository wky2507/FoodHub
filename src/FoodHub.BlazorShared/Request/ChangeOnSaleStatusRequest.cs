
namespace FoodHub.BlazorShared.Request
{
    public class ChangeOnSaleStatusRequest:BaseRequest
    {

        public ChangeOnSaleStatusRequest(int storeId, int productId) {

            StoreId = storeId;

            ProductId = productId;
        }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

    }
}
