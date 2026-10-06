
namespace FoodHub.Domain.Entity.StoreEntity
{
    public class StoreContact : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public string Phone { get; private set; } = string.Empty;

        public string IDNumber { get; private set; } = string.Empty;

        public string Image_IDCard_Url { get; private set; } = string.Empty;

        # nullable disable warnings
        public Store Store { get; private set; }

        public int StoreId { get; private set; }

        public ContactPersonRoles Role { get; private set; }

        private StoreContact() { }

        public StoreContact(string name, string phone, string idNumber, string image_IdCard_Url, ContactPersonRoles role) {

            Name = name;

            Phone = phone;

            IDNumber = idNumber;

            Image_IDCard_Url = image_IdCard_Url;

            Role = role;

        }
        
    }
}
