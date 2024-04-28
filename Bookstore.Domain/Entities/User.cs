namespace Bookstore.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
    }
}
