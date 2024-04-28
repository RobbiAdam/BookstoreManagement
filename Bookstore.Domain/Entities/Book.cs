using System.ComponentModel.DataAnnotations;
namespace Bookstore.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }= "";
        public string Description { get; set; } = "Description not available";
        public string Author { get; set; } = "Anonymous";
        public string GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [Range(0, int.MaxValue)]
        public double Price { get; set; } = 0;    

    }
}
