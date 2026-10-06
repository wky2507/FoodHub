using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    [Route("api/merchant/ordermanagement")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class GetOrdersEndpoint : EndpointBaseAsync.WithRequest<PagedGetOrdersRequest>.WithActionResult<PagedResult<OrderDto>>
    {
        public IReadRepository<Product> _productRepository;

        public IReadRepository<Buyer> _buyerRepository;

        public IReadRepository<Order> _orderRepository;

        public IReadRepository<Store> _storeRepository;

        public ILogger<Order> _logger;
        public GetOrdersEndpoint(IReadRepository<Product> productRepository, IReadRepository<Buyer> buyerRepository, IReadRepository<Order> orderRepository, IReadRepository<Store> storeRepository, ILogger<Order> logger)
        {
            _productRepository = productRepository;

            _buyerRepository = buyerRepository;

            _orderRepository = orderRepository;

            _storeRepository = storeRepository;

            _logger = logger;
        }
        [HttpGet("getorders")]
        public override async Task<ActionResult<PagedResult<OrderDto>>> HandleAsync([FromQuery] PagedGetOrdersRequest request, CancellationToken cancellationToken)
        {
            List<Order>? orders = new();

            DateTime? dateTime = null;

            var userId = HttpContext.User.FindFirstValue("sub");

            if (userId == null)
            {
                return BadRequest();
            }

            var store = await _storeRepository.FirstOrDefaultAsync(new GetStoreByUserId(userId), cancellationToken);

            if (store == null)
            {

                return NotFound();

            }

            var storeId = store.Id;

            var GetTotalCountSpec = new GetOrdersTotalCountByStoreId(storeId);

            var defaultTotalCount = await _orderRepository.CountAsync(GetTotalCountSpec, cancellationToken);

            var result = new PagedResult<OrderDto>
            {
                
                Page = request.Page,

                PageSize = request.PageSize,

                TotalCount = defaultTotalCount
            };

            //不传任何查询条件，就全量返回
            if (string.IsNullOrWhiteSpace(request.Keywords) && request.Status == null && string.IsNullOrWhiteSpace(request.OrderDate))
            {
                var ordersDefault = await _orderRepository.ListAsync(new GetOrdersByStoreIdSpec(storeId, request.Page, request.PageSize), cancellationToken);

                result.Items = MapToDto.MapToOrderDtos(ordersDefault);

                return Ok(result);

            }
            else {
                if (request.OrderDate != null)
                {
                    if (DateTime.TryParseExact(request.OrderDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                    {
                        dateTime = dt;
                    }
                    else
                    {
                        return BadRequest("日期格式错误");
                    }
                }

                orders = await _orderRepository.ListAsync(new GetOrdersByConditionSpec(storeId, request.Page, request.PageSize, request.Keywords, request.Status, dateTime,_logger));

                result.Items = MapToDto.MapToOrderDtos(orders);

                result.TotalCount = await _orderRepository.CountAsync(new GetCountByConditionSpec(storeId,request.Keywords,request.Status,dateTime));

                return Ok(result);
            }


        }
    }
}
