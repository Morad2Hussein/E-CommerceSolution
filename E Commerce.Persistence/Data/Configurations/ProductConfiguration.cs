using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Commerce.Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(x => x.Name)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);
            builder.Property(x => x.PictureUrl)
                   .HasMaxLength(200);
            builder.Property(x => x.Price)
                     .HasPrecision(18, 2);
            #region RelationShip
            builder.HasOne(x => x.ProductBrand)
                   .WithMany()
                   .HasForeignKey(x => x.BrandId);

            builder.HasOne(x => x.ProductType)
                   .WithMany()
                   .HasForeignKey(x => x.TypeId);


            #endregion


        }
    }
}
