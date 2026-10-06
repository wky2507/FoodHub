//using Ardalis.ApiEndpoints;
//using FoodHub.BlazorShared.Request;
//using FoodHub.Domain.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
//{
//    public class DeleteDishesImageEndpoint : EndpointBaseAsync.WithRequest<DeleteFoodImageRequest>.WithActionResult
//    {
//        public readonly IRepository<Product> _productRepository;

//        public DeleteDishesImageEndpoint(IRepository<Product> productRepository) {
            
//            _productRepository = productRepository;

//        }


//        protected override async Task<ActionResult> HandleAsync(DeleteFoodImageRequest request,CancellationToken cancellationToken) {

//            var existUrl = await _productRepository.AnyAsync();
                

        
        
//        }


//    }
//}
