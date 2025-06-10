namespace ApplicationCore.Entities;

public class Shopping_Cart_Item
{
    public int Id { get; set; }
    public int Cart_Id { get; set; }
    public int Product_Id { get; set; }
    public int ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Shopping_Cart ShoppingCart { get; set; }
}