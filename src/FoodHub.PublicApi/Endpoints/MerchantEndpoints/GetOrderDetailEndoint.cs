using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    [Route("api/merchant/ordermanagement")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class GetOrderDetailEndoint : EndpointBaseAsync.WithRequest<GetOrderDetailRequest>.WithActionResult<OrderDto>
    {

        public IRepository<Order> _orderRepository;

        public IRepository<Product> _productRepository;

        public GetOrderDetailEndoint(IRepository<Order> orderRepository,IRepository<Product> productRepository) {

            _orderRepository = orderRepository;

            _productRepository = productRepository;
        }

        [HttpGet("orderdetail")]
        public override async Task<ActionResult<OrderDto>> HandleAsync([FromQuery]GetOrderDetailRequest request,CancellationToken cancellationToken) {

            var orderId = request.OrderId;

            var order = await _orderRepository.FirstOrDefaultAsync(new GetOrderByOrderIdSpec(orderId),cancellationToken);

            if (order == null) {

                return BadRequest();

            }

            var orderDto = MapToDto.MapToOrderDto(order);

            return Ok(orderDto);

        } 

    }
}
