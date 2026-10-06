using Ardalis.ApiEndpoints;
using FoodHub.BlazorShared.Response;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{

    [Route("api/merchant/ordermanagement")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
    public class GetOrdersDataEndpoint : EndpointBaseAsync.WithoutRequest.WithActionResult<GetOrdersDataResponse>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IRepository<Store> _storeRepository;

        public GetOrdersDataEndpoint(IOrderRepository orderRepository, IRepository<Store> storeRepository)
        {

            _orderRepository = orderRepository;

            _storeRepository = storeRepository;
        }
        [HttpGet("getordersdata")]
        public override async Task<ActionResult<GetOrdersDataResponse>> HandleAsync(CancellationToken cancellationToken)
        {

            var userId = HttpContext.User.FindFirstValue("sub");

            var store = await _storeRepository.FirstOrDefaultAsync(new GetStoreByUserId(userId), cancellationToken);

            if (store == null) {

                return BadRequest();
            
            }

            var storeId = store.Id;

            var response = new GetOrdersDataResponse();

            response.TotalCount = await _orderRepository.CountAsync(new GetOrdersTotalCountByStoreId(storeId), cancellationToken);

            response.SendingCount = await _orderRepository.CountAsync(new GetSendingCountSpec(storeId), cancellationToken);

            response.PendingCount = await _orderRepository.CountAsync(new GetPendingCountSpec(storeId), cancellationToken);

            response.MakingCount = await _orderRepository.CountAsync(new GetMakingCountSpec(storeId), cancellationToken);

            response.TodayFinishedCount = await _orderRepository.CountAsync(new GetTodayFinishedCountSpec(storeId), cancellationToken);

            response.TodayTurnover = await _orderRepository.SumAsync(new GetTodayOrdersSpec(storeId), cancellationToken);

            return Ok(response);

        }
    }
}
