using FoodHub.BlazorShared.Request;
using FoodHub.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FoodHub.PublicApi.Controllers
{

    [Route("api/merchant/delete/")]
    [ApiController]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT + "," + RoleConstants.Roles.USER)]
    public class DeleteController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IRepository<Product> _productRepository;

        public DeleteController(IWebHostEnvironment webHostEnvironment,IRepository<Product> productRepository) {

            _webHostEnvironment = webHostEnvironment;

            _productRepository = productRepository;
        }

        [HttpDelete("dishesImage")]
        public async Task<ActionResult> DeleteDishesImgaeTemporory([FromQuery] DeleteFoodImageRequest request,CancellationToken cancellationToken) {

            var fileName = request.FileName.TrimStart('/');

            var product = await _productRepository.GetByIdAsync(request.Id,cancellationToken);

            if (string.IsNullOrWhiteSpace(fileName)) {

                return BadRequest("请求失败");

            }
            
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "DishesImages", fileName);

            if (System.IO.File.Exists(filePath))
            {

                System.IO.File.Delete(filePath);

            }

            if (product != null)
            {
                product.ChangePictureUri("/Uploads/Default/Anonymous.png");

                var affectLine = await _productRepository.SaveChangesAsync();

                if (affectLine > 0)
                {

                    return Ok(new { Message = "删除成功" });

                }

                return BadRequest();
            }

            return Ok();
        
        }




    }
}
