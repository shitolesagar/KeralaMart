using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class ForgotPassword
    {
        public int Id { get; set; }
        public string OTP { get; set; }
        public string VerificationCode { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsUsed { get; set; }
        public string Token { get; set; }
        public string SMSResponse { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
