namespace FoodHub.BlazorShared.Response
{
    public class GetOrdersDataResponse
    {
        //全部订单
        public int TotalCount { get; set; }
        //待接单
        public int PendingCount { get; set; }

        //制作中
        public int MakingCount { get; set; }

        //配送中
        public int SendingCount { get; set; }

        //今日已经完成
        public int TodayFinishedCount { get; set; }

        //今日营业额
        public decimal TodayTurnover { get; set; }

        public GetOrdersDataResponse() { }

        public GetOrdersDataResponse( int pendingCount, int makingCount, int sendingCount, int todayFinishedCount, int todayTurnover)
        {
            PendingCount = pendingCount;
            
            MakingCount = makingCount;
            
            SendingCount = sendingCount;
            
            TodayFinishedCount = todayFinishedCount;
            
            TodayTurnover = todayTurnover;
        }
    }
}
