using Ardalis.ApiEndpoints;
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    [Route("api/merchant/dishesManagement")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class AddDishesEndpoint : EndpointBaseAsync.WithRequest<ProductDto>.WithActionResult
    {
        public readonly IRepository<Product> _productRepository;

        public readonly IRepository<Store> _storeRepository;

        public readonly IWebHostEnvironment _webHostEnvironment;
        public AddDishesEndpoint(IRepository<Product> productRepository,IRepository<Store> storeRepository,IWebHostEnvironment webHostEnvironment) {

            _productRepository = productRepository;

            _storeRepository = storeRepository;

            _webHostEnvironment = webHostEnvironment;

        }
        [HttpPost("addDishes")]
        public override async Task<ActionResult> HandleAsync(ProductDto productDto,CancellationToken cancellationToken) {

            var userId = HttpContext.User.FindFirstValue("sub");

            if (userId == null) {

                return BadRequest();
            
            }

            var specStore = new GetStoreByUserId(userId);
            
            //store找
            var store = await _storeRepository.FirstOrDefaultAsync(specStore, cancellationToken);
            
            if (store == null)
            {
                return NotFound();
            }

            var storeId = store.Id;

            var getDishNameByStoreId = new GetDishNameByStoreId(storeId,productDto.Name);

            var existName = await _productRepository.FirstOrDefaultAsync(getDishNameByStoreId, cancellationToken);

            if (existName != null) {

                return BadRequest("已经存在同名的菜品");
            
            }

            productDto.StoreId = storeId;

            decimal price = productDto.Price ?? 0;

            if (string.IsNullOrWhiteSpace(productDto.PictureUri)) {

                productDto.PictureUri = "/Uploads/Default/Anonymous.png";
            
            }

            var product = new Product(productDto.Name, price, productDto.StoreId, productDto.CategoryName, productDto.PictureUri,productDto.IsOnsale);

            

            var result =  await _productRepository.AddAsync(product);

            if(result == null)
            {
                return BadRequest();
            }

            return Ok();

        }


    }
}
