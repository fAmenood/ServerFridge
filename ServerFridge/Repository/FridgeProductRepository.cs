using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using System;

namespace ServerFridge.Repository
{
 
    public class FridgeProductRepository : IFridgeProductRepository
    {
        public readonly AppDbContext _appDbContext;
        public FridgeProductRepository(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }
        public async Task<List<FridgeProductsDTO>> GetFridgeProducts()
        {
            return await _appDbContext.FridgeProducts.Select(x => new FridgeProductsDTO
            {
                ProductId = x.ProductId,
                FridgeId = x.FridgeId,
                Id = x.Id,
                Quantity = x.Quantity,

                
            }).ToListAsync();
             
        }
        public async Task<FridgeProductsDTO> GetFridgeProductsById(Guid id)
        {
            var fridgeProd= await _appDbContext.FridgeProducts
              
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (fridgeProd == null) return null;

            return new FridgeProductsDTO
            {
                Id = fridgeProd.Id,
                FridgeId = fridgeProd.FridgeId,
                ProductId = fridgeProd.ProductId,
                Quantity = fridgeProd.Quantity
            };
                



            
        }
        public async Task<FridgeProductsDTO> AddProductToFridge(FridgeProductCreateDTO fridgeProductsDTO)
        {
                var fridgeExists = await _appDbContext.Fridges
              .AnyAsync(f => f.Id == fridgeProductsDTO.FridgeId);

            if (!fridgeExists)
            {
                throw new ArgumentException($"Fridge with id {fridgeProductsDTO.FridgeId} doesn't exist");
            }
            var productExists = await _appDbContext.Products
                .AnyAsync(p => p.Id == fridgeProductsDTO.ProductId);

            if (!productExists)
            {
                throw new ArgumentException($"Product with id {fridgeProductsDTO.ProductId} doesn't exist");
            }

            // Проверяем, не существует ли уже такой связи
            var existingRelation = await _appDbContext.FridgeProducts
                .FirstOrDefaultAsync(fp =>
                    fp.FridgeId == fridgeProductsDTO.FridgeId &&
                    fp.ProductId == fridgeProductsDTO.ProductId);

            if (existingRelation != null)
            {
                // Если связь уже существует - обновляем количество
                existingRelation.Quantity += fridgeProductsDTO.Quantity;
                await _appDbContext.SaveChangesAsync();

                return new FridgeProductsDTO
                {
                    Id = existingRelation.Id,
                    FridgeId = existingRelation.FridgeId,
                    ProductId = existingRelation.ProductId,
                    Quantity = existingRelation.Quantity
                };
            }

            // Создаем новую связь
            var newFridgeProduct = new FridgeProducts
            {
                Id = Guid.NewGuid(),
                FridgeId = fridgeProductsDTO.FridgeId,
                ProductId = fridgeProductsDTO.ProductId,
                Quantity = fridgeProductsDTO.Quantity
            };

            await _appDbContext.FridgeProducts.AddAsync(newFridgeProduct);
            await _appDbContext.SaveChangesAsync();

            // Возвращаем созданный объект с ID
            return new FridgeProductsDTO
            {
                Id = newFridgeProduct.Id,
                FridgeId = newFridgeProduct.FridgeId,
                ProductId = newFridgeProduct.ProductId,
                Quantity = newFridgeProduct.Quantity
            };
        }
 
        public async Task<FridgeProductsDTO> UpdateFridgeProducts(Guid id, UpdateFridgeProductsDTO updateDto)
        {
            var fridgeProduct = await _appDbContext.FridgeProducts.FindAsync(id);
            if (fridgeProduct == null)
            {
                return null;
            }

            // Проверка на дубликат при изменении связей
            if (updateDto.FridgeId.HasValue || updateDto.ProductId.HasValue)
            {
                var newFridgeId = updateDto.FridgeId ?? fridgeProduct.FridgeId;
                var newProductId = updateDto.ProductId ?? fridgeProduct.ProductId;

                var duplicateExists = await _appDbContext.FridgeProducts
                    .AnyAsync(fp =>
                        fp.Id != id &&
                        fp.FridgeId == newFridgeId &&
                        fp.ProductId == newProductId);

                if (duplicateExists)
                {
                    throw new ArgumentException("This product already exists in the specified fridge");
                }
            }

            // Обновляем поля
            if (updateDto.Quantity.HasValue)
            {
                fridgeProduct.Quantity = updateDto.Quantity.Value;
            }

            if (updateDto.ProductId.HasValue)
            {
                fridgeProduct.ProductId = updateDto.ProductId.Value;
            }

            if (updateDto.FridgeId.HasValue)
            {
                fridgeProduct.FridgeId = updateDto.FridgeId.Value;
            }

            _appDbContext.FridgeProducts.Update(fridgeProduct);
            await _appDbContext.SaveChangesAsync();

            return new FridgeProductsDTO
            {
                Id = fridgeProduct.Id,
                FridgeId = fridgeProduct.FridgeId,
                ProductId = fridgeProduct.ProductId,
                Quantity = fridgeProduct.Quantity
            };
        }
        public async Task<bool> DeleteFridgeProducts(Guid id)
        {
            var delFrProds = await _appDbContext.FridgeProducts.FindAsync(id);
            if(delFrProds == null) return false;

            _appDbContext.FridgeProducts.Remove(delFrProds);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> ZeroQuantityFridgeProducts()
        {
            return await _appDbContext.Database.ExecuteSqlRawAsync("EXEC ZeroQuantityFridgeProductes");
        }

    }
}
