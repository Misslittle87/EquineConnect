using EquineConnect.Core.Models;

namespace EquineConnect.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string?> Login(Login login);
        Task<string?> Register(Register register);
        //Task<string> ForgotPassword(ForgotPassword forgotPassword);
        //Task<string> ResetPassword(ResetPassword resetPassword);
    }
}
