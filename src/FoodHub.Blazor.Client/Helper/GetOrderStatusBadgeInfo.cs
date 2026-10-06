namespace FoodHub.Blazor.Client.Helper
{
    public static class GetOrderStatusBadgeInfoTool
    {

        public static (string Bg, string Icon) GetOrderStatusBadgeInfo(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => ("bg-warning", "fa-clock"),

                OrderStatus.Making => ("bg-primary", "fa-utensils"),

                OrderStatus.WaitingPick => ("bg-info", "fa-hand-holding"),

                OrderStatus.Sending => ("bg-info", "fa-motorcycle"),

                OrderStatus.Completed => ("bg-success", "fa-check-circle"),

                OrderStatus.Cancle => ("bg-secondary", "fa-times-circle"),

                OrderStatus.Reject => ("bg-danger", "fa-ban"),
                // 这里就是 default，处理未知枚举值
                _ => ("bg-light", "fa-question-circle")
            };




        }

    }
}
