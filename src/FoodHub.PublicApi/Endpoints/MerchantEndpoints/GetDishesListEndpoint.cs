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
    public class GetDishesListEndpoint : EndpointBaseAsync.WithRequest<PagedGetDishesRequest>.WithActionResult<PagedResult<ProductDto>>
    {
        IReadRepository<Product> _productRepository;

        IReadRepository<Store> _storeRepository;

        public GetDishesListEndpoint(IReadRepository<Product> productRepository,IReadRepository<Store> storeRepository) {

            _productRepository = productRepository;

            _storeRepository = storeRepository;
        }
        [HttpGet("getDishesList")]
        [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
        public override async Task<ActionResult<PagedResult<ProductDto>>> HandleAsync([FromQuery] PagedGetDishesRequest request, CancellationToken cancellationToken) {

            //获取用户ID
            var userId = HttpContext.User.FindFirstValue("sub");

            if (userId == null) {

                return BadRequest();

            }

            var getStoreByUserIdSpec = new GetStoreByUserIdSpec(userId);

            //找商铺
            var store = await _storeRepository.FirstOrDefaultAsync(getStoreByUserIdSpec);

            if (store == null) {

                return BadRequest();

            }

            var storeId = store.Id;

            var getDishesCountSpec = new GetDishesSpec(storeId);

            var count = await _productRepository.CountAsync(getDishesCountSpec, cancellationToken);

            if ((int)request.Category! == 0) {

                var allProductList = await _productRepository.ListAsync(new AllDishesFindByCondiSpec(storeId,request.Page,request.PageSize,request.Keywords,request.IsOnSale), cancellationToken);

                if (allProductList == null) {

                  var defaultList = await _productRepository.ListAsync(new DefautlDishesPagedSpec(storeId,request.Page,request.PageSize), cancellationToken);

                  return Ok(
                        new PagedResult<ProductDto>
                        {
                            Items = MapToDto.MapToListProductDto(defaultList),

                            Page = request.Page,

                            PageSize = request.PageSize,

                            TotalCount = count
                        });
                }

                var allProductResult = new PagedResult<ProductDto>
                {
                    Items = MapToDto.MapToListProductDto(allProductList),

                    Page = request.Page,

                    PageSize = request.PageSize,

                    TotalCount = count
                };

                return (allProductResult);

            }

            var getDishesListSpec = new GetDishesListSpec(storeId,request.Page,request.PageSize,request.Category,request.IsOnSale,request.Keywords);

            var dishesList = await _productRepository.ListAsync(getDishesListSpec, cancellationToken);

            var getdishesListCountSpec = new GetDishesListCountSpec(storeId, request.Keywords, request.IsOnSale);

            var dishesListCount = await _productRepository.CountAsync(getdishesListCountSpec, cancellationToken);

            if (dishesList == null)
            {

                return BadRequest();

            }
             var dishesListDto = MapToDto.MapToListProductDto(dishesList);

            var result = new PagedResult<ProductDto>
            {

                Items = dishesListDto,

                Page = request.Page,

                PageSize = request.PageSize,

                TotalCount = dishesListCount

            };

             return Ok(result);

        }


    }
}
