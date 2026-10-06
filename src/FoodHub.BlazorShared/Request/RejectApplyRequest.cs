using FoodHub.Domain.Share.Constants;


namespace FoodHub.BlazorShared.Request
{
    public class RejectApplyRequest : BaseRequest
    {
        public int Id { get; set; }

        public string ReviewComment { get; set; } = string.Empty;

        public ApplicationStatus Status { get; set; }

        public string UserId { get; set; }

        public RejectApplyRequest(int id,string reviewComment,ApplicationStatus status,string userId) {

            Id = id;
            
            ReviewComment = reviewComment;

            Status = status;

            UserId = userId;
        }

    }
}
