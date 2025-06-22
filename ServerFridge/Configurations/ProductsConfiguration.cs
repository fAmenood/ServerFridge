using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerFridge.Models;

namespace ServerFridge.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasData
            (
                new Products
                {
                    Id = new Guid("20FADC8C-7B02-4668-B652-073BCDE750FC"),
                    Name = "Apple",
                    DefaultQuantity = 7
                },
                new Products
                {
                    Id = new Guid("1605D6A2-5E70-44ED-9393-BCCB8E46B910"),
                    Name = "Banana",
                    DefaultQuantity = 5

                },
                new Products
                {
                    Id = new Guid("9B19425C-2503-48CB-B823-8A123B3A8CE3"),
                    Name= "Sushi rolls",
                    DefaultQuantity = 10 
                }

            );
        }
    }
}
