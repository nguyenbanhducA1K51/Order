namespace ApplicationCore.Entities;

public class Payment_Type
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Payment_Method> PaymentMethods { get; set; }
}