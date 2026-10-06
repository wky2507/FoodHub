using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
namespace FoodHub.BlazorShared.Dto
{
    public class ApplicantDto :BaseRequest
    {
        public ApplicantDto(string storeName, string address, string licenseImage_Url,string phone,IEnumerable<ContactDto> contacts)
        {
            StoreName = storeName;

            Address = address;
            
            LicenseImage_Url = licenseImage_Url;

            Phone = phone;

            foreach (var contact in contacts) {

                Contacts.Add(new ContactDto(contact.Name, contact.Phone, contact.ID_Number, contact.PersonRoles));

            }
        }

        public ApplicantDto() { }

        [Required(ErrorMessage = "店铺名称不能为空")]
        [StringLength(100, ErrorMessage = "店铺名称最多 100 个字符")]
        public string StoreName { get; set; } = string.Empty;

        [Required(ErrorMessage = "店铺地址不能为空")]
        [StringLength(200, ErrorMessage = "店铺地址最多 200 个字符")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage ="需要上传营业执照")]
        public string LicenseImage_Url { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入联系电话")]
        public string Phone { get; set; } = string.Empty;

        [MinLength(1,ErrorMessage ="至少要有一个联系人")]
        public List<ContactDto> Contacts { get; set; } = new();

       
    }
}
