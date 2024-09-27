namespace API.Data.Dto
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string message { get; set; }
    }
}