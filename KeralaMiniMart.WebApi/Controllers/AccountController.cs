using System.Threading.Tasks;
using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeralaMiniMart.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IUserService _userService;
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private IProductImagesRepository _productImagesRepository;
        private IUserAddressRepository _userAddressRepository;
        private IOrderRepository _orderSummaryRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        private IApplicationUserRepository _applicationUserRepository;
        private IForgotPasswordRepository _forgotPasswordRepository;
        private IEmailService _emailService;

        public AccountController(IUserService userService, IEmailService emailService, IForgotPasswordRepository forgotPasswordRepository, ICategoryRepository categoryRepository, IApplicationUserRepository applicationUserRepository, IOrderDetailsRepository orderDetailsRepository, IOrderRepository orderSummaryRepository, IUserAddressRepository userAddressRepository, IProductImagesRepository productImagesRepository, IUnitOfWork unitOfWork)
        {
             _userService = userService;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _productImagesRepository = productImagesRepository;
            _userAddressRepository = userAddressRepository;
            _orderSummaryRepository = orderSummaryRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _applicationUserRepository = applicationUserRepository;
            _forgotPasswordRepository = forgotPasswordRepository;
            _emailService = emailService;
        }

        #region RegistrationWithImage
        [Route("api/Account/RegisterWithImage")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterWithImage request)
        {
            var response = await _userService.RegisterUserWithImage(request);

            if(response.error==true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message,ErrorMessage = response.ErrorMessage });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data });
           
        }
        #endregion

        #region RegistrationWithData
        [Route("api/Account/RegisterWithData")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterWithData([FromBody]RegisterWithData request)
        {
            var response =  _userService.RegisterUserWithData(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data });

        }
        #endregion

        #region UpdateProfile
        [Route("api/Account/UpdateProfile")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult UpdateProfile([FromBody]UpdateProfileRequestResource request)
        {
            var response = _userService.UpdateProfile(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });

        }
        #endregion

        #region Login
        [Route("api/Account/Login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _userService.LoginUser(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, ApplicationUserId = response.ApplicationUserId });
        }

        #endregion

        #region Forgot Password (sendOtp)
        [Route("api/Account/SendOtp")]
        [AllowAnonymous]
        [HttpGet]
        public  IActionResult SendOtp([FromQuery]BaseRequest request)
        {
            var response = _userService.OTPSend(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });

        }
        #endregion

        #region Forgot Password Verify OTP
        [Route("api/Account/VerifyOtp")]
        [AllowAnonymous]
        [HttpPost]
        public  IActionResult ForgotPassword([FromBody] VerifyOtpRequest request)
        {
            var response = _userService.VerifyOTP(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion


        #region Reset Password
        [Route("api/Account/ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var response = _userService.ResetPassword(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion


        #region Get My Profile
        [Route("api/Account/GetMyProfile")]
        [HttpGet]
        public IActionResult GetMyProfile(int ApplicationUserId)
        {
            var response = _userService.GetMyProfile(ApplicationUserId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message,data=response.data });
        }

        #endregion



    }
}
