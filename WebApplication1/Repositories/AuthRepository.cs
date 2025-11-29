using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Security;
using WebApplication1.Services;

namespace WebApplication1.Repositories
{
    public class AuthRepository(AppDbContext context , SignInManager<ApplicationUser>signInManager , UserManager<ApplicationUser>userManager
         ,RoleManager<IdentityRole>roleManager , IEmailService emailService , TokenService tokenService) : IAuthRepository
    {

        public async Task<string> Sign_upAsync(RegisterDto registerDto, string Role)
        {
            var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
            
            if (existingUser != null)
            {
               return " The Email is already registered";   
            }

            var newUser = new ApplicationUser()
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email

            };
            var result = await userManager.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
                return string.Join(',', result.Errors.Select(e => e.Description));

            if (!await roleManager.RoleExistsAsync(Role))
            {
                await roleManager.CreateAsync(new IdentityRole(Role));
            }
            await userManager.AddToRoleAsync(newUser, Role);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmationLink = $"https://localhost:7001/Auth/ConfirmEmail?userId={newUser.Id}&token={Uri.EscapeDataString(token)}";

            await emailService.SendEmailAsync(newUser.Email, "Confirm your email", $"Click the link to confirm your account: {confirmationLink}");

            return $"{newUser.Email} registered successfully";

        }
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var existUser = await userManager.FindByEmailAsync(loginDto.Email);
            if (existUser == null)
            {
                return "The Email is not Found";
            }
            var IsPassValid = await userManager.CheckPasswordAsync(existUser, loginDto.Password);

            if (!IsPassValid)
            {
                return "Password is not Correct ";
            }
            if (!existUser.EmailConfirmed)
            {
                return "Please, Confirm your Email";
            }
            
               
            return await tokenService.GenerateTokenAsync(existUser.Id);
        }
        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;


            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return true;
            }
            return false;


        }
        public async Task<bool> ResendConfirmationEmailAsync(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user is null || user.EmailConfirmed)
            {
                return false;
            }

         
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            string confirmationLink = $"https://localhost:7001/Auth/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            await emailService.SendEmailAsync(user.Email, "Confirm your email",
            $"Click the link to confirm your account: {confirmationLink}");

            return true;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var existingUser = await userManager.FindByEmailAsync(changePasswordDto.Email);
            if (existingUser == null)
            {
                return false;
            }
            var result = await userManager.ChangePasswordAsync(existingUser, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            return result.Succeeded;
        }
     

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email); 

            if (user == null)
            {
                return false;
            }
            var decodedToken = Uri.UnescapeDataString(resetPasswordDto.Token); 
            var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);

            return result.Succeeded;
        }

     
        public async Task<bool> ForgotPassword (string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
           
            if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
            {
                return false;
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Uri.EscapeDataString(token);

            var resetLink = $"https://localhost:7001/Auth/ResetPassword?email={user.Email}&token={encodedToken}";

            await  emailService.SendEmailAsync(user.Email, "Reset Password",
            $"Click the link to confirm your account: {resetLink}");

            return true ;

        }

        
    }
}
