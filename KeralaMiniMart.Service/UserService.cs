using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities;
using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.ApiResponseResource;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Service.ExtensionMethods;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KeralaMiniMart.Service
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private ISmsService _smsService;
        private IFileServices _fileServices;
        private IProductImagesRepository _productImagesRepository;
        private IUserAddressRepository _userAddressRepository;
        private IOrderRepository _orderSummaryRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        private IApplicationUserRepository _applicationUserRepository;
        private IForgotPasswordRepository _forgotPasswordRepository;


        public UserService(IOptions<AppSettings> option, ICategoryRepository categoryRepository, IFileServices fileServices, ISmsService smsService, IForgotPasswordRepository forgotPasswordRepository, IApplicationUserRepository applicationUserRepository, IOrderDetailsRepository orderDetailsRepository, IOrderRepository orderSummaryRepository, IUserAddressRepository userAddressRepository, IProductImagesRepository productImagesRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _appSettings = option.Value;
            _categoryRepository = categoryRepository;
            _productImagesRepository = productImagesRepository;
            _userAddressRepository = userAddressRepository;
            _orderSummaryRepository = orderSummaryRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _applicationUserRepository = applicationUserRepository;
            _forgotPasswordRepository = forgotPasswordRepository;
            _smsService = smsService;
            _fileServices = fileServices;
        }

        #region RegisterUserWithData
        public RegisterResponse RegisterUserWithData(RegisterWithData request)
        {

            RegisterResponse res = new RegisterResponse();

            var user = _applicationUserRepository.FindByPhoneNumber(request.PhoneNumber);
            try
            {
                if (user != null)
                {
                    res.error = true;
                    res.Message = StringConstants.UserExist;
                    return res;
                }
                else
                {
                    user = new ApplicationUser
                    {
                        Name = request.Name,
                        Email = request.EmailId,
                        RoleId = 2,
                        MobileNumber = request.PhoneNumber,
                        City = request.City,
                        ProfilePicture = request.Image,
                    };

                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                        user.PasswordHash = passwordHash;
                        user.PasswordSalt = passwordSalt;
                        user.CreatedDateTime = DateTime.Now;
                    }
                    _applicationUserRepository.Add(user);
                    _unitOfWork.SaveChanges();
                    var obj = new RegisterApiResponseResource
                    {
                        ApplicationUserId = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.MobileNumber,
                        City = user.City,
                    };
                    res.data = obj;
                    res.Message = StringConstants.UserSaved;
                    return res;
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region UpdateProfile
        public CommonResponse UpdateProfile(UpdateProfileRequestResource request)
        {
            CommonResponse res = new CommonResponse();
            var user = _applicationUserRepository.FindById(request.ApplicationUserId);
            try
            {
                if (user != null)
                {
                    user.Email = request.EmailId;
                    user.Name = request.Name;
                    user.MobileNumber = request.PhoneNumber;
                    user.ProfilePicture = request.Image;
                    user.City = request.City;
                    _unitOfWork.SaveChanges();
                    res.Message = StringConstants.UserUpdated;
                    return res;
                }
                else
                {
                    res.error = true;
                    res.Message = StringConstants.NotFound; ;
                    return res;
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region RegisterUserWithImage
        public async Task<RegisterWithImageResponse> RegisterUserWithImage(RegisterWithImage request)
        {
            RegisterWithImageResponse res = new RegisterWithImageResponse();
            try
            {
                string relativePath = await _fileServices.SaveImageAndReturnRelativePath(request.file, FolderConstants.UserFolder);
                res.Message = StringConstants.Success;
                res.data = relativePath;
                return res;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                res.error = true;
                res.Message = StringConstants.ServerError;
                res.ErrorMessage = e.Message;
                return res;
            }
        }
        #endregion


        #region Login
        public LoginResponse LoginUser(LoginRequest request)
        {
            LoginResponse res = new LoginResponse();
            var user = _applicationUserRepository.FindByPhoneNumber(request.PhoneNumber);
            if (user != null)
            {
                if (user.IsOTPVerified == false)
                {
                    res.error = true;
                    res.Message = StringConstants.OTPNotVerified;
                    return res;
                }
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    res.error = true;
                    res.Message = StringConstants.LoginError;
                    return res;
                }
                res.Message = StringConstants.LoginSuccess;
                res.ApplicationUserId = user.Id;
                return res;
            }
            res.error = true;
            res.Message = StringConstants.LoginError;
            return res;
        }


        public ApplicationUser LoginAdmin(string email)
        {
            var user = _applicationUserRepository.FindByEmail(email);
            return user;
        }
        #endregion

        #region SendOTP
        public CommonResponse OTPSend(BaseRequest request)
        {
            CommonResponse res = new CommonResponse();

            try
            {
                ApplicationUser user = _applicationUserRepository.FindByPhoneNumber(request.PhoneNumber);
                if (user != null)
                {
                    if (user.RoleId != 2)
                    {
                        res.error = true;
                        res.Message = StringConstants.UserNotAuthorised;
                        return res;
                    }

                    string code = CreateOtp();
                    ForgotPassword record = new ForgotPassword()
                    {
                        ExpireDate = DateTime.Now.AddMinutes(30),
                        OTP = code,
                        ApplicationUserId = user.Id,
                    };
                    _forgotPasswordRepository.Add(record);
                    _unitOfWork.SaveChanges();

                    var smsResponse = _smsService.SendSms(user.MobileNumber, _smsService.CreateAccountVerificationMessage(code));
                    res.Message = StringConstants.ResetPasswordOTPSent;
                    record.SMSResponse = smsResponse;
                    _unitOfWork.SaveChanges();
                    return res;
                }
                else
                {
                    res.error = true;
                    res.Message = StringConstants.NoUser;
                    return res;
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }

        #endregion

        #region VerifyOTP
        public CommonResponse VerifyOTP(VerifyOtpRequest request)
        {
            CommonResponse res = new CommonResponse();
            var userData = _forgotPasswordRepository.FindByPhoneNumberOtp(request.PhoneNumber, request.OTP);
            if (userData != null)
            {
                if (userData.OTP == request.OTP && userData.ExpireDate > DateTime.Now && !userData.IsUsed)
                {
                    var user = _applicationUserRepository.FindByPhoneNumber(request.PhoneNumber);
                    user.IsOTPVerified = true;
                    userData.IsUsed = true;
                    _unitOfWork.SaveChanges();
                    res.Message = StringConstants.OTPMatch;
                    return res;
                }
                res.error = true;
                res.Message = StringConstants.OTPNotMatch;
                return res;
            }
            res.error = true;
            res.Message = StringConstants.OTPNotMatch;
            return res;
        }
        #endregion


        #region ResetPassword
        public CommonResponse ResetPassword(ResetPasswordRequest request)
        {
            CommonResponse res = new CommonResponse();
            var user = _applicationUserRepository.FindByPhoneNumber(request.PhoneNumber);
            if (user != null)
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.PasswordReset;
                return res;
            }
            res.error = true;
            res.Message = StringConstants.ServerError;
            return res;
        }
        #endregion

        #region GetMyProfile
        public MyProfileResponse GetMyProfile(int Id)
        {
            MyProfileResponse res = new MyProfileResponse();
            try
            {
                var profile = _applicationUserRepository.FindById(Id);
                MyProfile obj = new MyProfile()
                {
                    Name = profile.Name,
                    MobileNumber = profile.MobileNumber,
                    EmailId = profile.Email,
                    City = profile.City,
                    ProfilePicture = string.IsNullOrEmpty(profile.ProfilePicture) ? null : profile.ProfilePicture
                };
                res.Message = StringConstants.Success;
                res.data = obj;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        public async Task<CustomerDetailsViewModel> GetCustomerDetails(int id)
        {
            CustomerDetailsViewModel model;
            ApplicationUser user = await _applicationUserRepository.GetCustomerDetails(id);
            if (user == null)
                return null;
            model = new CustomerDetailsViewModel()
            {
                Email = user.Email,
                ImagePath = string.IsNullOrEmpty(user.ProfilePicture) ? null : _appSettings.WebApiBaseUrl + user.ProfilePicture,
                MobileNumber = user.MobileNumber,
                Id = user.Id,
                Name = user.Name,
                City = user.City,
                RegisteredDate = user.CreatedDateTime.ToStringDatePattern()
            };
            model.UserAddressList = user.UserAddresses.Select((x, index) => new UserAddressViewModels()
            {
                Address = x.Address,
                Locality = x.Locality,
                Landmark = x.Landmark,
                MobileNumber = x.MobileNumber,
                Number = index + 1,
                Pincode = x.Pincode
            }).ToList();
            model.OrderList = user.Orders.Select((x, index) => new OrderListViewModel()
            {
                CreatedDate = x.CreatedDate.ToStringDatePattern(),
                Id = x.Id,
                Number = index + 1,
                OrderNumber = x.OrderNumber,
                Status = x.DeliveryStatus.Status
            }).ToList();
            return model;
        }

        #region Private Methods

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private Object CreateResponseAfterSuccessfulAuthantication(ApplicationUser user)
        {
            return new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(CreateClaimsAndJwtToken(user)),
                UserId = user.Id,
                EmailId = user.Email,
                Name = user.Name
            };
        }
        private JwtSecurityToken CreateClaimsAndJwtToken(ApplicationUser user)
        {
            var claims = new[]
               {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my secreate key please check"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string ForgotPasswordMailBody(string code, string baseUrl)
        {
            baseUrl += "Account/ResetPassword?metadata=" + code;
            string msg = "<p>Hi User,</p>\r\n<p>&nbsp; &nbsp; &nbsp;To reset your password, please <a title=\"Reset password link\" href=\"" + baseUrl + "\" target=\"_blank\" rel=\"noopener\">click here</a>. This link is valid for 12 hours.</p>\r\n<p>Thanks &amp; Regards <br /> Plugable Team</p>";
            return msg;
        }

        private string ForgotPasswordOtpMailBody(string otp)
        {
            string msg = "<p>Dear Customer,</p>\r\n<p>&nbsp; &nbsp; &nbsp; To reset your password, OTP is " + otp + ". This OTP is valid for 30 min. Do not share it with anyone.</p>\r\n<p>Thanks &amp; Regards <br /> Kerala Mini Mart Team</p>";
            return msg;
        }

        private string RegistrationOtpMailBody(string otp)
        {
            string msg = "<p>Dear Customer,</p>\r\n<p>&nbsp; &nbsp; &nbsp; To register your account, please verify OTP as " + otp + ". This OTP is valid for 30 min. Do not share it with anyone.</p>\r\n<p>Thanks &amp; Regards <br /> Kerala Mini Mart Team</p>";
            return msg;
        }

        private string CreateGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        private string CreateOtp()
        {
            return "8899";
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random otp = new Random();
            for (int i = 0; i < 4; i++)
            {
                int pos = otp.Next(1, charArr.Length);
                strrandom += charArr.GetValue(pos);
            }
            return strrandom;
        }

        public async Task<CustomerWrapperViewModel> GetWrapperForIndexView(FilterBase filter)
        {
            CustomerWrapperViewModel ResponseModel = new CustomerWrapperViewModel
            {
                TotalCount = _applicationUserRepository.GetCustomerIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<ApplicationUser> list = await _applicationUserRepository.GetCusotmerIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.UserList = list.Select((x, index) => new CustomerListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                CreatedDate = x.CreatedDateTime.ToStringDatePattern(),
                Number = ResponseModel.PagingData.FromRecord + index,
                PhoneNumber = x.MobileNumber
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }







        #endregion
    }
}
