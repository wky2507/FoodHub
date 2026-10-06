using FoodHub.BlazorShared.Dto;

namespace FoodHub.BlazorShared.Response
{
    public class GetSaleTrendResponse
    {
        public List<SalesTrendDto> salesTrendDtos { get; set; } = new();

    }
}
