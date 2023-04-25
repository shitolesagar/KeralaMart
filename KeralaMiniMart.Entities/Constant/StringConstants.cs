using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Constant
{
    public static class StringConstants
    {
        public static string LoginError { get; } = "Please enter registered mobile number or password";
        public static string AppId { get; } = "KeralaMiniMart";

        public static readonly string ServerError = "Something went wrong.";
        public static readonly string DeletedMessage = "Record Deleted Successfully";
        public static readonly string ProductDeleted = "Product Deleted Successfully";
        public static readonly string ProductNotFound = "Product not found.";
        public static readonly string SubCatDeleted = "Filter Deleted Successfully";
        public static readonly string CatDeleted = "Category Deleted Successfully";
        public static readonly string CatNotFound = "Category not found.";
        public static readonly string ImageUploadError = "Unable to upload. please try again.";
       // public static readonly string LoginError = "Could not verify username or password";
        public static readonly string Success = "Succesfull";
        public static readonly string StatusCode10 = "10";
        public static readonly string StatusCode20 = "20";
        public static readonly string StatusCode30 = "30";
        public static readonly string StatusCode40 = "40";
        public static readonly string NotFound = "Record Not Found";
        public static readonly string Filter = "Filter not belongs to the given category.";
        public static readonly string AddedMessage = "Record Added Successfully";
        public static readonly string ProductAdded = "Product Added Successfully";
        public static readonly string SubCatAdded = "Filter Added Successfully";
        public static readonly string SubCatNotFound = "Filter not found.";
        public static readonly string CatAdded = "Category Added Successfully";
        public static readonly string ProductUpdated = "Product Updated Successfully";
        public static readonly string QuantityUpdated = "Product quantity updated successfully";
        public static readonly string SubCatUpdated = "Filter Updated Successfully";
        public static readonly string UserUpdated = "User Updated Successfully";
        public static readonly string CatUpdated = "Category Updated Successfully";
        public static readonly string InvalidFile = "Invalid file type";
        public static readonly string EmptyFile = "Empty File ";
        public static readonly string FileNotFound = "File not found.";
        public static readonly string LargeImage = "Image exciding 2 MB";
        public static readonly string ImageSaved = "Image saved succesfully.";
        public static readonly string RecordExist = "Record Already Exist";
        public static readonly string TokenExist = "Token Already Exist";
        public static readonly string ProdNameRequired = "Product name is required.";
        public static readonly string Sagar = "sagar.shitole@omni-bridge.net";
        public static readonly string Rupali = "rupali.yenare@omni-bridge.net";
        public static readonly string KeralaMart = "keralaminimart@gmail.com";
        public static readonly string ItemExist = "Item already exist in the cart";
        public static readonly string AppIdNull = "AppId can not be null";
        public static readonly string ItemAdded = "Item added to cart Successfully.";
        public static readonly string AddressAdded = "Address Added Successfully.";
        public static readonly string AddressUpdated = "Address Updated Successfully.";
        public static readonly string AddressDeleted = "Address Deleted Successfully.";
        public static readonly string UserExist = "User with this mobile number is already exist";
        public static readonly string UserSaved = "User saved Successfully.";
        public static readonly string UserVerified = "User verified successfully.";
        public static readonly string NoUser = "User not found";
        public static readonly string LoginSuccess = "Login Successfully.";
        public static readonly string PasswordReset = "Password Reset sucessfully.";
        public static readonly string OrderBooked = "Order Booked Successfully.";
        public static readonly string LibNotFound = "Library not found.";
        public static readonly string OrderPlaced = "Order Placed Successfully.";
        public static readonly string UserNotAuthorised = "You are not Authorize. Please contact Support team";
        public static readonly string ResetPasswordOTPSent = "Reset password Code is sent on your registered mobile number";
        public static readonly string RegistrationOTPSent = "Registration Code is sent on your registered mobile number";
        public static readonly string OTPNotMatch = "Could not verify OTP";
        public static readonly string OTPMatch = "OTP verified successfully";
        public static readonly string Message = "Successfull";
        public static readonly string SubtotalPriceZero = "Subtotal price cannot be zero";
        public static readonly string OTPNotVerified = "User mobile number is not verified, please use forgot password to verify the mobile number";
        public static readonly string OTPConfirmation = "Kerala Mini Mart OTP Confirmation";
        public static readonly string ResetPasswordOTP = "Kerala Mini Mart Reset Password";
        public static readonly string TransactionIdUpdated = "TransactionId Updated Successfully";
        public static readonly string TokenAdded = "Token Added Successfully";
        public static readonly string CantAddMore = "Sorry, you can't add more of this item";
        public static readonly string RateChanged = "Rate for ordered products has been changed.Please verify again.";
        public static readonly string OutOfStock = "Oops! looks like few items went out of stock. Please remove them from the cart list and then place the order.";

        public static string ImageNotification { get; } = "Image";
        public static string TextNotification { get; } = "Text";
        public static string ProductRemovedFromCart { get; set; } = "Product removed from cart successfully.";

        public static string GetOrderNotificationMessage(string orderNumber)
        {
            return "Your order number " + orderNumber + " placed successfully";
        }
    }
}
