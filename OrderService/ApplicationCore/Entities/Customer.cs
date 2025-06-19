namespace ApplicationCore.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Profile_PIC { get; set; }
    public ICollection<User_Address> UserAddresses { get; set; }
    public ICollection<ApplicationCore.Entities.Order> Orders { get; set; }
}