using ApplicationCore.Entities;

namespace Order.Models;

public class CustomerDetailModal
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Profile_PIC { get; set; }
    public ICollection<AddressModal> Addresses { get; set; }

}