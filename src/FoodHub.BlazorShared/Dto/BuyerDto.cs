using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Dto
{
    public class BuyerDto
    {
        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public BuyerDto() { }

        public BuyerDto(string name, string phone) {

            Name = name;

            Phone = phone;
        }
    }
}
