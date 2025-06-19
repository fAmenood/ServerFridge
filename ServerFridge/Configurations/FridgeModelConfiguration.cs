using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerFridge.Models;

namespace ServerFridge.Configurations
{
    public class FridgeModelConfiguration:IEntityTypeConfiguration<FridgeModel>
    {
        public void Configure(EntityTypeBuilder<FridgeModel> builder)
        {
            builder.HasData
            (
                new FridgeModel
                {
                    Id = new Guid("3B93B477-08A5-4E4D-8FB7-637C47ADBEA1"),
                    Name = "Lenovo",
                    Year = 2014

                },
                new FridgeModel
                {
                    Id = new Guid("AAAEA151-8E40-4276-8A6A-1C275B120C1F"),
                    Name = "Xiaomi",
                    Year = 2013
                }

            );
        }
    }
}
