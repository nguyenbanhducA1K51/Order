namespace ApplicationCore.Entities;

public class User_Address
{
    public int Id { get; set; }
    public int Customer_Id { get; set; }
    public int Address_Id { get; set; }
    public bool IsDefaultAddress { get; set; }
    public Address Address { get; set; }
    public Customer Customer { get; set; }
}