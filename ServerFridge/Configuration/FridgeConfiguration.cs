using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerFridge.Models;

namespace ServerFridge.Configuration
{
    public class FridgeConfiguration: IEntityTypeConfiguration<Fridge>
    {
        public void Configure(EntityTypeBuilder<Fridge> builder)
        {
            builder.HasData
            (
                new Fridge
                {
                    Id = new Guid("2FCD1D29-5C3B-4089-B3D1-EC8524069741"),
                    Name = "Appolon",
                    OwnerName = "Ivan Ivanov",
                    ModelId = new Guid("3B93B477-08A5-4E4D-8FB7-637C47ADBEA1")
                },
                new Fridge
                {
                    Id = new Guid("6092B048-AFAF-429C-A95A-07F57CA3A58F"),
                    Name="Oxygen 3.0",
                    OwnerName ="Alex Alexdrov",
                    ModelId = new Guid("AAAEA151-8E40-4276-8A6A-1C275B120C1F")
                },
                new Fridge
                {
                    Id =new Guid("2F46B686-1436-46DB-A85C-9863BCADD7EA"),
                    Name="Nevermore",
                    OwnerName ="Nikolai Nikolaev",
                    ModelId = new Guid("3B93B477-08A5-4E4D-8FB7-637C47ADBEA1")
                }


            );
        }
    }
}
