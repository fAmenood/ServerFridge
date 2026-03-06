//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.StaticFiles;
//using Microsoft.EntityFrameworkCore;
//using ServerFridge.DataContext;
//using ServerFridge.Models;
//using System.IO;
//using System.Threading.Tasks;

//namespace ServerFridge.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FileUploadsController : ControllerBase
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly AppDbContext _context;
//        private const string UploadsFolder = "uploads"; 

//        public FileUploadsController(
//            IWebHostEnvironment webHostEnvironment,
//            AppDbContext context)
//        {
//            _webHostEnvironment = webHostEnvironment;
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post([FromForm] FileUpload fileUpload)
//        {
//            try
//            {
//                var product = await _context.Products
//                    .FirstOrDefaultAsync(p => p.Id == fileUpload.ProductId);

//                if (product == null)
//                {
//                    return NotFound($"Product with ID {fileUpload.ProductId} not found");
//                }

//                if (fileUpload.files == null || fileUpload.files.Length == 0)
//                {
//                    return BadRequest("No file uploaded");
//                }


//                string uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, UploadsFolder);
//                if (!Directory.Exists(uploadsPath))
//                {
//                    Directory.CreateDirectory(uploadsPath);
//                }


//                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileUpload.files.FileName)}";
//                string filePath = Path.Combine(uploadsPath, uniqueFileName);


//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {
//                    await fileUpload.files.CopyToAsync(fileStream);
//                }


//                if (!string.IsNullOrEmpty(product.ImagePath))
//                {
//                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImagePath);
//                    if (System.IO.File.Exists(oldFilePath))
//                    {
//                        System.IO.File.Delete(oldFilePath);
//                    }
//                }

//                // Сохранение относительного пути
//                product.ImagePath = Path.Combine(UploadsFolder, uniqueFileName);
//                _context.Products.Update(product);
//                await _context.SaveChangesAsync();

//                return Ok(new
//                {
//                    Message = "Upload successful",
//                    ImagePath = product.ImagePath 
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpGet("{*filePath}")] // Разрешаем относительные пути
//        public IActionResult Get([FromRoute] string filePath)
//        {
//            try
//            {
//                // Безопасное объединение путей
//                string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

//                // Проверка безопасности пути
//                if (!fullPath.StartsWith(Path.Combine(_webHostEnvironment.WebRootPath, UploadsFolder)))
//                {
//                    return BadRequest("Invalid file path");
//                }

//                if (!System.IO.File.Exists(fullPath))
//                    return NotFound();

//                var provider = new FileExtensionContentTypeProvider();
//                if (!provider.TryGetContentType(fullPath, out var contentType))
//                {
//                    contentType = "application/octet-stream";
//                }

//                var fileStream = System.IO.File.OpenRead(fullPath);
//                return File(fileStream, contentType);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }
//    }
//}
using Microsoft.AspNetCore.Authorization;
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
        private readonly AppDbContext _context;
        private const string UploadsFolder = "uploads";

        public FileUploadsController(
            IWebHostEnvironment webHostEnvironment,
            AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post([FromForm] FileUpload fileUpload)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == fileUpload.ProductId);

                if (product == null)
                {
                    return NotFound($"Product with ID {fileUpload.ProductId} not found");
                }

                if (fileUpload.files == null || fileUpload.files.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }


                string uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, UploadsFolder);
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }


                string fileExtension = Path.GetExtension(fileUpload.files.FileName);
                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string fullFilePath = Path.Combine(uploadsPath, uniqueFileName);

                using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await fileUpload.files.CopyToAsync(fileStream);
                }


                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    string oldFilePath = Path.Combine(uploadsPath, product.ImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                product.ImagePath = uniqueFileName;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Upload successful",
                    FileName = uniqueFileName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Policy = "AllUsers")]
        [HttpGet("{fileName}")]
        public IActionResult Get([FromRoute] string fileName)
        {
            try
            {
                var safeFileName = Path.GetFileName(fileName);
                var filePath = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    UploadsFolder,
                    safeFileName);

                if (!System.IO.File.Exists(filePath))
                    return NotFound();

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