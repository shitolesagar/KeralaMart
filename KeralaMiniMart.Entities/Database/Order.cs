using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Order
    {
        public int Id { get; set; }

        public decimal SubTotalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public decimal GSTPrice { get; set; }
        public decimal DeliveryCharges { get; set; }

        public string OrderNumber { get; set; }
        public string TransactionId { get; set; }

        public string DeliveredSmsResponse { get; set; }

        public string Comments { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int UserAddressId { get; set; }
        public UserAddress UserAddress { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int MRP { get; set; }

        public int PaymentStatusId { get; set; }
        public OrderPaymentStatus PaymentStatus { get; set; }

        public int DeliveryStatusId { get; set; }
        public OrderDeliveryStatus DeliveryStatus { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
