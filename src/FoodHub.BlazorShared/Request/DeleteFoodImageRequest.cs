namespace FoodHub.BlazorShared.Request
{
    public class DeleteFoodImageRequest : BaseRequest
    {
        public string FileName { get; set; } = string.Empty;

        public int Id { get; set; }

        public DeleteFoodImageRequest() { }

        public DeleteFoodImageRequest(string fileName,int id) : this() {

            FileName = fileName;

            Id = id
;
        }

    }
}
