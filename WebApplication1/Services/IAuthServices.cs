using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IAuthServices 
    {
        Task<string> SignUpAsync(RegisterDto registerDto, string role);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<bool> ConfirmEmailAsync(string userId, string token);
        Task<bool> ResendConfirmationEmailAsync(string email);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);








    }
}
