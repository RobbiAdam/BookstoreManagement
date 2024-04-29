namespace Bookstore.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public double TotalAmount { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem : BaseEntity
    {
        public string BookId { get; set; }
        public virtual Book Book { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}