using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.PublicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = RoleConstants.Roles.MERCHANT + "," + RoleConstants.Roles.USER)]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {

            _environment = environment;

        }

        [HttpPost("license")]
        public async Task<IActionResult> UploadLicense(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {

                var res = new UploadResultDto { Url = null };

                return BadRequest(res);

            }
            //拿到项目wwwroot目录路径
            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
            //拼接具体的路径
            var uploadsFolder = Path.Combine(webRootPath, "Uploads", "Licenses");
            
            if (!Directory.Exists(uploadsFolder))
            {

                Directory.CreateDirectory(uploadsFolder);

            }

            var fileExtension = Path.GetExtension(file.FileName);

            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(stream);

            }

            var relativeUrl = $"/Uploads/Licenses/{uniqueFileName}";

            return Ok(new UploadResultDto { Url = relativeUrl });
        }

        [HttpPost("dishesImage")]
        public async Task<IActionResult> UploadDishesImage(IFormFile file) {

            if (file == null || file.Length == 0) {

                var res = new UploadResultDto { Url = null };

                return BadRequest(res);
            }

            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
            //文件路径
            var uploadsFolder = Path.Combine(webRootPath, "Uploads", "DishesImages");

            if (!Directory.Exists(uploadsFolder)) {

                Directory.CreateDirectory(uploadsFolder);

            }
            //拿了文件名的后缀(jpg、png)
            var fileExtension = Path.GetExtension(file.FileName);
            //唯一的文件名
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            //文件路径加文件名
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(stream);

            }

            var relativeUrl = $"/Uploads/DishesImages/{uniqueFileName}";

            return Ok(new UploadResultDto { Url = relativeUrl });
        }

    }
}
