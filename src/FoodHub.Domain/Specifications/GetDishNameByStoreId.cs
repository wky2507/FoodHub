using Ardalis.Specification;
using FoodHub.Domain.Entity.StoreEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.Domain.Specifications
{
    public class GetDishNameByStoreId : Specification<Product>
    {

        public GetDishNameByStoreId(int storeId,string name) {

            Query.Where(c => c.StoreId == storeId && c.Name == name);
        
        }


    }
}
