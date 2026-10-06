using System.ComponentModel.DataAnnotations;
using FoodHub.Domain.Share.Constants;
namespace FoodHub.BlazorShared.Dto
{
    public class ContactDto
    {
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入电话")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入身份证号")]
        public string ID_Number { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "请选择联系人的角色")]
        public ContactPersonRoles PersonRoles { get; set; }

        public ContactDto(string name, string phone, string id_Number, ContactPersonRoles personRoles) {

            Name = name;

            Phone = phone;

            ID_Number = id_Number;

            PersonRoles = personRoles;
        }
        public ContactDto() { }
    }
}
