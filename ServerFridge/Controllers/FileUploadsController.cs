//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ServerFridge.Models;

//namespace ServerFridge.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FileUploadsController : ControllerBase
//    {
//        public static IWebHostEnvironment _webHostEnvironment;

//        public FileUploadsController(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }
//        [HttpPost] 
//        public async Task<string> Post([FromForm] FileUpload fileUpload )
//        {
//            try
//            {
//                if(fileUpload.files.Length>0)
//                {
//                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
//                    if(!Directory.Exists(path))
//                    {
//                        Directory.CreateDirectory(path);
//                    }
//                    using (FileStream fileStream = System.IO.File.Create(path + fileUpload.files.FileName)) 
//                    {
//                        fileUpload.files.CopyTo(fileStream);
//                        fileStream.Flush();
//                        return "Upload Done.";


//                    }
//                }
//                else
//                {
//                    return "Something Failed.";
//                }
//            }
//            catch(Exception ex)
//            {
//                return ex.Message;
//            }
//        }

//        [HttpGet("{fileName}")]
//        public IActionResult Get([FromRoute] string fileName)
//        {
//            var filePath = Path.Combine(
//                _webHostEnvironment.WebRootPath,
//                "uploads",
//                fileName);

//            if (!System.IO.File.Exists(filePath))
//                return NotFound();

//            var fileStream = System.IO.File.OpenRead(filePath);
//            return File(fileStream, "image/png");
//        }
//    }
//}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.Models;
using System.IO;
using System.Threading.Tasks;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context; // Контекст БД

        public FileUploadsController(
            IWebHostEnvironment webHostEnvironment,
            AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileUpload fileUpload)
        {
            try
            {
                // 1. Проверка существования продукта
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == fileUpload.ProductId);

                if (product == null)
                {
                    return NotFound($"Product with ID {fileUpload.ProductId} not found");
                }

                // 2. Проверка наличия файла
                if (fileUpload.files == null || fileUpload.files.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                // 3. Создание директории для загрузок
                string uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                // 4. Генерация уникального имени файла
                string uniqueFileName = $"{Guid.NewGuid()}_{fileUpload.files.FileName}";
                string filePath = Path.Combine(uploadsPath, uniqueFileName);

                // 5. Сохранение файла
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.files.CopyToAsync(fileStream);
                }

                // 6. Удаление старого изображения (если существует)
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // 7. Обновление пути изображения в БД
                product.ImagePath = Path.Combine("uploads", uniqueFileName);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Upload successful",
                    ImagePath = product.ImagePath
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{fileName}")]
        public IActionResult Get([FromRoute] string fileName)
        {
            try
            {
                var filePath = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "uploads",
                    fileName);

                if (!System.IO.File.Exists(filePath))
                    return NotFound();

                // Автоматическое определение типа контента
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var fileStream = System.IO.File.OpenRead(filePath);
                return File(fileStream, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}