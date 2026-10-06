using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Interface;

namespace FoodHub.Domain.Entity.MerchantApplicationAggregate
{
    public class MerchantApplication :  BaseEntity, IAggregateRoot
    {

       public MerchantApplication(string userId,string storeName, string address,string phone, string licenseImage_Url,ApplicationStatus status, DateTime submittedAt) 
        :this()
        {
            UserId = userId;
            
            StoreName = storeName;

            Address = address;

            Phone = phone;

            LicenseImage_Url = licenseImage_Url;

            Status = status;

            SubmittedAt = submittedAt;
        }

        #nullable disable
        private MerchantApplication() { }

        public ApplicationUser ApplicationUser { get; private set; }

        public string UserId { get; private set; }
      
        public string StoreName { get;private set; } = string.Empty;

        public string Address { get;private set; } = string.Empty;

        public string Phone { get; private set; } = string.Empty;

        public string LicenseImage_Url { get;private set; } = string.Empty;

        public DateTime SubmittedAt { get;private set; } = DateTime.UtcNow;

        public string ReviewComment { get; private set; } = string.Empty;

        public ApplicationStatus Status { get;private set; }

        public DateTime? ReviewAt { get; private set; }

        private  List<ApplicationContact> _ApplicationContact = new List<ApplicationContact>();

        public  IReadOnlyCollection<ApplicationContact> ApplicationContact => _ApplicationContact.AsReadOnly();

        public void AddContact(string name, string phone, string id_Number, ContactPersonRoles role)
        {

            _ApplicationContact.Add(new ApplicationContact(name, phone, id_Number, role));

        }

        public bool Reject(string reviewComment) {

            ReviewComment = reviewComment;

            if (string.IsNullOrWhiteSpace(ReviewComment)) {
                
                return false;
            
            }

            Status = ApplicationStatus.Rejected;

            ReviewAt = DateTime.UtcNow;

            return true;
        }

        public void Approve() {

            Status = ApplicationStatus.Approved;

            ReviewAt = DateTime.UtcNow;
        }

    }
 
}
