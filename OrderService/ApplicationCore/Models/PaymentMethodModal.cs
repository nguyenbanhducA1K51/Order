namespace Order.Models;

public class PaymentMethodModal
{
    public int Id { get; set; }
    public int PaymentTypeId { get; set; }
    public string Provider { get; set; }
    public int AccountNumer{get;set;}
    public bool isDefault {get;set;}
    public DateTime Expiry{get;set;}
    
}