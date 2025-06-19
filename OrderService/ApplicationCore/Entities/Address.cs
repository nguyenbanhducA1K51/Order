namespace ApplicationCore.Entities;

public class Address
{
    public int Id { get; set; }
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public ICollection<User_Address> User_Address { get; set; }
}