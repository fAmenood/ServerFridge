using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerFridge.Models;

namespace ServerFridge.Configuration
{
    public class FridgeProductsConfiguration: IEntityTypeConfiguration<FridgeProducts>
    {
        public void Configure(EntityTypeBuilder<FridgeProducts> builder)
        {
            builder.HasData
            (
                new FridgeProducts
                {
                    Id = new Guid("30ED1B4B-B7F2-4FE5-9DAA-608C8083CCFF"),
                    FridgeId = new Guid("2FCD1D29-5C3B-4089-B3D1-EC8524069741"),
                    ProductId=new Guid("20FADC8C-7B02-4668-B652-073BCDE750FC"),
                    Quantity = 10
                },
                new FridgeProducts
                { 
                    Id=new Guid("FFA74149-1AB8-42CF-9015-B4FFB95B0762"),
                    FridgeId=new Guid("6092B048-AFAF-429C-A95A-07F57CA3A58F"),
                    ProductId =new Guid("9B19425C-2503-48CB-B823-8A123B3A8CE3"),
                    Quantity = 19
                },

                new FridgeProducts
                {
                    Id=new Guid("7A3192AE-1BCD-4A4A-85ED-C998ACF2E2A7"),
                    ProductId=new Guid("1605D6A2-5E70-44ED-9393-BCCB8E46B910"),
                    FridgeId= new Guid("2FCD1D29-5C3B-4089-B3D1-EC8524069741"),
                    
                    Quantity = 6
                }

            );
        }
    }
}
