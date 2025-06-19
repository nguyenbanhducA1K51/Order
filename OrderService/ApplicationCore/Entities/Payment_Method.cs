namespace ApplicationCore.Entities;

public class Payment_Method
{
    public int Id { get; set; }
    public int PaymentTypeId { get; set; }
    public string Provider { get; set; }
    public int AccountNumer{get;set;}
    public bool isDefault {get;set;}
    public DateTime Expiry{get;set;}
    public Payment_Type PaymentType{get;set;}
}