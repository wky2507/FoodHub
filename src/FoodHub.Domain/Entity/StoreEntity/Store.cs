using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Interface;
using System.Globalization;

namespace FoodHub.Domain.Entity.StoreEntity
{
    public class Store : BaseEntity,IAggregateRoot
    {
        //唯一索引
        public string UserId { get; private set; } 

        public string StoreName { get; private set; } = string.Empty;

        public string StorePhone { get; private set; } = string.Empty;

        public string StoreAddress { get; private set; } = string.Empty;

        public string License_Image_Url { get; private set; } = string.Empty;

        public string Store_Image_Url { get; private set; } = string.Empty;

        public bool IsOpen { get; private set; } = false;

        public ApplicationUser ApplicationUser { get; private set; }

        //Product作为独立聚合根用
        //public ICollection<Product> Products { get; private set; } = new List<Product>();


        private List<StoreContact> _StoreContacts = new List<StoreContact>();

        public IReadOnlyCollection<StoreContact> StoreContacts => _StoreContacts.AsReadOnly();

        #pragma warning disable
        private Store() { }

        public Store(string userId,string name,string phone,string address,string image_Url,bool isOpen) :this()
        {
            UserId = userId;

            StoreName = name;

            StorePhone = phone;

            StoreAddress = address;

            License_Image_Url = image_Url;

            IsOpen = isOpen;
        }

        public void AddStoreImage(string url) {

            Store_Image_Url = url;
        
        }

    }
}