using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.ApiRequestResource
{
    public class BaseRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AppId { get; set; }
        public string Subject { get; set; }
    }
    public class LoginRequest : BaseRequest
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
    public class ExternalLoginRequest : BaseRequest
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
    public class VerifyOtpRequest : BaseRequest
    {
        public string OTP { get; set; }
    }
    public class ResetPasswordRequest : BaseRequest
    {
        public string Password { get; set; }
    }
    public class RegisterWithData : ExternalLoginRequest
    {

        public string Password { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
    }
    public class UpdateProfileRequestResource : RegisterWithData
    {
        public int ApplicationUserId { get; set; }
    }
    public class RegisterWithImage
    {
        public IFormFile file { get; set; }
    }
    
    public class MyProfile
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string City { get; set; }
    }
    
}
