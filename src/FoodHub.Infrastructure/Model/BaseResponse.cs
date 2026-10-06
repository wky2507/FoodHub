namespace FoodHub.Infrastructure.Model
{
    public class BaseResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; } = string.Empty;

        public static BaseResponse Fail(string message) => new() { Result = false, Message = message };
    }
}
