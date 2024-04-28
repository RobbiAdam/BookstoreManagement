namespace Bookstore.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int Quantity { get; set; }
        public string BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
