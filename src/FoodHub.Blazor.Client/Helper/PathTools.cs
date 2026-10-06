using Microsoft.Extensions.Options;

namespace FoodHub.Blazor.Client.Helper
{
    public class PathTools
    {
        private IOptions<BaseUrlConfiguration> _options { get; set; } = default!;

        private readonly string ApiPath = string.Empty;

        public PathTools(IOptions<BaseUrlConfiguration> options) {

            _options = options;

            ApiPath = options.Value.ApiBase;
        
        }

        public string DishesImageDownloadCombine(string? relativePath) {

            var fileName = Path.GetFileName(relativePath);

            var path = Path.Combine(ApiPath,"download","dishesImage",fileName ?? "");

            return path;
        }


        public string DishesImageUploadCombine() {

            var path = Path.Combine(ApiPath, "Upload", "dishesImage");

            return path;
        }

        public string LisenceImageCombine() {

            var path = Path.Combine(ApiPath, "Upload", "license");

            return path;
        }
    }
}
