using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeralaMiniMart.Web.Controllers
{
    public class DignosisController : Controller
    {
        private readonly ISmsService _smsService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public DignosisController(ISmsService smsService, IUserService userService, IUnitOfWork unitOfWork, IApplicationUserRepository applicationUserRepository)
        {
            _smsService = smsService;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _applicationUserRepository = applicationUserRepository;
        }
        public IActionResult Index(IFormFile file)
        {
            ViewBag.currentDirectory = System.IO.Directory.GetCurrentDirectory();
            return View();
        }

        public IActionResult CheckImagePath(string imagePath)
        {
            ViewBag.ImagePath = imagePath;
            return View();
        }

        public IActionResult SendSms(string message="this is default test message", string number="919423237999")
        {
            //string data = _smsService.SendSms(number, message);
            string data = _smsService.SendSms(number, _smsService.CreateAccountVerificationMessage("7676"));
            return Content("OK   :" + data);
        }
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin()
        {
            var user = _applicationUserRepository.FindByEmail("admin@gmail.com");
            if (user == null)
            {
                ApplicationUser AdminUser = new ApplicationUser()
                {
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    CreatedDateTime = DateTime.Now,
                    MobileNumber = "9423237999",
                    RoleId = 1
                };
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    AdminUser.PasswordSalt = hmac.Key;
                    AdminUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Reset1234"));
                }
                _applicationUserRepository.Add(AdminUser);
                await _unitOfWork.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Account");
        }
    }
}
