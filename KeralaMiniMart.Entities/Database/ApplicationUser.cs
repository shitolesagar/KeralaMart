using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string MobileNumber { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsOTPVerified { get; set; }
        public string Username { get; set; }
        public string City { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<ActivityLog> ActivityLogs { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<UserDeviceInfo> UserDeviceInfos { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<ForgotPassword> ForgotPasswords { get; set; }
    }
}
