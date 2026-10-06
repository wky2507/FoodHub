using Ardalis.GuardClauses;

namespace FoodHub.Domain.Entity.MerchantApplicationAggregate
{
    public class ApplicationContact : BaseEntity
    {

        public string Name { get; private set; } = string.Empty;

        public string Phone { get; private set; } = string.Empty;

        public string IDNumber { get; private set; } = string.Empty;

        public MerchantApplication MerchantApplication { get; private set; }

        public int MerchantApplicationId { get; private set; }

        public ContactPersonRoles Role { get; private set; }

        #nullable disable
        private ApplicationContact() { }

        public ApplicationContact(string name, string phone, string idNumber, ContactPersonRoles role)
        :this()
        {
            Guard.Against.NullOrEmpty(name, nameof(name));

            Guard.Against.NullOrEmpty(phone, nameof(phone));

            Guard.Against.NullOrEmpty(idNumber, nameof(idNumber));

            Name = name;

            Phone = phone;

            IDNumber = idNumber;

            Role = role;

        }

       
    }

}

