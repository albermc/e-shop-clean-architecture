using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
	internal class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
	{
		public void Configure(EntityTypeBuilder<LineItem> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne<Product>()
				.WithMany()
				.HasForeignKey(li => li.ProductId);

			builder.OwnsOne(x => x.Price);
			builder.Property(x => x.Price).HasConversion(x => x.Amount, value => new Money("USD", value));
		}
	}
}
