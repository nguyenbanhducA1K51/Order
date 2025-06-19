namespace Order.Models;

public class OrderModal
{
        public DateTime Order_Date { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingMethod { get; set; }
        public string BillAmount { get; set; }
        public string Order_Status { get; set; }
}