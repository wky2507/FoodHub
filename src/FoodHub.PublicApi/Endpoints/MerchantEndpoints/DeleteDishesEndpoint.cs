using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    [Route("api/merchant/dishesManagement")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class DeleteDishesEndpoint : EndpointBaseAsync.WithRequest<DeleteDishesRequest>.WithActionResult
    {
        public IRepository<Store> _storeRepository;

        public IRepository<Product> _productRepository;

        public ILogger<DeleteDishesEndpoint> _logger;

        public DeleteDishesEndpoint(IRepository<Store> storeRepository,IRepository<Product> productRepository,ILogger<DeleteDishesEndpoint> logger) 
        {
            _storeRepository = storeRepository;

            _productRepository = productRepository;

            _logger = logger;
        }

        [HttpDelete("dishesDelete")]
        public async override Task<ActionResult> HandleAsync([FromQuery]DeleteDishesRequest request,CancellationToken cancellationToken)
        {
            _logger.LogInformation($"收到删除菜品请求{request}");

            var getProductByStoreId = new GetProductByStoreIdAndProductId(request.StoreId,request.ProductId);

            var product = await _productRepository.FirstOrDefaultAsync(getProductByStoreId, cancellationToken);

            if (product == null) {

                return BadRequest();
            
            }

            var affectLines = await _productRepository.DeleteAsync(product, cancellationToken);

            if (affectLines > 0) {

                return Ok();
            
            }
            else{
                return BadRequest();
            }
        }


    }
}
