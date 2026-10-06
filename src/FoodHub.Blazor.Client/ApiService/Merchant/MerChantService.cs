using FoodHub.BlazorShared.Dto;
using FoodHub.Blazor.Client.Services;
using FoodHub.BlazorShared.Response;

namespace FoodHub.Blazor.Client.ApiService.Merchant
{
    public class MerChantService
    {
        public HttpService _httpService = default!;

        public MerChantService(HttpService httpService) {

            _httpService = httpService;

        }

        public async Task<HttpCallResultDto<DashBoardDto>> GetDashBoardAsync()
        {
            try
            {
                var result = await _httpService.HttpGet<DashBoardDto>("merchant/dashboard", null);

                return result;
            }
            catch (Exception) {

                throw;

            }
        }
        public async Task<HttpCallResultDto<object>> AddDishesAsync(ProductDto productDto) {
            try
            {
                if (productDto == null)
                {

                    throw new ArgumentNullException();

                }

                var result = await _httpService.HttpPost<object>("merchant/dishesManagement/addDishes", productDto);

                return result;
            }
            catch (Exception) {

                throw;

            }
        }

        public async Task<HttpCallResultDto<object>> DeleteFoodImageTemproryAsync(DeleteFoodImageRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException();

            }

            var result = await _httpService.HttpDelete<object>("merchant/delete/dishesImage", request);

            return result;
        }

        public async Task<HttpCallResultDto<PagedResult<ProductDto>>> GetProductDtoListAsync(PagedGetDishesRequest request) {

            var result = await _httpService.HttpGet<PagedResult<ProductDto>>("merchant/dishesManagement/getDishesList",request);

            return result;        
        
        }

        public async Task<HttpCallResultDto<ProductDto>> GetProductDtoAsync(GetDishRequest request) {

            var result = await _httpService.HttpGet<ProductDto>("merchant/dishesManagement/edit/getDish", request);

            return result;
        }


        public async Task<HttpCallResultDto<object>> ChangeOnSaleStatusAsync(ChangeOnSaleStatusRequest request) {

            if (request == null) {

                throw new ArgumentNullException();
            
            }

            var result = await _httpService.HttpPost<object>("merchant/dishesManagement/changeOnSaleStatus",request);

            return result;
        
        }

        public async Task<HttpCallResultDto<object>> DeleteDishesAsync(DeleteDishesRequest request)
        {

            if (request == null)
            {

                throw new ArgumentNullException();

            }

            var result = await _httpService.HttpDelete<object>("merchant/dishesManagement/dishesDelete",request);

            return result;

        }

        public async Task<HttpCallResultDto<object>> UpdateDishesAsync(ProductDto request) {
           
                if (request == null) {

                    throw new ArgumentNullException();

                }

                var result = await _httpService.HttpPut<object>("merchant/dishesManagement/updateDishes", request);

                return result;
        }

        public async Task<HttpCallResultDto<PagedResult<OrderDto>>> GetOrdersAsync(PagedGetOrdersRequest request) {

            if (request == null) {

                throw new ArgumentNullException();

            }

            var result = await _httpService.HttpGet<PagedResult<OrderDto>>("merchant/ordermanagement/getorders", request);

            return result;
        }

        public async Task<HttpCallResultDto<object>> ChangeOrderStatus(ChangeOrderStatusRequest request) {

            if (request == null)
            {
                throw new ArgumentNullException();
            }


            var result = await _httpService.HttpPost<object>("merchant/ordermanagement/changestatus", request);

            return result;

        }

        public async Task<HttpCallResultDto<OrderDto>> GetOrderDetailAsync(GetOrderDetailRequest request) {

            if (request == null) {
                throw new ArgumentNullException();
            }

            var result = await _httpService.HttpGet<OrderDto>("merchant/ordermanagement/orderdetail", request);

            return result;

        }

        public async Task<HttpCallResultDto<GetOrdersDataResponse>> GetOrdersDataAsync() {

            var result = await _httpService.HttpGet<GetOrdersDataResponse>("merchant/ordermanagement/getordersdata",null);

            return result;
        }
    }
}
