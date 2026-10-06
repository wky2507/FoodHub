namespace FoodHub.BlazorShared.Dto
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        public ProductDto ProductDto { get; set; } = new();

        public int Count { get;  set; }

        public decimal Price { get;  set; }

        public string ProductSummary { get; set; } = string.Empty;

        public OrderItemDto() {

        }

        public string GenerateProductSummary() {

            return ProductSummary = $"{ProductDto.Name} × {Count}";

        }

    }
}
