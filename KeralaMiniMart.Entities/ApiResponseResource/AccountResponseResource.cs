using KeralaMiniMart.Entities.ApiRequestResource;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.ApiResponseResource
{
    public class CommonResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
    }

    public class LoginResponse : CommonResponse
    {
        public int ApplicationUserId { get; set; }
    }
    public class PlaceOrderResponse : CommonResponse
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string DeliveryDate { get; set; }
        public string MessageError { get; set; }
    }

    public class RegisterWithImageResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string data { get; set; }
    }

    public class MyProfileResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public MyProfile data { get; set; }
    }

    public class RegisterApiResponseResource
    {
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
    }

    public class RegisterResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public RegisterApiResponseResource data { get; set; }
    }
    public class UpdateProfileResource
    {
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class UpdateProfileResponseResource
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public UpdateProfileResource data { get; set; }
    }
}
