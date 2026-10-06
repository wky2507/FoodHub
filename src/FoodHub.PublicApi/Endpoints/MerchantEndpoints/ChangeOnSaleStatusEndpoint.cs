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
    public class ChangeOnSaleStatusEndpoint : EndpointBaseAsync.WithRequest<ChangeOnSaleStatusRequest>.WithActionResult
    {
        public readonly IReadRepository<Store> _storeRepository; 

        public readonly IRepository<Product> _productRepository;

        public ChangeOnSaleStatusEndpoint(IRepository<Product> productRepository,IReadRepository<Store> storeRepository) 
        {

            _productRepository = productRepository;

            _storeRepository = storeRepository;
        }
        [HttpPost("changeOnSaleStatus")]
        public async override Task<ActionResult> HandleAsync(ChangeOnSaleStatusRequest request,CancellationToken cancellationToken) 
        {

            var getProductByStoreId = new GetProductByStoreIdAndProductId(request.StoreId,request.ProductId);

            var product = await _productRepository.FirstOrDefaultAsync(getProductByStoreId, cancellationToken);

            if (product == null) {

                return NotFound();
            
            }

            product.ChangeOnSaleStatus();

            var affectLines =   await _productRepository.SaveChangesAsync(cancellationToken);

            if (affectLines > 0)
            {
                return Ok();
            }
            else {
                return BadRequest();
            }
        }



    }
}
