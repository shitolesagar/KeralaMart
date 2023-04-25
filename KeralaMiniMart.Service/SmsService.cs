using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.WebViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KeralaMiniMart.Service
{
    public interface ISmsService
    {
        string SendSms(string phoneNumber, string Message);
        string CreateAccountVerificationMessage(string otp);
        string CreateDeliveryMessage(string orderNumber);
        string CancelDeliveryMessage(string orderNumber);
    }

    public class SmsService : ISmsService
    {
        private readonly IConfigurationDataRepository _configurationDataRepository;

        public SmsService(IConfigurationDataRepository configurationDataRepository)
        {
            _configurationDataRepository = configurationDataRepository;
        }
        public string SendSms(string phoneNumber, string Message)
        {
            String message = HttpUtility.UrlEncode(Message);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "qzqsh1DRK2E-DJVndNaGiEQVHyNihKo1TSa7dj0jr5"},
                {"numbers" , phoneNumber},
                {"message" , message},
                {"sender" , "KMMART"}
                //{"test", "true" }
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }

        public string CreateAccountVerificationMessage(string otp)
        {
            string hashCode = _configurationDataRepository.GetAll().FirstOrDefault()?.SmsHashKey;
            return string.Format("<#> Your Kerala Mini Mart OTP is: {0}. Note: Please DO NOT SHARE this OTP with anyone to ensure account's security.%n{1}", otp, hashCode);
        }

        public string CreateDeliveryMessage(string orderNumber)
        {
            return string.Format("Delivered: we are happy we have delivered your Order number: {0}", orderNumber);
        }

        public string CancelDeliveryMessage(string orderNumber)
        {
            return string.Format("We're sorry to inform that your order {0} has been cancelled.", orderNumber);
        }
    }
}
