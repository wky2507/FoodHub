using Ardalis.ApiEndpoints;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    [Route("api/merchant/dishesManagement/")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class UpdateDishesEndpoint :EndpointBaseAsync.WithRequest<ProductDto>.WithActionResult
    {
        public IRepository<Product> _productRepository;

        public UpdateDishesEndpoint(IRepository<Product> productRepository) {

            _productRepository = productRepository;
        
        }
        [HttpPut("updateDishes")]
        public override async Task<ActionResult> HandleAsync(ProductDto request,CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null) {

               return BadRequest();

            }

            product.UpdateProduct(request.Name, request.Price ?? 0, request.CategoryName, request.PictureUri, request.IsOnsale);

            var line =  await _productRepository.SaveChangesAsync();

            if (line > 0)
            {
                return Ok();
            }
          

            return BadRequest();
        }

    }
}
