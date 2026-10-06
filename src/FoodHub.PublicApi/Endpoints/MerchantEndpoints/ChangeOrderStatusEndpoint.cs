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
    public class ChangeOrderStatusEndpoint :EndpointBaseAsync.WithRequest<ChangeOrderStatusRequest>.WithActionResult
    {

        public IRepository<Order> _orderRepository;


        public ChangeOrderStatusEndpoint(IRepository<Order> orderRepository) {

            _orderRepository = orderRepository;

        }
        [HttpPost("changestatus")]
        public override async Task<ActionResult> HandleAsync(ChangeOrderStatusRequest request, CancellationToken cancellationToken) {

            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order == null) {

                return NotFound();
            
            }

            var status = request.Status;

            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow ;

            switch (status) {
                
               case OrderStatus.Making:

                    order.AddAcceptTime(dateTimeOffset);

                    break;
                case OrderStatus.Reject:

                    order.AddRejectTime(dateTimeOffset);

                    break;
                case OrderStatus.WaitingPick:

                    order.AddFinishTime(dateTimeOffset);

                    break;
                case OrderStatus.Cancle:

                    order.AddCancelTime(dateTimeOffset);

                    break;
            }

            order.ChangeStatus(request.Status);

            await _orderRepository.SaveChangesAsync(cancellationToken);

            return Ok();
        
        }

    }
}
