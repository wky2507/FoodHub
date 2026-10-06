namespace FoodHub.Blazor.Client.Helper
{
    public static class MapService
    {

        public static string MapKindofFood(int number) {

            return number switch
            {

              

                1 => KindofFood.HotDish.GetDisplayName(),

                2 => KindofFood.ColdDish.GetDisplayName(),

                3 => KindofFood.Staple.GetDisplayName(),

                4 => KindofFood.Drink.GetDisplayName(),

                5 => KindofFood.Dessert.GetDisplayName(),

                _ => ""
            };


        }



    }
}
