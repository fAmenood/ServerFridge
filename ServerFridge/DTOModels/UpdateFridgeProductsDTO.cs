namespace ServerFridge.DTOModels
{
    public class UpdateFridgeProductsDTO
    {



        public Guid? ProductId { get; set; }



        public Guid? FridgeId { get; set; }

        public int? Quantity { get; set; }
    }
}
