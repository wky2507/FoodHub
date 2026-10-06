using Ardalis.Specification;


namespace FoodHub.Domain.Specifications
{
    public class GetDishesListSpec : Specification<Product>
    {

        public GetDishesListSpec(int storeId,int page,int pageSize, KindofFood? category, bool? isOnsale,string? keywords ) {

            Query.Where(c => c.StoreId == storeId);

            if (category.HasValue) {
            
                Query.Where(x => x.CategoryName == (int)category.Value);
            
            }
            if (isOnsale.HasValue) {

                Query.Where(x => x.IsOnsale == isOnsale.Value);

            }


            if (!string.IsNullOrWhiteSpace(keywords)) {

                Query.Where(c => c.Name.Contains(keywords));
            
            }

            var validatePage = (page < 1) ? 1 : page;

            var validatePageSize = (pageSize < 10) ? 10 : pageSize;

            var skip = (validatePage - 1) * validatePageSize;

            Query.OrderByDescending(c => c.CreateAt)
                 .Skip(skip)
                 .Take(validatePageSize);


        }


    }
}
