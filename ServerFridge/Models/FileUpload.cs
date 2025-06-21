namespace ServerFridge.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }


        public Guid ProductId { get; set; } 
    }
}
