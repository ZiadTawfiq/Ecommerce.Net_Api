using WebApplication1.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class AuthServices(IAuthRepository authRepository) : IAuthServices
    {
        public Task<string> SignUpAsync(RegisterDto registerDto, string Role)
        {
            return authRepository.Sign_upAsync(registerDto, Role);
        }
        public Task<string> LoginAsync(LoginDto loginDto)
        {
            return authRepository.LoginAsync(loginDto);
        }
        public Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            return authRepository.ResetPasswordAsync(resetPasswordDto);
        }
        public Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            return authRepository.ChangePasswordAsync(changePasswordDto);
        }
        public Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            return authRepository.ConfirmEmailAsync(userId, token);
        }
        public Task<bool> ResendConfirmationEmailAsync(string email)
        {
            return authRepository.ResendConfirmationEmailAsync(email);
        }
        public Task<bool> ForgotPasswordAsync(string email)
        {
            return authRepository.ForgotPassword(email);
        }

    }
}
