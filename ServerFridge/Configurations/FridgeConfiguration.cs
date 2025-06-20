using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerFridge.Models;

namespace ServerFridge.Configurations
{
    public class FridgeConfiguration: IEntityTypeConfiguration<Fridge>
    {
        public void Configure(EntityTypeBuilder<Fridge> builder)
        {
            var fridgeId1 = Guid.Parse("A1B2C3D4-1234-5678-9012-ABCDEF123456");
            var fridgeId2 = Guid.Parse("B2C3D4E5-2345-6789-0123-BCDEF1234567");
            var fridgeId3 = Guid.Parse("C3D4E5F6-3456-7890-1234-CDEF12345678");
            var fridgeId4 = Guid.Parse("D4E5F6F7-4567-8901-2345-DEF123456789");

            // GUID для моделей (должны соответствовать существующим моделям)
            var modelId1 = Guid.Parse("3B93B477-08A5-4E4D-8FB7-637C47ADBEA1");
            var modelId2 = Guid.Parse("AAAEA151-8E40-4276-8A6A-1C275B120C1F");

            // Указываем явное имя таблицы
            builder.ToTable("Fridges");

            // Настраиваем маппинг свойств
            builder.Property(f => f.Id)
                .HasColumnName("Fridge Id");

            builder.Property(f => f.ModelId)
                .HasColumnName("Model Id");

            builder.HasData
            (
                new Fridge
                {
                    Id = fridgeId1,
                    Name = "Appolon",
                    OwnerName = "Ivan Ivanov",
                    ModelId = modelId1
                },
                new Fridge
                {
                    Id=fridgeId2,
                    Name="Oxygen 3.0",
                    OwnerName ="Alex Alexdrov",
                    ModelId = modelId2
                },
                new Fridge
                {
                    Id=fridgeId4,
                    Name="Nevermore",
                    OwnerName ="Nikolai Nikolaev",
                    ModelId = modelId1
                }


            );
        }
    }
}
