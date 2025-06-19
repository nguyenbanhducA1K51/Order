namespace ApplicationCore.Entities;

public class Shopping_Cart
{
    public int Id { get; set; }
    public int Customer_Id { get; set; }
    public int CustomerName { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Shopping_Cart_Item> Shopping_Cart_Items { get; set; }
    
}