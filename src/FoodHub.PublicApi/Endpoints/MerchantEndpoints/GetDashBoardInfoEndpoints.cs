using Ardalis.ApiEndpoints;
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodHub.PublicApi.Endpoints.MerchantEndpoints
{
    public class GetDashBoardInfoEndpoints : EndpointBaseAsync.WithoutRequest.WithActionResult<DashBoardDto>
    {

        private readonly IProductRepository _productRepository;

        private readonly IReadRepository<Store> _storeRepository;

        private readonly IOrderRepository _orderRepository;
        public GetDashBoardInfoEndpoints(IOrderRepository orderRepository, IProductRepository productRepository,IReadRepository<Store> storeRepository) {

            _orderRepository = orderRepository;

            _productRepository = productRepository;

            _storeRepository = storeRepository;
        
        }

        [HttpGet("api/merchant/dashboard")]
        [Authorize(Roles = RoleConstants.Roles.MERCHANT)]
        public override async Task<ActionResult<DashBoardDto>> HandleAsync(CancellationToken cancellationToken) {

            var userId = HttpContext.User.FindFirstValue("sub");

            if (userId == null) {
                return BadRequest();
            }

            var store = await _storeRepository.FirstOrDefaultAsync(new GetStoreByUserId(userId),cancellationToken);

            if (store == null) {
                return BadRequest();
            }

            var todayOrdersSpec = new GetTodayOrdersSpec(store.Id);

            var dashBoardDto = new DashBoardDto();

            //今日营业额
            dashBoardDto.TodayTurnover = await _orderRepository.SumAsync(todayOrdersSpec, cancellationToken);

            dashBoardDto.TodayOrders = await _orderRepository.CountAsync(todayOrdersSpec,cancellationToken);

            dashBoardDto.PendingOrders = await _orderRepository.CountAsync(new GetTodayPendingOrdersSpec(store.Id), cancellationToken);

            dashBoardDto.DishesCount = await _productRepository.CountAsync(new GetDishesSpec(store.Id), cancellationToken);
            //只拿了前四个
            dashBoardDto.Orders = MapToDto.MapToOrderDtos(await _orderRepository.ListAsync(new GetFourTodayDishesSpec(store.Id), cancellationToken));

            //处于售卖状态的菜品数量
            dashBoardDto.OnSaleDishesCount = await _productRepository.CountAsync(new GetDishesOnSaleSpec(), cancellationToken);

            //拿了前20个
            var startDate = new DateTimeOffset(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1, 0, 0, 0, TimeSpan.Zero);

            var endDate = startDate.AddMonths(1);

            var hotSaleDtos = await _orderRepository.GetHotSaleDishesAsync(store.Id, startDate, endDate, cancellationToken);

            dashBoardDto.HotSaleDishes = hotSaleDtos;

            return Ok(dashBoardDto);
        }


    }
}
