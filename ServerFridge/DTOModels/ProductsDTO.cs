using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class ProductsDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int DefaultQuantity { get; set; }
    }
}
