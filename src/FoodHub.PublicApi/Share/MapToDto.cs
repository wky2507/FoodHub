using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.PublicApi.Share
{
    public static class MapToDto
    {
        public static List<MerchantApplicantDto> MapToDtoMerchantApplicant(List<MerchantApplication> applications)
        {
            var result = new List<MerchantApplicantDto>();

            foreach (var application in applications)
            {
              
                var data =  new MerchantApplicantDto
                {

                    Id = application.Id,

                    UserId = application.UserId,

                    StoreName = application.StoreName,

                    Address = application.Address,

                    LicenseImageUrl = application.LicenseImage_Url,

                    Status = application.Status,

                    SubmitTime = application.SubmittedAt,

                    ReviewComment = application.ReviewComment,

                    Contacts = application.ApplicationContact
                                .Select(c => new ContactDto
                                {
                                    Name = c.Name,

                                    Phone = c.Phone,

                                    ID_Number = c.IDNumber,

                                    PersonRoles = c.Role
                                }).ToList()
                };

                result.Add(data);

            }
            return result;
        }

        public static List<OrderDto> MapToDtoOrder(List<Order> orders) {
           
            var list =  new List<OrderDto>();

            foreach (var order in orders) {

                var orderDto = new OrderDto();

                //orderDto.Id = order.Id;

                //orderDto.BuyerId = order.BuyerId;

                //orderDto.StoreId = order.StoreId;

                orderDto.OrderDate = order.OrderDate;

                orderDto.TotalPrice = order.TotalPrice;

                orderDto.Status = order.Status;

                orderDto.Address = order.Address;

                orderDto.orderItemDtos = MapToDtoOrderItem(order.OrderItems.ToList());

                list.Add(orderDto);
            }

            return list;
        }

        public static List<OrderItemDto> MapToDtoOrderItem(List<OrderItem> orderItems) {

            var list = new List<OrderItemDto>();
        
            foreach (var orderItem in orderItems) { 
                
                var  orderItemDto = new OrderItemDto();

                //orderItemDto.OrderId = orderItem.OrderId;

                orderItemDto.ProductId = orderItem.ProductId;

                orderItemDto.Price = orderItem.Price;

                orderItemDto.Count = orderItem.Count;

                orderItemDto.ProductDto = MapToDto.MapToProductDto(orderItem.Product);

                list.Add(orderItemDto);
            }

            return list;
        
        }

        public static List<ProductDto> MapToListProductDto(List<Product> products) {

            var list = new List<ProductDto>();

            foreach (var product in products) {

                var productDto = new ProductDto();

                productDto.Id = product.Id;

                productDto.Name = product.Name;

                productDto.Price = product.Price;

                productDto.StoreId = product.StoreId;

                productDto.IsOnsale = product.IsOnsale;

                productDto.PictureUri = product.PictureUri;

                productDto.CategoryName = product.CategoryName;

                list.Add(productDto);
            }

            return list;
        
        }

        public static ProductDto MapToProductDto(Product product) {

            var productDto = new ProductDto();

            productDto.Id = product.Id;

            productDto.Name = product.Name;

            productDto.Price = product.Price;

            productDto.StoreId = product.StoreId;

            productDto.IsOnsale = product.IsOnsale;

            productDto.PictureUri = product.PictureUri;

            productDto.CategoryName = product.CategoryName;

            productDto.SelectCategory = (KindofFood)product.CategoryName;

            return productDto;
        }

        public static Product MapToProduct(ProductDto productDto) {

            var product = new Product(productDto.Name, productDto.Price ?? 0, productDto.StoreId, productDto.CategoryName, productDto.PictureUri, productDto.IsOnsale);

            return product;
        
        }

        public static List<OrderDto> MapToOrderDtos(List<Order> orders) {

            var orderDtos = new List<OrderDto>();

            foreach (var order in orders) {

                var orderDto = new OrderDto();

                orderDto.Id = order.Id;

                orderDto.OrderNumber = order.OrderNumber;

                orderDto.OrderDate = order.OrderDate;

                orderDto.TotalPrice = order.TotalPrice;

                orderDto.Status = order.Status;

                orderDto.BuyerDto = MapToDto.MapToBuyerDto(order.Buyer);

                orderDto.orderItemDtos = MapToDto.MapToDtoOrderItem(order.OrderItems.ToList());

                orderDtos.Add(orderDto);
            }

            return orderDtos;
        
        }
        public static BuyerDto MapToBuyerDto(Buyer buyer) {
            if (buyer == null) {
                return new BuyerDto();
            }

            var buyerDto = new BuyerDto();

            buyerDto.Name = buyer.Name;

            buyerDto.Phone = buyer.Phone;

            return buyerDto;
        }

        public static OrderDto MapToOrderDto(Order order) {

            var orderDto = new OrderDto();
            //订单Id
            orderDto.Id = order.Id;
            //订单编号
            orderDto.OrderNumber = order.OrderNumber;
            //下单时间
            orderDto.OrderDate = order.OrderDate;
            //商品总价
            orderDto.TotalPrice = order.TotalPrice;
            //订单状态
            orderDto.Status = order.Status;
            //顾客
            orderDto.BuyerDto = MapToDto.MapToBuyerDto(order.Buyer);
            //订单项列表
            orderDto.orderItemDtos = MapToDto.MapToDtoOrderItem(order.OrderItems.ToList());
            //地址
            orderDto.Address = order.Address;
            //配送费
            orderDto.DeliveryFee = order.DeliveryFee;
            //优惠费用
            orderDto.DiscountAmount = order.DiscountAmount;
            //实际支付金额
            orderDto.ActualPayment = order.ActualPayment;
            //顾客备注
            orderDto.Remark = order.Remarks;
            //支付时间
            orderDto.PaymentTime = order.PaymentTime;
            //是否支付
            orderDto.IsPaid = order.IsPaid;
            //接单时间
            orderDto.AcceptedTime = order.AcceptedTime;
            //拒单时间
            orderDto.RejectedTime = order.RejectedTime;
            //出餐时间
            orderDto.MealFinishedTime = order.MealFinishedTime;
            //取消订单时间
            orderDto.CanceledTime = order.CanceledTime;

            return orderDto;
        }
    }
}
