using FoodHub.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
namespace FoodHub.Domain.Entity.BasketAggregate
{
    public class Basket : BaseEntity,IAggregateRoot
    {

        private List<BasketItem> _items = new List<BasketItem>();

        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
        
        public int BuyerId { get; private set; }

        #pragma warning disable
        private Basket() { }

        public Basket(int buyerId) :this()
        {

            BuyerId = buyerId;
        
        }
    
        public int TotalCount => _items.Sum( i => i.Count );

        public void SetNewBuyerId(int id){

            BuyerId = id;
        
        }

        public void RemoveItem(int id) {
            try
            {
                var item = _items.FirstOrDefault(i => i.ProductId == id);

                if (item != null)
                {

                    _items.Remove(item);

                }
            }
            catch (Exception ex) { 
            
                Console.WriteLine(ex.Message);

            }

        }

        public void RemoveEmptyItem() 
        {
            _items.RemoveAll(i => i.Count == 0);
        
        }



        public void AddItem(decimal unitPrice, int productId,int count = 1) {

            if (!_items.Any(i => i.ProductId == productId)){
                
                _items.Add(new BasketItem(unitPrice, productId, count));

                return;

            }
            try
            {
                var tempItem = _items.First(i => i.ProductId == productId);

                tempItem.AddCount(count);

            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message);
            
            }

        }


    }
}
