namespace FoodHub.Blazor.Client.Constants
{
    public static class DraftKeys
    {
        public const string MerchantApply = "draft:merchant:apply";

        public const string MerchantProduct = "draft:merchant:product";
        //下面是我的case，回头可以模仿
        public const string StoreProfile = "draft:store:profile";

        public static string ProductEdit(Guid productId)
            => $"draft:product:{productId}";

        public static string CategoryEdit(Guid categoryId)
            => $"draft:category:{categoryId}";

    }
}
