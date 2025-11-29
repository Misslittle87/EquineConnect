using EquineConnect.Core.Interfaces;
using EquineConnect.Core.Models;
using EquineConnect.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EquineConnect.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        //private readonly IEmailSender _emailSender;
        public AuthService(UserManager<User> userManager, IConfiguration config /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _config = config;
            //_emailSender = emailSender;
        }

        public async Task<string?> Register(Register register)
        {
            var user = new User { UserName = register.UserName, Email = register.Email };
            var result = await _userManager.CreateAsync(user, register.Password);

            return result.Succeeded ? "User created" : null;
        }

        public async Task<string?> Login(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, login.Password)))
                return null;

            var expirationTime = login.RememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1);

            return GenerateTokenString(user, expirationTime);
        }

        public string GenerateTokenString(User model, DateTime expirationTime)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Id),
                    new Claim(ClaimTypes.Email, model.Email)
                }),
                Expires = expirationTime,
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<string> ForgotPassword(ForgotPassword forgotPassword)
        //{
        //    var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
        //    if (user == null)
        //    {
        //        return "Invalid request";
        //    }

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var resetLink = $"http://localhost:7171/reset-password?token={token}&email={forgotPassword.Email}";

        //    await _emailSender.SendEmailAsync(forgotPassword.Email, "Password Reset", $"Click here to reset your password: {resetLink}");

        //    return "Password reset link sent.";
        //}

        //public async Task<string> ResetPassword(ResetPassword resetPassword)
        //{
        //    var user = await _userManager.FindByEmailAsync(resetPassword.Email);
        //    if (user == null)
        //    {
        //        return "Invalid request";
        //    }

        //    var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);
        //    if (!result.Succeeded)
        //    {
        //        return "Failed to reset password.";
        //    }

        //    return "Password has been reset successfully.";
        //}
    }
}
