using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    [Route("api/merchant/dishesManagement/edit")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class GetDishEndpoint : EndpointBaseAsync.WithRequest<GetDishRequest>.WithActionResult<ProductDto>
    {
        public IReadRepository<Product> _productRepository;

        public GetDishEndpoint(IReadRepository<Product> productRepository) {

            _productRepository = productRepository;

        }

        [HttpGet("getDish")]
        public override async Task<ActionResult<ProductDto>> HandleAsync([FromQuery] GetDishRequest request,CancellationToken cancellationToken) {

            var id = request.ProductId;

            var product = await _productRepository.GetByIdAsync(id,cancellationToken);

            if (product == null) {
               
                return NotFound();

            }

            var productDto = MapToDto.MapToProductDto(product);

            return Ok(productDto);

        }
    }
}
