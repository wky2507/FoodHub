using FoodHub.BlazorShared.Dto;
namespace FoodHub.BlazorShared.Request
{
    public class ApplicantRequest: BaseRequest
    {
        private ApplicantRequest() { }
        public ApplicantRequest(string storeName, string adress, string licenseImage_Url,List<ContactDto> contacts) {

            this.StoreName = storeName;

            this.Adress = adress;

            this.LicenseImage_Url = licenseImage_Url;

            this.Contacts = contacts;
        }
        
        public string StoreName { get;  init; } = string.Empty;

        public string Adress { get; init; } = string.Empty;

        public string LicenseImage_Url { get; init; } = string.Empty;

        public List<ContactDto> Contacts { get; init; } = new();
    }
}
