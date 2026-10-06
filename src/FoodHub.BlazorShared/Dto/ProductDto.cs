using System.ComponentModel.DataAnnotations;

namespace FoodHub.BlazorShared.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="请输入菜品名称")]
        public string Name { get;  set; } = string.Empty;
        [Required(ErrorMessage ="请输入菜品的价格")]
        public decimal? Price { get;  set; }
        
        public int StoreId { get;  set; }

        [Required(ErrorMessage = "请选择菜品分类")]
        public KindofFood? SelectCategory { get; set; }

        [Required(ErrorMessage ="请选择菜品的种类")]
        public int CategoryName { get; set; }
        
        public bool IsOnsale { get;  set; } = false;

        public string? PictureUri { get; set; }

        //销售数量
        public int TodaySales { get; set; }

        public ProductDto() { }

        public ProductDto(int id,string name, decimal price, int storeId, int categoryName, string pictureUri) : this()
        {
            Id = id;

            Name = name;

            Price = price;

            StoreId = storeId;

            CategoryName = categoryName;

            PictureUri = pictureUri;

        }
    }
}
