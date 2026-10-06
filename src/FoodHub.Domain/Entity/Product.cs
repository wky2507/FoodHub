
using FoodHub.Domain.Entity.StoreEntity;
using FoodHub.Domain.Interface;
using System.Reflection.Metadata.Ecma335;

namespace FoodHub.Domain.Entity
{
    public class Product :  BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public decimal Price { get; private set; }
       
        public int StoreId { get; private set; }

        public bool IsOnsale { get; private set; } = false;

        public string? PictureUri { get; private set; }
        
        public int CategoryName { get; set; }
        //时间
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Store Store { get; private set; }

        #pragma warning disable
        private Product() { }

        public Product(string name, decimal price, int storeId, int categoryName, string? pictureUri,bool isOnSale):this()
        {
           
            Name = name;

            Price = price;

            StoreId = storeId;

            CategoryName = categoryName;

            PictureUri = pictureUri;

            IsOnsale = isOnSale;

        }

        public void ChangeOnSaleStatus() {

            IsOnsale = !IsOnsale;
        
        }
        public void ChangePictureUri(string path) {

            PictureUri = path;

        }

        public void UpdateProduct(string name, decimal price, int categoryName, string? pictureUri, bool isOnSale)
        {

            Name = name;

            Price = price;

            CategoryName = categoryName;

            PictureUri = pictureUri;

            IsOnsale = isOnSale;

        }
    }
}
