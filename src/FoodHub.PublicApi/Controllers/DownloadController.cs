using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FoodHub.PublicApi.Controllers
{
    [ApiController]
    [Route("api/download/")]
    [Authorize(Roles = RoleConstants.Roles.USER + "," + RoleConstants.Roles.ADMINISTRATORS + "," +RoleConstants.Roles.MERCHANT)]
    //[Authorize(Roles = RoleConstants.Roles.MERCHANT + "," + RoleConstants.Roles.USER)]
    public class DownloadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        private string filePath = string.Empty;

        private IWebHostEnvironment _hostEnvironment { get; set; }
        public DownloadController(IWebHostEnvironment environment,IWebHostEnvironment hostEnvironment) {

            _environment = environment;
        
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("license/{fileName}")]
        public IActionResult DownloadLicenseImage(string fileName) {


            if (string.IsNullOrWhiteSpace(fileName)) {

                return BadRequest("请求参数异常!");

            }
            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

            string filePath;

            filePath = Path.Combine(webRootPath, "Uploads", "Licenses", fileName);

            if (!System.IO.File.Exists(filePath)) {
               
                return NotFound("文件不存在");
            
            }

            var name = Path.GetFileName(filePath);
            //读取磁盘真实的文件，返回给浏览器
            return PhysicalFile(filePath, "image/png", name);

        }

        [HttpGet("dishesImage/{fileName?}")]
        public IActionResult DownloadDishesImage(string? fileName)
        {


            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

            filePath = Path.Combine(webRootPath, "Uploads", "DishesImages", fileName ?? "");

            Console.WriteLine($"路径是{filePath}");

            if (!System.IO.File.Exists(filePath))
            {

                var defaultImageName = "Anonymous.png";

                defaultImageName = Path.GetFileName(defaultImageName);

                filePath = Path.Combine(webRootPath, "Uploads", "Default", defaultImageName);

                Console.WriteLine($"路径是{filePath}");

                return PhysicalFile(filePath, "image/png", "Anonymous.png");
            }

            var name = Path.GetFileName(filePath);

            return PhysicalFile(filePath, "image/png", name);

        }
        //[HttpGet("dishesImage")]
        //public IActionResult test()
        //{

        //    return Ok();
        //}
    }
}
