using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IForgotPasswordRepository : IRepository<ForgotPassword>
    {
        ForgotPassword FindByEmailOtp(string Email, string OTP);
        ForgotPassword FindByPhoneNumberOtp(string phoneNumber, string OTP);
    }
}
