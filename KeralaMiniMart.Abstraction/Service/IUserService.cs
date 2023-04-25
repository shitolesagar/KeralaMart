using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.ApiResponseResource;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface IUserService
    {
        RegisterResponse RegisterUserWithData(RegisterWithData request);

        CommonResponse UpdateProfile(UpdateProfileRequestResource request);

        Task<RegisterWithImageResponse> RegisterUserWithImage(RegisterWithImage request);

        LoginResponse LoginUser(LoginRequest request);

        CommonResponse OTPSend(BaseRequest request);

        CommonResponse ResetPassword(ResetPasswordRequest request);

        Task<CustomerWrapperViewModel> GetWrapperForIndexView(FilterBase filter);

        CommonResponse VerifyOTP(VerifyOtpRequest request);
        ApplicationUser LoginAdmin(string email);
        MyProfileResponse GetMyProfile(int Id);
        Task<CustomerDetailsViewModel> GetCustomerDetails(int id);
    }
}