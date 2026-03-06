namespace ServerFridge.DTOModels
{
    public class JWTResponseDTO
    {
        public string JWTToken {  get; set; }   
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public DateTime timeToSign { get; set; }
    }
}
