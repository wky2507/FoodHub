using Ardalis.GuardClauses;
using FoodHub.Domain.Interface;

namespace FoodHub.Domain.Entity.BuyerAggregate
{
    public class Buyer : BaseEntity,IAggregateRoot
    {
        public string IdentityGuid { get;private set; }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();

        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();
        
        #pragma warning disable 
        private Buyer() { }

        public Buyer(string name,string phone) :this()
        {

            Guard.Against.NullOrEmpty(name);

            Guard.Against.NullOrEmpty(phone);

            IdentityGuid = Guid.NewGuid().ToString();

            Name = name;

            Phone = phone;
        }

    }
}
