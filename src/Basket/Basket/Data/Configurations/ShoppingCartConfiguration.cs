namespace Basket.Data.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UserName)
            .IsUnique();

        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(100);

        // One to many relationship between ShoppingCart and ShoppingCartItem
        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey(e => e.ShoppingCartId);
    }
}