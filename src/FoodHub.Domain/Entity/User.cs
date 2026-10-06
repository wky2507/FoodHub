using FoodHub.Domain.Entity;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
namespace FoodHub.Domain.Entity
{
    public class User : BaseEntity
    {

         public string Name { get; private set; }

         public string Phone { get; private set; }

         public string PasswordHash { get; private set; }


        #pragma warning disable
        private User() { }

        public User(string name, string phone, string passwordHash):this()
        {
            Name = name;

            Phone = phone;

            PasswordHash = passwordHash;
        }

    }
}
