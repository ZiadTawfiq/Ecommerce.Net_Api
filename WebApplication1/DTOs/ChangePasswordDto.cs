namespace WebApplication1.DTOs
{
    public class ChangePasswordDto
    {
        public string Email { get; set;}
        public string OldPassword { get; set;}
        public string NewPassword { get; set;}
    }
}
