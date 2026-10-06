using FoodHub.Domain.Share.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Request
{
    public class ApproveRequest:BaseRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationStatus Status { get; set; }

        public ApproveRequest(int id,string userId, ApplicationStatus status) {

            Id = id;

            UserId = userId;

            Status = status;

        }

    }
}
