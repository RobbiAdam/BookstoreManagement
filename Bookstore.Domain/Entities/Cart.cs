using Bookstore.Domain.Entities;

public class Cart : BaseEntity
{
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

public class CartItem : BaseEntity
{
    public string BookId { get; set; }
    public virtual Book Book { get; set; }
    public virtual Cart Cart { get; set; }
    public int Quantity { get; set; }
}