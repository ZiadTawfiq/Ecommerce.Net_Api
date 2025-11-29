using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface IAuthRepository
    {
        Task<string> Sign_upAsync(RegisterDto registerDto, string Role);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<bool> ConfirmEmailAsync(string userId, string token);

        Task<bool> ResendConfirmationEmailAsync(string Email);
        Task<bool> ForgotPassword(string Email);

    }
}
