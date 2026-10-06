using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace FoodHub.Domain.Entity.BasketAggregate
{
    public class BasketItem :BaseEntity
    {
        public decimal UnitPrice { get; private set; }

        public int Count { get; private set; }

        public int ProductId { get; private set; }

        public int BasketId { get; private set; }

        public Basket Basket { get; private set; }

        #pragma warning disable
        private BasketItem() { }

       public BasketItem(decimal unitPrice,int productId, int count) :this()
        {
            Guard.Against.Negative(unitPrice);

            Guard.Against.OutOfRange(productId,nameof(productId),1,int.MaxValue);

            SetCount(count);
        }

        public void AddCount(int count) {

            Guard.Against.OutOfRange(count, nameof(count), 0, int.MaxValue);

            Count += count;
        
        }

        public void SetCount(int count) {

            Guard.Against.OutOfRange(count, nameof(count), 0, int.MaxValue);

            Count = count;
        
        }


    }
}
