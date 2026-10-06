
using Ardalis.GuardClauses;
namespace FoodHub.Domain.Entity.BuyerAggregate
{
    public class PaymentMethod : BaseEntity
    {
        public string Name { get; private set; }

        public int BuyerId { get; private set; }

        public Buyer Buyer { get; private set; }

        #pragma warning disable
        private PaymentMethod() { }

        public PaymentMethod(string name):this()
        {
            
            Guard.Against.NullOrEmpty(name,nameof(name));

            Name = name;
        
        }

    }
}
