namespace ApplicationCore.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime Order_Date { get; set; }
    public int Customer_Id { get; set; }
    public string CustomerName { get; set; }
    public string PaymentMethodId { get; set; }
    public string PaymentName { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingMethod { get; set; }
    public string BillAmount { get; set; }
    public string Order_Status { get; set; }
    
    public ICollection<Order_Details> Order_Details { get; set; }
    public Customer Customer { get; set; }
}