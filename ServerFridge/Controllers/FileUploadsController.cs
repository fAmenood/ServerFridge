using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerFridge.Models;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;
        
        public FileUploadsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost] 
        public async Task<string> Post([FromForm] FileUpload fileUpload )
        {
            try
            {
                if(fileUpload.files.Length>0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + fileUpload.files.FileName)) 
                    {
                        fileUpload.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "Upload Done.";


                    }
                }
                else
                {
                    return "Something Failed.";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("{fileName}")]
        public IActionResult Get([FromRoute] string fileName)
        {
            var filePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "uploads",
                fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileStream = System.IO.File.OpenRead(filePath);
            return File(fileStream, "image/png"); // Автоматически закроет поток
        }
    }
}
