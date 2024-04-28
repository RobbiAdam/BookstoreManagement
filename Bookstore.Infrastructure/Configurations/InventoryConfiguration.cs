using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(i => i.Quantity).HasDefaultValue(0);

            builder.HasOne(b => b.Book)
                    .WithMany()
                    .HasForeignKey(b => b.BookId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
