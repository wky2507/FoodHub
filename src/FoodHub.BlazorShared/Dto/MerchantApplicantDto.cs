
using FoodHub.Domain.Share.Constants;


namespace FoodHub.BlazorShared.Dto
{
    public class MerchantApplicantDto 
    {

            public MerchantApplicantDto() { }

            public MerchantApplicantDto(string userId, string storeName, string address, string licenseImageUrl, ApplicationStatus status, DateTime submitAt)
            {
                    UserId = userId;

                    StoreName = storeName;

                    Address = address;

                    LicenseImageUrl = licenseImageUrl;

                    Status = status;

                    SubmitTime = submitAt;
            }
            public int Id { get; set; }

            public string UserId { get; set; } = string.Empty;

            public string StoreName { get; set; } = string.Empty;

            public string Address { get; set; } = string.Empty;

            public string LicenseImageUrl { get; set; } = string.Empty;

            public ApplicationStatus Status { get; set; }

            public DateTime SubmitTime { get; set; }

            public string? ReviewComment { get; set; }

            public DateTime? ReviewAt { get; set; } 

            public List<ContactDto> Contacts { get; set; } = new();

    }
}
